using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet.HighLevel
{
    /// <summary>
    /// Binds a logical group name to a fixed set of resolved properties.
    /// </summary>
    public sealed class ResolvedPropertyGroup
    {
        /// <summary>
        /// Creates a resolved property group.
        /// </summary>
        public ResolvedPropertyGroup(string name, string elementName, IEnumerable<ResolvedProperty> properties)
        {
            Name = name;
            ElementName = elementName;
            Properties = new ReadOnlyCollection<ResolvedProperty>(properties.ToList());
        }

        /// <summary>
        /// Logical group name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Element that owns the properties.
        /// </summary>
        public string ElementName { get; }

        /// <summary>
        /// Resolved properties in the group.
        /// </summary>
        public IReadOnlyList<ResolvedProperty> Properties { get; }
    }
}
