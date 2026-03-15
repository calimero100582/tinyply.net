using TinyPlyNet;

namespace TinyPlyNet.HighLevel
{
    /// <summary>
    /// Binds a canonical semantic name to a resolved PLY property.
    /// </summary>
    public sealed class ResolvedProperty
    {
        /// <summary>
        /// Creates a resolved property.
        /// </summary>
        public ResolvedProperty(string canonicalName, PlyPropertySchema property)
        {
            CanonicalName = canonicalName;
            Property = property;
        }

        /// <summary>
        /// Canonical semantic name.
        /// </summary>
        public string CanonicalName { get; }

        /// <summary>
        /// Resolved schema property.
        /// </summary>
        public PlyPropertySchema Property { get; }
    }
}
