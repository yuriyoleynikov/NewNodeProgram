using System;
using System.Collections;
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

            var value = TryParseInt64(input, index, numberLength);
            if (value != null)
                return new NodeParseResult { Node = new NumberNode(value.Value), StartPosition = index, Length = numberLength };
            return null;
        }
        private static long? TryParseInt64(
            string input,
            int startIndex,
            int length,
            bool allowLeadingWhitespace = true,
            bool allowTrailingWhitespace = true)
        {
            if (length == 0)
                return null;
            var trimChars = new List<char>();
            if (allowLeadingWhitespace)
                trimChars.Add(' ');
            if (allowTrailingWhitespace)
                trimChars.Add('\t');

            var digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var position = startIndex;
            var minusFlag = false;
            var value = new Int64();
            for (; position < startIndex + length;)
            {
                if (input[position] == '+' || input[position] == '-')
                {
                    if (input[position] == '-')
                        minusFlag = true;
                    position++;
                    break;
                }
                var trimFlag = false;
                foreach (var tChar in trimChars)
                {
                    if (input[position] == tChar)
                    {
                        trimFlag = true;
                        position++;
                        break;
                    }
                }
                if (trimFlag)
                    continue;
                break;
            }

            var int64s = new List<int>() { };

            for (; position < startIndex + length;)
            {
                var digitFlag = false;
                foreach (var digit in digits)
                {
                    if (input[position] == digit)
                    {
                        digitFlag = true;
                        int64s.Add(int.Parse(digit.ToString()));
                        position++;
                        break;
                    }
                }
                if (digitFlag)
                    continue;
                break;
            }

            for (; position < startIndex + length;)
            {
                var trimFlag = false;
                foreach (var tChar in trimChars)
                {
                    if (input[position] == tChar)
                    {
                        trimFlag = true;
                        position++;
                        break;
                    }
                }
                if (trimFlag)
                    continue;
                break;
            }

            if (position < startIndex + length)
                return null;

            var val1 = int64s.ToArray<int>();
            var val = new int[val1.Length];
            for (int i = 0; i < val1.Length; i++)
            {
                val[val1.Length - i - 1] = val1[i]; 
            }

            value = 0;
            for (int i = 0; i < val.Length; i++)
            {
                try
                {
                    if (!minusFlag)
                    {
                        checked
                        {
                            value = value + val[i] * (long)Math.Pow((double)10, (double)i);
                        }
                    }
                    else
                    {
                        checked
                        {
                            value = value - val[i] * (long)Math.Pow((double)10, (double)i);
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }            
            return value;
        }
    }
}
