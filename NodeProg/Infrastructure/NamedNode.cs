using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeProg
{
    public class NamedNode : Node
    {
        public NamedNode(IDictionary<string, Node> entries)
        {
            Entries = entries;
        }
        public NamedNode()
        {
            Entries = new Dictionary<string, Node>();
        }        
        IDictionary<string, Node> Entries { get; }

        public override string ToString()
        {           
            StringBuilder temp = new StringBuilder("{");
            bool isFirst = true;
            foreach (var entry in Entries)
            {
                if (isFirst)
                    isFirst = false;
                else
                    temp.Append(", ");
                temp.Append(entry.Key + " = " + entry.Value.ToString());
            }
            temp.Append("}");
            return temp.ToString();
        }

        public override bool Equals(object obj)
        {
            var objEntries = (obj as NamedNode)?.Entries;

            if (objEntries == null)
                return false;

            using (var entriesEnumerator = Entries.OrderBy(result => result.Key).GetEnumerator())
            using (var objEntriesEnumerator = objEntries.OrderBy(result => result.Key).GetEnumerator())
                while (true)
                {
                    if (!entriesEnumerator.MoveNext())
                        return !objEntriesEnumerator.MoveNext();
                    if (!objEntriesEnumerator.MoveNext())
                        return false;
                    if (!(entriesEnumerator.Current.Equals(objEntriesEnumerator.Current)))
                        return false;
                }
        }
        public static NodeParseResult? TryParse(int index, string input, NodeParseSettings parseSettings)
        {
            if (string.Compare(input, index, parseSettings.namedOpenMarker, 0, parseSettings.namedOpenMarker.Length) == 0)
            {
                var listResults = ParseNodesInternalNamedNode(index + parseSettings.namedOpenMarker.Length, input, parseSettings).ToList();

                var lastResult = listResults.LastOrDefault();
                var nextIndex = lastResult.Node != null ? lastResult.StartPosition + lastResult.Length : index + parseSettings.openMarker.Length;
                if (string.Compare(input, nextIndex, parseSettings.namedCloseMarker, 0, parseSettings.namedCloseMarker.Length) != 0)
                    throw new FormatException("namedCloseMarker not found.");
                var entries = listResults.ToDictionary(result => result.Key, result => result.Node);
                var listNamedNode = new NamedNode(entries);
                return new NodeParseResult
                {
                    Node = listNamedNode,
                    StartPosition = index,
                    Length = nextIndex + parseSettings.namedCloseMarker.Length - index
                };
            }
            return null;
        }
        public static IEnumerable<NamedNodeParseResult> ParseNodesInternalNamedNode(int index, string input, NodeParseSettings parseSettings)
        {
            if (string.Compare(input, index, parseSettings.namedCloseMarker, 0, parseSettings.namedCloseMarker.Length) == 0)
            {
                yield break;
            }

            while (true)
            {
                var namedAssignMarkerIndex = input.IndexOf(parseSettings.namedAssignMarker, index);
                if (namedAssignMarkerIndex < 0)
                    throw new FormatException("namedAssignMarker not found.");
                var key = input.Substring(index, namedAssignMarkerIndex - index);
                var nodeParseResult = TryParseNodeInternal(namedAssignMarkerIndex + parseSettings.namedAssignMarker.Length, input, parseSettings);
                var length = nodeParseResult.Value.StartPosition + nodeParseResult.Value.Length - index;
                yield return new NamedNodeParseResult
                {
                    Key = key,
                    Node = nodeParseResult.Value.Node,
                    StartPosition = index,
                    Length = length
                };

                index += length;
                if (string.Compare(input, index, parseSettings.separator, 0, parseSettings.separator.Length) != 0)
                {
                    yield break;
                }

                index += parseSettings.separator.Length;

            }
        }
    }
}