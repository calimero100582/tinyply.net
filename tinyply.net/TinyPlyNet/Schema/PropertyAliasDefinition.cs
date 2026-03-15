using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Defines a canonical property name and the aliases that may satisfy it.
    /// </summary>
    public sealed class PropertyAliasDefinition
    {
        /// <summary>
        /// Creates a new alias definition.
        /// </summary>
        public PropertyAliasDefinition(
            string canonicalName,
            IEnumerable<string>? aliases = null,
            Type? expectedType = null,
            bool required = true)
        {
            CanonicalName = canonicalName;
            ExpectedType = expectedType;
            Required = required;

            var names = new List<string> { canonicalName };
            if (aliases != null)
            {
                names.AddRange(aliases.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            CandidateNames = new ReadOnlyCollection<string>(
                names
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList());
        }

        /// <summary>
        /// Canonical property name used by the caller.
        /// </summary>
        public string CanonicalName { get; }

        /// <summary>
        /// Candidate names that may match a property in the schema.
        /// </summary>
        public IReadOnlyList<string> CandidateNames { get; }

        /// <summary>
        /// Optional expected CLR type for the matched property.
        /// </summary>
        public Type? ExpectedType { get; }

        /// <summary>
        /// Indicates whether the property is required for a successful resolution.
        /// </summary>
        public bool Required { get; }
    }
}
