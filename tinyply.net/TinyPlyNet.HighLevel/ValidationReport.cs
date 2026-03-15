using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet.HighLevel
{
    /// <summary>
    /// Aggregated validation result.
    /// </summary>
    public sealed class ValidationReport
    {
        /// <summary>
        /// Creates a validation report.
        /// </summary>
        public ValidationReport(IEnumerable<ValidationIssue> issues)
        {
            Issues = new ReadOnlyCollection<ValidationIssue>(issues.ToList());
        }

        /// <summary>
        /// Collected issues.
        /// </summary>
        public IReadOnlyList<ValidationIssue> Issues { get; }

        /// <summary>
        /// Gets whether the report contains no error-level issues.
        /// </summary>
        public bool IsValid => Issues.All(x => x.Severity != ValidationSeverity.Error);
    }
}
