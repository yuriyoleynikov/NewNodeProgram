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

            using (var enumerator = input.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return null;

                if (allowLeadingWhitespace)
                    while (char.IsWhiteSpace(enumerator.Current))
                        if (!enumerator.MoveNext())
                            return null;

                var minus = 1;
                if (enumerator.Current == '+' || enumerator.Current == '-')
                {
                    if (enumerator.Current == '-')
                        minus = -1;
                    if (!enumerator.MoveNext())
                        return null;
                }

                long value = 0;
                while (char.IsDigit(enumerator.Current))
                {
                    try
                    {
                        value = checked(value * 10 + minus * (enumerator.Current - '0'));
                    }
                    catch (OverflowException)
                    {
                        return null;
                    }
                    if (!enumerator.MoveNext())
                        return value;
                }

                if (allowTrailingWhitespace)
                    while (char.IsWhiteSpace(enumerator.Current))
                        if (!enumerator.MoveNext())
                            return value;

                return null;
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

            return ToEnumerableInternal(input, startIndex, length);
        }

        private static IEnumerable<char> ToEnumerableInternal(this string input, int startIndex, int length)
        {
            for (int index = startIndex; index < startIndex + length; index++)
            {
                yield return input[index];
            }
        }
    }
}
