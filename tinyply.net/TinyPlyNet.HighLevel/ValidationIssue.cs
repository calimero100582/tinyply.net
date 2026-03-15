namespace TinyPlyNet.HighLevel
{
    /// <summary>
    /// Structured validation issue used by higher-level import pipelines.
    /// </summary>
    public sealed class ValidationIssue
    {
        /// <summary>
        /// Creates a validation issue.
        /// </summary>
        public ValidationIssue(string code, string message, ValidationSeverity severity)
        {
            Code = code;
            Message = message;
            Severity = severity;
        }

        /// <summary>
        /// Stable issue code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Human-readable issue message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Severity classification.
        /// </summary>
        public ValidationSeverity Severity { get; }
    }
}
