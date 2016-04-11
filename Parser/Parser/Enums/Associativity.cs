using System;

namespace Parser.Enums
{
    [Flags]
    public enum Associativity
    {
        R = 0,
        L = 1,
        B = 2
    }
}