namespace TinyPlyNet
{
    /// <summary>
    /// Classifies schema resolution failures.
    /// </summary>
    public enum PlySchemaIssueKind
    {
        /// <summary>
        /// The target element could not be found.
        /// </summary>
        MissingElement,

        /// <summary>
        /// No property matched the requested alias definition.
        /// </summary>
        MissingProperty,

        /// <summary>
        /// More than one property matched the requested alias definition.
        /// </summary>
        AmbiguousProperty,

        /// <summary>
        /// A property matched by name but had an incompatible CLR type.
        /// </summary>
        TypeMismatch
    }
}
