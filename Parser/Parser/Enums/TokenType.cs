using System;

namespace Parser.Enums
{
    [Flags]
    public enum TokenType
    {
        Number = 1,
        Literal = 2,
        Operator = 3,
        Function = 4
    }
}