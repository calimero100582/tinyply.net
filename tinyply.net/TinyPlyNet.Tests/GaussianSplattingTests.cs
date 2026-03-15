using System.Collections.Generic;
using System.IO;
using System.Text;
using TinyPlyNet.GaussianSplatting;
using Xunit;

namespace TinyPlyNet.Tests;

public class GaussianSplattingTests
{
    [Fact]
    public void GetSchema_ExposesPropertyKindsAndTypes()
    {
        const string ply = """
                           ply
                           format ascii 1.0
                           element vertex 1
                           property float x
                           property list uchar int vertex_indices
                           end_header
                           1.5
                           3 0 1 2
                           """;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(ply));
        var file = new PlyFile(stream);
        var schema = file.GetSchema();
        var vertex = schema.FindElement("vertex");

        Assert.NotNull(vertex);
        Assert.Equal(2, vertex!.Properties.Count);
        Assert.Equal(PlyPropertyKind.Scalar, vertex.Properties[0].Kind);
        Assert.Equal(typeof(float), vertex.Properties[0].PropertyType);
        Assert.Equal(PlyPropertyKind.List, vertex.Properties[1].Kind);
        Assert.Equal(typeof(byte), vertex.Properties[1].ListCountType);
        Assert.Equal(typeof(int), vertex.Properties[1].PropertyType);
    }

    [Fact]
    public void ResolveAliases_ReportsAmbiguousCandidates()
    {
        const string ply = """
                           ply
                           format ascii 1.0
                           element vertex 1
                           property float x
                           property float pos_x
                           end_header
                           1 2
                           """;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(ply));
        var file = new PlyFile(stream);
        var schema = file.GetSchema();
        var vertex = schema.FindElement("vertex");

        var result = PlySchemaResolver.ResolveAliases(
            vertex!,
            new[]
            {
                new PropertyAliasDefinition("x", new[] { "pos_x" }, typeof(float))
            });

        Assert.False(result.Success);
        Assert.Single(result.Issues);
        Assert.Equal(PlySchemaIssueKind.AmbiguousProperty, result.Issues[0].Kind);
    }

    [Fact]
    public void Validator_AcceptsMinimalGaussianSample()
    {
        using var stream = File.OpenRead("gaussian-splat-minimal.ply");
        var file = new PlyFile(stream);
        var validator = new GaussianSplattingValidator();

        var result = validator.Validate(file);

        Assert.True(result.IsValid);
        Assert.NotNull(result.Layout);
        Assert.Equal("vertex", result.Layout!.BindingPlan.ElementName);
        Assert.Equal(4, result.Layout.BindingPlan.Groups.Count);
        Assert.Single(result.Layout.BindingPlan.ScalarProperties);
    }

    [Fact]
    public void Validator_RejectsMissingRequiredGaussianProperties()
    {
        const string ply = """
                           ply
                           format ascii 1.0
                           element vertex 1
                           property float x
                           property float y
                           property float z
                           property float opacity
                           end_header
                           0 0 0 1
                           """;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(ply));
        var validator = new GaussianSplattingValidator();

        var result = validator.Validate(stream);

        Assert.False(result.IsValid);
        Assert.Contains(result.Report.Issues, x => x.Code == "missingproperty");
    }

    [Fact]
    public void Importer_ReadsMinimalGaussianSample()
    {
        using var stream = File.OpenRead("gaussian-splat-minimal.ply");
        var importer = new GaussianSplattingImporter();

        var data = importer.Import(stream);

        Assert.Equal(2, data.Count);
        Assert.Equal(6, data.Positions.Count);
        Assert.Equal(6, data.Scales.Count);
        Assert.Equal(8, data.Rotations.Count);
        Assert.Equal(2, data.Opacities.Count);
        Assert.Equal(6, data.DcFeatures.Count);
        Assert.Equal(0.8f, data.Opacities[1]);
    }

    [Fact]
    public void Importer_ReadsKnownAliasVariant()
    {
        const string ply = """
                           ply
                           format ascii 1.0
                           element vertex 1
                           property float x
                           property float y
                           property float z
                           property float scale_x
                           property float scale_y
                           property float scale_z
                           property float qw
                           property float qx
                           property float qy
                           property float qz
                           property float alpha
                           property float dc_0
                           property float dc_1
                           property float dc_2
                           end_header
                           1 2 3 4 5 6 1 0 0 0 0.9 0.11 0.22 0.33
                           """;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(ply));
        var importer = new GaussianSplattingImporter();

        var data = importer.Import(stream);

        Assert.Single(data.Opacities);
        Assert.Equal(0.9f, data.Opacities[0]);
        Assert.Equal(new List<float> { 4f, 5f, 6f }, data.Scales);
    }
}
