using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet.HighLevel
{
    /// <summary>
    /// Generic read plan for a resolved PLY element.
    /// </summary>
    public sealed class ElementBindingPlan
    {
        private readonly IReadOnlyDictionary<string, ResolvedProperty> _scalars;
        private readonly IReadOnlyDictionary<string, ResolvedPropertyGroup> _groups;

        /// <summary>
        /// Creates a new binding plan.
        /// </summary>
        public ElementBindingPlan(
            string elementName,
            IEnumerable<ResolvedProperty> scalarProperties,
            IEnumerable<ResolvedPropertyGroup> groups)
        {
            ElementName = elementName;
            _scalars = new ReadOnlyDictionary<string, ResolvedProperty>(
                scalarProperties.ToDictionary(x => x.CanonicalName, StringComparer.OrdinalIgnoreCase));
            _groups = new ReadOnlyDictionary<string, ResolvedPropertyGroup>(
                groups.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Element targeted by the binding plan.
        /// </summary>
        public string ElementName { get; }

        /// <summary>
        /// Scalar property bindings.
        /// </summary>
        public IReadOnlyDictionary<string, ResolvedProperty> ScalarProperties => _scalars;

        /// <summary>
        /// Group property bindings.
        /// </summary>
        public IReadOnlyDictionary<string, ResolvedPropertyGroup> Groups => _groups;

        /// <summary>
        /// Gets a scalar property binding by canonical name.
        /// </summary>
        public ResolvedProperty GetScalar(string canonicalName)
        {
            return _scalars[canonicalName];
        }

        /// <summary>
        /// Gets a property group binding by group name.
        /// </summary>
        public ResolvedPropertyGroup GetGroup(string groupName)
        {
            return _groups[groupName];
        }
    }
}
