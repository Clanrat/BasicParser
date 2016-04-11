using System.Linq;

namespace Parser.Converter.Helpers
{
    internal static class TokenHelper
    {
        public static bool IsPartialOperator(char token, char[] decimalSeparator)
        {
            return !(token == '(' || token == ')' || decimalSeparator.Contains(token) || char.IsDigit(token));
        }
    }
}