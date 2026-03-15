namespace TinyPlyNet
{
    /// <summary>
    /// Maps a canonical property name to an actual schema property.
    /// </summary>
    public sealed class PropertyAliasBinding
    {
        /// <summary>
        /// Creates a new alias binding.
        /// </summary>
        public PropertyAliasBinding(string canonicalName, string matchedAlias, PlyPropertySchema property)
        {
            CanonicalName = canonicalName;
            MatchedAlias = matchedAlias;
            Property = property;
        }

        /// <summary>
        /// Canonical property name used by the caller.
        /// </summary>
        public string CanonicalName { get; }

        /// <summary>
        /// Alias string that matched the schema.
        /// </summary>
        public string MatchedAlias { get; }

        /// <summary>
        /// Resolved schema property.
        /// </summary>
        public PlyPropertySchema Property { get; }
    }
}
