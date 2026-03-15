using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Defines a logical group of canonical properties.
    /// </summary>
    public sealed class PropertyGroupDefinition
    {
        /// <summary>
        /// Creates a new property group definition.
        /// </summary>
        public PropertyGroupDefinition(string name, IEnumerable<PropertyAliasDefinition> members)
        {
            Name = name;
            Members = new ReadOnlyCollection<PropertyAliasDefinition>(members.ToList());
        }

        /// <summary>
        /// Logical group name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Group members to resolve.
        /// </summary>
        public IReadOnlyList<PropertyAliasDefinition> Members { get; }
    }
}
