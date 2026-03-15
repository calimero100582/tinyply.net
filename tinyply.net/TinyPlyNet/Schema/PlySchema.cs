using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Immutable metadata snapshot for a parsed PLY header.
    /// </summary>
    public sealed class PlySchema
    {
        private readonly IReadOnlyList<PlyElementSchema> _elements;

        /// <summary>
        /// Creates a new schema snapshot.
        /// </summary>
        public PlySchema(IEnumerable<PlyElementSchema> elements)
        {
            _elements = new ReadOnlyCollection<PlyElementSchema>(elements.ToList());
        }

        /// <summary>
        /// Elements declared in the PLY header.
        /// </summary>
        public IReadOnlyList<PlyElementSchema> Elements => _elements;

        /// <summary>
        /// Finds an element by name using ordinal ignore-case matching.
        /// </summary>
        public PlyElementSchema? FindElement(string name)
        {
            return _elements.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
