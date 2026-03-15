using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Result of resolving canonical property aliases against an element schema.
    /// </summary>
    public sealed class PropertyAliasResolutionResult
    {
        /// <summary>
        /// Creates a new alias resolution result.
        /// </summary>
        public PropertyAliasResolutionResult(
            string elementName,
            IEnumerable<PropertyAliasBinding> bindings,
            IEnumerable<PlySchemaIssue> issues)
        {
            ElementName = elementName;
            Bindings = new ReadOnlyCollection<PropertyAliasBinding>(bindings.ToList());
            Issues = new ReadOnlyCollection<PlySchemaIssue>(issues.ToList());
        }

        /// <summary>
        /// Element that was resolved.
        /// </summary>
        public string ElementName { get; }

        /// <summary>
        /// Successful property bindings.
        /// </summary>
        public IReadOnlyList<PropertyAliasBinding> Bindings { get; }

        /// <summary>
        /// Issues encountered during resolution.
        /// </summary>
        public IReadOnlyList<PlySchemaIssue> Issues { get; }

        /// <summary>
        /// Gets whether resolution completed without issues.
        /// </summary>
        public bool Success => Issues.Count == 0;
    }
}
