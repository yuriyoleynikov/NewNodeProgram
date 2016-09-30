using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeProg
{
    public class ListNode : Node
    {
        public ListNode(IEnumerable<Node> nodes)
        {
            Children = nodes.ToArray();
        }

        public ListNode(params Node[] nodes) : this((IEnumerable<Node>)nodes)
        {
        }

        public IEnumerable<Node> Children { get; private set; }
        public override string ToString()
        {
            StringBuilder temp = new StringBuilder("[");            
            bool isFirst = true;
            foreach (var child in Children)
            {
                if (isFirst)
                    isFirst = false;
                else
                    temp.Append(", ");
                temp.Append(child);
            }
            temp.Append("]");
            return temp.ToString();
        }
        public override bool Equals(object obj)
        {
            var objChildren = (obj as ListNode)?.Children;            
            if (objChildren == null)
                    return false;

            using (var childrenEnumerator = Children.GetEnumerator())
            using (var objChildrenEnumerator = objChildren.GetEnumerator())
                while (true)
                {
                    if (!childrenEnumerator.MoveNext())
                        return !objChildrenEnumerator.MoveNext();
                    if (!objChildrenEnumerator.MoveNext())
                        return false;
                    if (!childrenEnumerator.Current.Equals(objChildrenEnumerator.Current))
                        return false;
                }
        }
        public static NodeParseResult? TryParse(int index, string input, NodeParseSettings parseSettings)
        {
            if (string.Compare(input, index, parseSettings.openMarker, 0, parseSettings.openMarker.Length) == 0)
            {
                var listResults = ParseNodesInternal(index + parseSettings.openMarker.Length, input, parseSettings).ToList();

                var lastResult = listResults.LastOrDefault();
                var nextIndex = lastResult.Node != null ? lastResult.StartPosition + lastResult.Length : index + parseSettings.openMarker.Length;
                if (string.Compare(input, nextIndex, parseSettings.closeMarker, 0, parseSettings.closeMarker.Length) != 0)
                    throw new FormatException("closeMarker not found.");

                var listNode = new ListNode(listResults.Select(result => result.Node));
                return new NodeParseResult { Node = listNode, StartPosition = index, Length = nextIndex + parseSettings.closeMarker.Length - index };
            }
            return null;
        }
    }
}
