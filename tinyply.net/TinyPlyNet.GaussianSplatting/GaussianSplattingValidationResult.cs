using TinyPlyNet.HighLevel;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// Combined layout and validation output for Gaussian Splatting schema checks.
    /// </summary>
    public sealed class GaussianSplattingValidationResult
    {
        /// <summary>
        /// Creates a validation result.
        /// </summary>
        public GaussianSplattingValidationResult(GaussianSplattingLayout? layout, ValidationReport report)
        {
            Layout = layout;
            Report = report;
        }

        /// <summary>
        /// Resolved layout when validation succeeds.
        /// </summary>
        public GaussianSplattingLayout? Layout { get; }

        /// <summary>
        /// Validation report for the schema.
        /// </summary>
        public ValidationReport Report { get; }

        /// <summary>
        /// Gets whether the schema is supported for import.
        /// </summary>
        public bool IsValid => Layout != null && Report.IsValid;
    }
}
