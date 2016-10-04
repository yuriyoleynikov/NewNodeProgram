using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NodeProg
{
    public partial class NumberNode : Node
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

            var value = TryParseInt64(input, index, numberLength);
            if (value != null)
                return new NodeParseResult { Node = new NumberNode(value.Value), StartPosition = index, Length = numberLength };
            return null;
        }        
    }
}
