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
                throw new FormatException("input == null");
            if (startIndex < 0)
                throw new FormatException("startIndex < 0");
            if (length < 0)
                throw new FormatException("length < 0");
            if (startIndex > input.Length)
                throw new FormatException("startIndex > input.Length");
            if (length > input.Length)
                throw new FormatException("length > input.Length");

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
            else
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
            else
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
            else
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
            {
                return null;
            }

            return value;
        }
    }
}
