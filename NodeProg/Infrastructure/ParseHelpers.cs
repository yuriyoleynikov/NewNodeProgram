using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProg
{
    public static class ParseHelpers
    {
        public static long? TryParseInt64(
            string input,
            int startIndex,
            int length,
            bool allowLeadingWhitespace = true,
            bool allowTrailingWhitespace = true)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startIndex < 0)
                throw new ArgumentException("Argument must not be negative", nameof(startIndex));
            if (length < 0)
                throw new ArgumentException("Argument must not be negative", nameof(length));
            if (input.Length < startIndex + length)
                throw new ArgumentException("input.Length < startIndex + length", nameof(length));

            if (length == 0)
                return null;

            var position = startIndex;
            var current = input[position];
            var endIndex = startIndex + length;

            if (allowLeadingWhitespace)
            {
                while (position < endIndex)
                {
                    current = input[position];
                    if (current != ' ' && current != '\t')
                        break;
                    position++;
                }
                if (position == endIndex)
                    return null;
                current = input[position];
            }

            var minus = 1;

            if (current == '+' || current == '-')
            {
                if (current == '-')
                    minus = -1;
                position++;
                if (position == endIndex)
                    return null;
                current = input[position];
            }

            long value = 0;

            while (position < endIndex)
            {
                current = input[position];

                if (current < '0' || current > '9')
                    break;

                try
                {
                    checked
                    {
                        value = value * 10 + (current - '0') * minus;
                    }
                }
                catch
                {
                    return null;
                }
                position++;
            }

            if (allowTrailingWhitespace)
            {
                while (position < endIndex)
                {
                    current = input[position];
                    if (current != ' ' && current != '\t')
                        return null;
                    position++;
                }
            }

            if (position == endIndex)
                return value;

            return null;
        }
    }
}
