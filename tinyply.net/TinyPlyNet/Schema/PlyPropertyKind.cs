namespace TinyPlyNet
{
    /// <summary>
    /// Represents the storage kind of a PLY property.
    /// </summary>
    public enum PlyPropertyKind
    {
        /// <summary>
        /// A single scalar value per element row.
        /// </summary>
        Scalar,

        /// <summary>
        /// A list value per element row.
        /// </summary>
        List
    }
}
