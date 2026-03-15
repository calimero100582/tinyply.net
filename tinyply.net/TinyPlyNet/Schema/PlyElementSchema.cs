using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Immutable metadata for a PLY element declared in the header.
    /// </summary>
    public sealed class PlyElementSchema
    {
        private readonly IReadOnlyList<PlyPropertySchema> _properties;

        /// <summary>
        /// Creates a new element schema snapshot.
        /// </summary>
        public PlyElementSchema(string name, int size, IEnumerable<PlyPropertySchema> properties)
        {
            Name = name;
            Size = size;
            _properties = new ReadOnlyCollection<PlyPropertySchema>(properties.ToList());
        }

        /// <summary>
        /// Element name declared in the PLY header.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Number of rows declared for the element.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Property metadata for the element.
        /// </summary>
        public IReadOnlyList<PlyPropertySchema> Properties => _properties;

        /// <summary>
        /// Finds a property by name using ordinal ignore-case matching.
        /// </summary>
        public PlyPropertySchema? FindProperty(string name)
        {
            return _properties.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds all properties matching a set of candidate names using ordinal ignore-case matching.
        /// </summary>
        public IReadOnlyList<PlyPropertySchema> FindProperties(IEnumerable<string> candidateNames)
        {
            var names = new HashSet<string>(candidateNames, StringComparer.OrdinalIgnoreCase);
            return new ReadOnlyCollection<PlyPropertySchema>(_properties.Where(x => names.Contains(x.Name)).ToList());
        }
    }
}
