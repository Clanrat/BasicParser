using System.Collections.Generic;
using System.Linq;

namespace Parser.Converter.Helpers
{
    internal static class TokenHelper
    {
        /*
        Checks if the token belongs to any special characters, and therefor
        can be part of the operator name
        */
        public static bool IsPartialOperator(char token, IEnumerable<char> decimalSeparator)
        {
            return !(token == '(' || token == ')' || decimalSeparator.Contains(token) || char.IsDigit(token));
        }
    }
}