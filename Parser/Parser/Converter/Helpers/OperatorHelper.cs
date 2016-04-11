﻿using Parser.Enums;
using Parser.Interface;

namespace Parser.Converter.Helpers
{
    internal static class OperatorHelper
    {
        public static bool CheckAssociativity(IOperator op, Associativity val)
        {
            return op.Associativity == val || op.Associativity == Associativity.B;
        }
    }
}