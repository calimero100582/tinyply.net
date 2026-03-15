using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyPlyNet
{
    /// <summary>
    /// Resolves canonical property definitions against PLY schema metadata.
    /// </summary>
    public static class PlySchemaResolver
    {
        /// <summary>
        /// Resolves alias definitions against an element schema.
        /// </summary>
        public static PropertyAliasResolutionResult ResolveAliases(
            PlyElementSchema element,
            IEnumerable<PropertyAliasDefinition> definitions)
        {
            var bindings = new List<PropertyAliasBinding>();
            var issues = new List<PlySchemaIssue>();

            foreach (var definition in definitions)
            {
                var matches = element.FindProperties(definition.CandidateNames);
                if (matches.Count == 0)
                {
                    if (definition.Required)
                    {
                        issues.Add(new PlySchemaIssue(
                            PlySchemaIssueKind.MissingProperty,
                            element.Name,
                            definition.CanonicalName,
                            $"Could not resolve required property '{definition.CanonicalName}' on element '{element.Name}'.",
                            definition.CandidateNames));
                    }

                    continue;
                }

                if (matches.Count > 1)
                {
                    issues.Add(new PlySchemaIssue(
                        PlySchemaIssueKind.AmbiguousProperty,
                        element.Name,
                        definition.CanonicalName,
                        $"Property '{definition.CanonicalName}' matched more than one property on element '{element.Name}'.",
                        matches.Select(x => x.Name)));
                    continue;
                }

                var match = matches[0];
                if (definition.ExpectedType != null && definition.ExpectedType != match.PropertyType)
                {
                    issues.Add(new PlySchemaIssue(
                        PlySchemaIssueKind.TypeMismatch,
                        element.Name,
                        definition.CanonicalName,
                        $"Property '{definition.CanonicalName}' expected CLR type '{definition.ExpectedType.Name}' but matched '{match.PropertyType.Name}'.",
                        definition.CandidateNames));
                    continue;
                }

                bindings.Add(new PropertyAliasBinding(
                    definition.CanonicalName,
                    definition.CandidateNames.First(x => string.Equals(x, match.Name, StringComparison.OrdinalIgnoreCase)),
                    match));
            }

            return new PropertyAliasResolutionResult(element.Name, bindings, issues);
        }

        /// <summary>
        /// Resolves a group definition against an element schema.
        /// </summary>
        public static PropertyGroupResolutionResult ResolveGroup(PlyElementSchema element, PropertyGroupDefinition group)
        {
            return new PropertyGroupResolutionResult(group, ResolveAliases(element, group.Members));
        }
    }
}
