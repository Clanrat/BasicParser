using System;

namespace Parser.Enums
{

    
    [Flags]
    public enum Associativity
    {
        R = 0, // Right assosiative
        L = 1, // Left Assosiative
        B = 2 // Interchangeable
    }
}