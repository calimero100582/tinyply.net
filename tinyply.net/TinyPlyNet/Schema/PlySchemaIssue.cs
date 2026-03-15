using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Describes a schema resolution issue.
    /// </summary>
    public sealed class PlySchemaIssue
    {
        /// <summary>
        /// Creates a new schema issue.
        /// </summary>
        public PlySchemaIssue(
            PlySchemaIssueKind kind,
            string elementName,
            string targetName,
            string message,
            IEnumerable<string>? candidateNames = null)
        {
            Kind = kind;
            ElementName = elementName;
            TargetName = targetName;
            Message = message;
            CandidateNames = new ReadOnlyCollection<string>((candidateNames ?? Enumerable.Empty<string>()).ToList());
        }

        /// <summary>
        /// Issue classification.
        /// </summary>
        public PlySchemaIssueKind Kind { get; }

        /// <summary>
        /// Element that was being resolved.
        /// </summary>
        public string ElementName { get; }

        /// <summary>
        /// Canonical property or group member name that failed resolution.
        /// </summary>
        public string TargetName { get; }

        /// <summary>
        /// Human-readable issue message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Candidate property names considered during resolution.
        /// </summary>
        public IReadOnlyList<string> CandidateNames { get; }
    }
}
