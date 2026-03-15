using System;
using System.Collections.Generic;
using System.Linq;
using TinyPlyNet.HighLevel;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// Resolves known Gaussian Splatting property layouts from a generic PLY schema.
    /// </summary>
    public sealed class GaussianSplattingLayoutResolver
    {
        /// <summary>
        /// Resolves a supported 3DGS layout from the provided schema.
        /// </summary>
        public GaussianSplattingValidationResult Resolve(PlySchema schema)
        {
            var issues = new List<ValidationIssue>();
            var vertexElement = schema.FindElement("vertex");
            if (vertexElement == null)
            {
                issues.Add(new ValidationIssue(
                    "missing_vertex_element",
                    "Could not find the required 'vertex' element.",
                    ValidationSeverity.Error));
                return new GaussianSplattingValidationResult(null, new ValidationReport(issues));
            }

            var groups = new[]
            {
                new PropertyGroupDefinition(
                    "position",
                    new[]
                    {
                        new PropertyAliasDefinition("x", expectedType: typeof(float)),
                        new PropertyAliasDefinition("y", expectedType: typeof(float)),
                        new PropertyAliasDefinition("z", expectedType: typeof(float))
                    }),
                new PropertyGroupDefinition(
                    "scale",
                    new[]
                    {
                        new PropertyAliasDefinition("scale_0", new[] { "scale_x", "sx" }, typeof(float)),
                        new PropertyAliasDefinition("scale_1", new[] { "scale_y", "sy" }, typeof(float)),
                        new PropertyAliasDefinition("scale_2", new[] { "scale_z", "sz" }, typeof(float))
                    }),
                new PropertyGroupDefinition(
                    "rotation",
                    new[]
                    {
                        new PropertyAliasDefinition("rot_0", new[] { "qw", "rot_w" }, typeof(float)),
                        new PropertyAliasDefinition("rot_1", new[] { "qx", "rot_x" }, typeof(float)),
                        new PropertyAliasDefinition("rot_2", new[] { "qy", "rot_y" }, typeof(float)),
                        new PropertyAliasDefinition("rot_3", new[] { "qz", "rot_z" }, typeof(float))
                    }),
                new PropertyGroupDefinition(
                    "dc_features",
                    new[]
                    {
                        new PropertyAliasDefinition("f_dc_0", new[] { "dc_0", "feature_dc_0" }, typeof(float)),
                        new PropertyAliasDefinition("f_dc_1", new[] { "dc_1", "feature_dc_1" }, typeof(float)),
                        new PropertyAliasDefinition("f_dc_2", new[] { "dc_2", "feature_dc_2" }, typeof(float))
                    })
            };

            var resolvedGroups = new List<ResolvedPropertyGroup>();
            foreach (var group in groups)
            {
                var result = PlySchemaResolver.ResolveGroup(vertexElement, group);
                issues.AddRange(result.AliasResult.Issues.Select(ToValidationIssue));
                if (result.Success)
                {
                    resolvedGroups.Add(new ResolvedPropertyGroup(
                        group.Name,
                        vertexElement.Name,
                        result.AliasResult.Bindings.Select(x => new ResolvedProperty(x.CanonicalName, x.Property))));
                }
            }

            var opacityResult = PlySchemaResolver.ResolveAliases(
                vertexElement,
                new[]
                {
                    new PropertyAliasDefinition("opacity", new[] { "alpha" }, typeof(float))
                });
            issues.AddRange(opacityResult.Issues.Select(ToValidationIssue));

            if (issues.Any(x => x.Severity == ValidationSeverity.Error))
            {
                return new GaussianSplattingValidationResult(null, new ValidationReport(issues));
            }

            var bindingPlan = new ElementBindingPlan(
                vertexElement.Name,
                opacityResult.Bindings.Select(x => new ResolvedProperty(x.CanonicalName, x.Property)),
                resolvedGroups);

            return new GaussianSplattingValidationResult(
                new GaussianSplattingLayout(bindingPlan),
                new ValidationReport(issues));
        }

        private static ValidationIssue ToValidationIssue(PlySchemaIssue issue)
        {
            return new ValidationIssue(
                issue.Kind.ToString().ToLowerInvariant(),
                issue.Message,
                ValidationSeverity.Error);
        }
    }
}
