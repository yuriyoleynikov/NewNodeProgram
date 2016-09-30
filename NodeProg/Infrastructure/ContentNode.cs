using System.Collections.Generic;
using System.Linq;

namespace NodeProg
{
    public class ContentNode : Node
    {
        public ContentNode(string str)
        {
            Content = str;
        }
        public string Content { get; }
        public override string ToString() => "\"" + Content.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
        public override bool Equals(object obj) => Content == (obj as ContentNode)?.Content;
        public static NodeParseResult? Parse(int index, string input, IEnumerable<string> stopMarkers)
        {
            var stopMarkerIndex = stopMarkers
                .Select(stopMarker => (int?)input.IndexOf(stopMarker, index))
                .Where(stop => stop >= 0)
                .Min() ?? input.Length;

            var contentLength = stopMarkerIndex - index;
            var contentNode = new ContentNode(input.Substring(index, contentLength));

            return new NodeParseResult { Node = contentNode, StartPosition = index, Length = contentLength };
        }
    }
}
