using System;
using System.Collections.Generic;
using Parser.Collections;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes;
using Parser.OperatorTypes.BaseTypes.Operator;

namespace Parser.FunctionParser
{
    public static class DefaultOperators
    {
        public static OperatorCollection<double> Operators = new OperatorCollection<double>(new List<IEvaluatable<double>>
        {
            new Operator2Args<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator2Args<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator2Args<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator2Args<double>("/", 2, Associativity.L, (a, b) => a / b),
            new Operator2Args<double>("^", 2, Associativity.L, Math.Pow),
            new Operator1Arg<double>("sin", 3, Associativity.L, Math.Sin),
            new Operator1Arg<double>("cos", 3, Associativity.L, Math.Cos),
            new Operator1Arg<double>("tan", 3, Associativity.L, Math.Tan),
            new Operator1Arg<double>("asin", 3, Associativity.L, Math.Asin),
            new Operator1Arg<double>("acos", 3, Associativity.L, Math.Acos),
            new Operator1Arg<double>("atan", 3, Associativity.L, Math.Atan)
        }); 


    }
}