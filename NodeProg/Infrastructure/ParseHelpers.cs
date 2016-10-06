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
                throw new FormatException("input.Length < startIndex + length");

            if (length == 0)
                return null;

            var position = startIndex;
            var current = input[position];

            if (allowLeadingWhitespace)
            {
                while (position < startIndex + length)
                {
                    current = input[position];
                    if (current != ' ' && current != '\t')
                        break;
                    position++;
                }
            }

            if (position == startIndex + length)
                return null;

            current = input[position];

            var minusFlag = false;

            if (current == '+' || current == '-')
            {
                if (current == '-')
                    minusFlag = true;
                position++;
            }

            if (position == startIndex + length)
                return null;

            current = input[position];

            long value = 0;

            while (position < startIndex + length)
            {
                current = input[position];

                if (current < '0' || current > '9')
                {
                    break;
                }

                try
                {
                    if (!minusFlag)
                    {
                        checked
                        {

                            value = value * 10 + (current - '0');
                        }
                    }
                    else
                    {
                        checked
                        {
                            value = value * 10 - (current - '0');
                        }
                    }
                }
                catch
                {
                    return null;
                }
                position++;
            }

            if (position == startIndex + length)
                return value;

            current = input[position];


            if (allowTrailingWhitespace)
            {
                while (position < startIndex + length)
                {
                    current = input[position];
                    if (current != ' ' && current != '\t')
                        return null;
                    position++;
                }
            }
            else
                return null;

            return value;
        }
    }
}
