using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProg
{
    public partial class NumberNode : Node
    {
        private static long? TryParseInt64(
            string input,
            int startIndex,
            int length,
            bool allowLeadingWhitespace = true,
            bool allowTrailingWhitespace = true)
        {
            if (length == 0)
                return null;

            var position = startIndex;


            if (allowLeadingWhitespace)
            {
                while (position < startIndex + length)
                {
                    if (!(input[position] == ' ' || '\t' == input[position]))
                        break;
                    position++;
                }
            }

            if (position == startIndex + length)
                return null;

            var minusFlag = false;

            if (input[position] == '+' || input[position] == '-')
            {
                if (input[position] == '-')
                    minusFlag = true;
                position++;
            }

            var endPosition = position;
            position = startIndex + length - 1;
            
            if (allowTrailingWhitespace)
            {
                while (position > endPosition)
                {
                    if (!(input[position] == ' ' || '\t' == input[position]))
                        break;
                    position--;
                }
            }

            long value = 0;
            long pow = 1;

            while (position >= endPosition)
            {
                if (!(input[position] >= '0' && input[position] <= '9' ))
                {
                    return null;
                }
                
                try
                {
                    if (!minusFlag)
                    {
                        checked
                        {
                            
                            value += (long)((int)input[position] - (int)'0') * pow;
                        }
                    }
                    else
                    {
                        checked
                        {
                            value -= (long)((int)input[position] - (int)'0') * pow;
                        }
                    }
                }
                catch
                {
                    return null;
                }
                position--;
                pow *= 10;
            }
            return value;
        }
    }
}
