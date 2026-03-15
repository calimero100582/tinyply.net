using System;

namespace TinyPlyNet
{
    /// <summary>
    /// Immutable metadata for a PLY property declared in the header.
    /// </summary>
    public sealed class PlyPropertySchema
    {
        /// <summary>
        /// Creates a new property schema snapshot.
        /// </summary>
        public PlyPropertySchema(string name, Type propertyType, Type? listCountType)
        {
            Name = name;
            PropertyType = propertyType;
            ListCountType = listCountType;
            Kind = listCountType is null ? PlyPropertyKind.Scalar : PlyPropertyKind.List;
        }

        /// <summary>
        /// Property name declared in the PLY header.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// CLR type of the property values.
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        /// CLR type of the list count values, or null for scalar properties.
        /// </summary>
        public Type? ListCountType { get; }

        /// <summary>
        /// Gets whether the property is scalar or list-based.
        /// </summary>
        public PlyPropertyKind Kind { get; }

        /// <summary>
        /// Gets whether the property is a list property.
        /// </summary>
        public bool IsList => Kind == PlyPropertyKind.List;
    }
}
