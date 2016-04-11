using System;
using System.Collections.Generic;
using Parser.Converter;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes;

namespace Parser.FunctionParser
{
    public static class DefaultOperators
    {
        public static OperatorCollection Operators = new OperatorCollection(new List<IOperator>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b),
            new Operator<double>("^", 2, Associativity.L, Math.Pow),
            new UnaryOperator<double>("sin", 1, Associativity.L, Math.Sin),
            new UnaryOperator<double>("cos", 1, Associativity.L, Math.Cos),
            new UnaryOperator<double>("tan", 1, Associativity.L, Math.Tan),
            new UnaryOperator<double>("asin", 1, Associativity.L, Math.Asin),
            new UnaryOperator<double>("acos", 1, Associativity.L, Math.Acos),
            new UnaryOperator<double>("atan", 1, Associativity.L, Math.Atan)
        }); 


    }
}