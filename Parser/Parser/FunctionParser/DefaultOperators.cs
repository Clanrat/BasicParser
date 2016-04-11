using System;
using System.Collections.Generic;
using Parser.Collections;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes;

namespace Parser.FunctionParser
{
    public static class DefaultOperators
    {
        public static OperatorCollection<double> Operators = new OperatorCollection<double>(new List<IOperator<double>>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b),
            new Operator<double>("^", 2, Associativity.L, Math.Pow),
            new UnaryOperator<double>("sin", 3, Associativity.L, Math.Sin),
            new UnaryOperator<double>("cos", 3, Associativity.L, Math.Cos),
            new UnaryOperator<double>("tan", 3, Associativity.L, Math.Tan),
            new UnaryOperator<double>("asin", 3, Associativity.L, Math.Asin),
            new UnaryOperator<double>("acos", 3, Associativity.L, Math.Acos),
            new UnaryOperator<double>("atan", 3, Associativity.L, Math.Atan)
        }); 


    }
}