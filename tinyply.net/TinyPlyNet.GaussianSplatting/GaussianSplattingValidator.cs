using System.IO;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// Validates whether a PLY file uses a supported Gaussian Splatting layout.
    /// </summary>
    public sealed class GaussianSplattingValidator
    {
        private readonly GaussianSplattingLayoutResolver _resolver = new GaussianSplattingLayoutResolver();

        /// <summary>
        /// Validates the schema of a parsed PLY file.
        /// </summary>
        public GaussianSplattingValidationResult Validate(PlyFile file)
        {
            return _resolver.Resolve(file.GetSchema());
        }

        /// <summary>
        /// Validates a PLY stream by parsing its header.
        /// </summary>
        public GaussianSplattingValidationResult Validate(Stream stream)
        {
            var file = new PlyFile(stream);
            return Validate(file);
        }
    }
}
