using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NodeProg
{
    public abstract class Node
    {
        public static IEnumerable<Node> ParseNodes(
            string input,
            string separator,
            string openMarker,
            string closeMarker,
            string commentOpenMarker,
            string commentCloseMarker,
            string namedOpenMarker,
            string namedCloseMarker,
            string namedAssignMarker)
        {
            return ParseNodes(input, new NodeParseSettings
            {
                separator = separator,
                openMarker = openMarker,
                closeMarker = closeMarker,
                commentOpenMarker = commentOpenMarker,
                commentCloseMarker = commentCloseMarker,
                namedOpenMarker = namedOpenMarker,
                namedCloseMarker = namedCloseMarker,
                namedAssignMarker = namedAssignMarker
            });
        }

        public static IEnumerable<Node> ParseNodes(string input, NodeParseSettings parseSettings)
        {
            var listResults = ParseNodesInternal(0, input, parseSettings);
            var lastIndex = 0;
            foreach (var parseResult in listResults)
            {
                yield return parseResult.Node;
                lastIndex = parseResult.StartPosition + parseResult.Length;
            }
            if (lastIndex != input.Length)
                throw new FormatException($"Unexpected '{input[lastIndex]}' at position {lastIndex}.");
        }

        public static IEnumerable<Node> ParseNodes(string input)
        {
            return ParseNodes(input, new NodeParseSettings
            {
                separator = ",",
                openMarker = "(",
                closeMarker = ")",
                commentOpenMarker = "/*",
                commentCloseMarker = "*/",
                namedOpenMarker = "{",
                namedCloseMarker = "}",
                namedAssignMarker = "="
            });
        }

        public static IEnumerable<Node> ParseNodes(string input, string separator, string openMarker, string closeMarker)
        {
            return ParseNodes(input, new NodeParseSettings
            {
                separator = separator,
                openMarker = openMarker,
                closeMarker = closeMarker,
                commentOpenMarker = "/*",
                commentCloseMarker = "*/",
                namedOpenMarker = "{",
                namedCloseMarker = "}",
                namedAssignMarker = "=",
            });
        }

        public static NodeParseResult? TryParseNodeInternal(int index, string input, NodeParseSettings parseSettings)
        {
            return
                ListNode.TryParse(index, input, parseSettings) ??
                CommentNode.TryParse(index, input, parseSettings.commentOpenMarker, parseSettings.commentCloseMarker) ??
                NamedNode.TryParse(index, input, parseSettings) ??
                NumberNode.TryParse(index, input, new[] { parseSettings.closeMarker, parseSettings.separator, parseSettings.namedCloseMarker }) ??
                ContentNode.Parse(index, input, new[] { parseSettings.closeMarker, parseSettings.separator, parseSettings.namedCloseMarker });

        }
        public static IEnumerable<NodeParseResult> ParseNodesInternal(int index, string input, NodeParseSettings parseSettings)
        {
            if (input.Length == 0)
                yield break;

            if (string.Compare(input, index, parseSettings.closeMarker, 0, parseSettings.closeMarker.Length) == 0)
                yield break;

            while (true)
            {
                var parseResult = TryParseNodeInternal(index, input, parseSettings);

                Debug.Assert(parseResult != null);
                Debug.Assert(parseResult.Value.StartPosition == index);

                yield return parseResult.Value;

                index += parseResult.Value.Length;
                if (string.Compare(input, index, parseSettings.separator, 0, parseSettings.separator.Length) != 0)
                {
                    yield break;
                }
                index += parseSettings.separator.Length;
            }
        }
    }
}