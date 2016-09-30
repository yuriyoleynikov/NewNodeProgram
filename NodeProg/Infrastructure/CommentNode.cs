using System;

namespace NodeProg
{
    public class CommentNode : Node
    {
        public CommentNode(string str)
        {
            Comment = str;
        }

        public string Comment { get; }
        public override string ToString() => "/*" + Comment + "*/";
        public override bool Equals(object obj) => Comment == (obj as CommentNode)?.Comment;

        public static NodeParseResult? TryParse(int index, string input, string commentOpenMarker, string commentCloseMarker)
        {
            if (string.Compare(input, index, commentOpenMarker, 0, commentOpenMarker.Length) == 0)
            {
                var commentCloseMarkerIndex = input.IndexOf(commentCloseMarker, index + commentOpenMarker.Length);
                if (commentCloseMarkerIndex < 0)
                    throw new FormatException("commentCloseMarker not found.");
                var commetLength = commentCloseMarkerIndex - index - commentOpenMarker.Length;
                var commentNode = new CommentNode(input.Substring(index + commentOpenMarker.Length, commetLength));

                return new NodeParseResult
                {
                    Node = commentNode,
                    StartPosition = index,
                    Length = commetLength + commentOpenMarker.Length + commentCloseMarker.Length
                };
            }
            return null;
        }
    }
}