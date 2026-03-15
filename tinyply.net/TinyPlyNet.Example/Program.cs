using System;
using System.IO;
using TinyPlyNet;
using TinyPlyNet.GaussianSplatting;

namespace TinyPlyNet.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var samplePath = args.Length > 0 ? args[0] : "gaussian-splat-minimal.ply";

            using (var validationStream = new FileStream(samplePath, FileMode.Open, FileAccess.Read))
            {
                var file = new PlyFile(validationStream);
                var validator = new GaussianSplattingValidator();
                var validation = validator.Validate(file);

                Console.WriteLine($"Schema valid: {validation.IsValid}");
                foreach (var issue in validation.Report.Issues)
                {
                    Console.WriteLine($"- {issue.Code}: {issue.Message}");
                }
            }

            using (var importStream = new FileStream(samplePath, FileMode.Open, FileAccess.Read))
            {
                var importer = new GaussianSplattingImporter();
                var data = importer.Import(importStream);

                Console.WriteLine($"Imported splats: {data.Count}");
                Console.WriteLine($"Position values: {data.Positions.Count}");
                Console.WriteLine($"DC feature values: {data.DcFeatures.Count}");
            }
        }
    }
}
