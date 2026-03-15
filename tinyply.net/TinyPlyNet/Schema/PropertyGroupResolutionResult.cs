namespace TinyPlyNet
{
    /// <summary>
    /// Result of resolving a logical property group.
    /// </summary>
    public sealed class PropertyGroupResolutionResult
    {
        /// <summary>
        /// Creates a new property group resolution result.
        /// </summary>
        public PropertyGroupResolutionResult(PropertyGroupDefinition group, PropertyAliasResolutionResult aliasResult)
        {
            Group = group;
            AliasResult = aliasResult;
        }

        /// <summary>
        /// The group definition that was resolved.
        /// </summary>
        public PropertyGroupDefinition Group { get; }

        /// <summary>
        /// Underlying alias resolution result for the group members.
        /// </summary>
        public PropertyAliasResolutionResult AliasResult { get; }

        /// <summary>
        /// Gets whether the group resolved without issues.
        /// </summary>
        public bool Success => AliasResult.Success;
    }
}
