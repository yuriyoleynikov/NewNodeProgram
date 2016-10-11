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

            return TryParseInt64(input.ToEnumerable(startIndex, length), allowLeadingWhitespace, allowTrailingWhitespace);
        }

        public static long? TryParseInt64(
            IEnumerable<char> input,
            bool allowLeadingWhitespace = true,
            bool allowTrailingWhitespace = true)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var iChar = input.GetEnumerator())
            {
                if (!iChar.MoveNext())
                    return null;

                if (allowLeadingWhitespace)
                    while (Char.IsWhiteSpace(iChar.Current))
                        if (!iChar.MoveNext())
                            return null;

                var minus = 1;
                if (iChar.Current == '+' || iChar.Current == '-')
                {
                    if (iChar.Current == '-')
                        minus = -1;
                    if (!iChar.MoveNext())
                        return null;
                }

                long value = 0;
                var iCharMN = true;
                do
                {
                    if (!Char.IsDigit(iChar.Current))
                        break;
                    try
                    {
                        checked
                        {
                            value = value * 10 + minus * (iChar.Current - '0');
                        }
                    }
                    catch (OverflowException)
                    {
                        return null;
                    }
                    iCharMN = iChar.MoveNext();
                } while (iCharMN);

                if (!iCharMN)
                    return value;

                if (allowTrailingWhitespace)
                {
                    do
                    {
                        if (!Char.IsWhiteSpace(iChar.Current))
                            return null;
                    } while (iChar.MoveNext());
                }
                else
                    return null;

                return value;
            }
        }

        public static IEnumerable<char> ToEnumerable(this string input, int startIndex, int length)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startIndex < 0)
                throw new ArgumentException("Argument must not be negative", nameof(startIndex));
            if (length < 0)
                throw new ArgumentException("Argument must not be negative", nameof(length));
            if (input.Length < startIndex + length)
                throw new ArgumentException("input.Length < startIndex + length", nameof(length));

            for (int index = startIndex; index < startIndex + length; index++)
            {
                yield return input[index];
            }
        }
    }
}
