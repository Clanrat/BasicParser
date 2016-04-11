﻿using Parser.Enums;
using Parser.Interface;

namespace Parser.Converter.Helpers
{
    public static class OperatorHelper
    {
        public static bool CheckAssociativity(IOperator op, Associativity val)
        {
            return op.Associativity == val || op.Associativity == Associativity.B;
        }
    }
}