using System;
using System.Collections.Generic;
using System.Linq;

namespace NodeProg
{
    public class NumberNode : Node
    {
        public NumberNode(long value)
        {
            Value = value;
        }
        public long Value { get; }
        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value == (obj as NumberNode)?.Value;
        public static NodeParseResult? TryParse(int index, string input, IEnumerable<string> stopMarkers)
        {
            var stopMarkerIndex = stopMarkers
                .Select(stopMarker => (int?)input.IndexOf(stopMarker, index))
                .Where(stop => stop >= 0)
                .Min() ?? input.Length;

            var numberLength = stopMarkerIndex - index;
            var numberString = input.Substring(index, numberLength);

            long value;

            if (Int64.TryParse(numberString.Trim(), out value))
                return new NodeParseResult { Node = new NumberNode(value), StartPosition = index, Length = numberLength };
            return null;

        }
    }
}
