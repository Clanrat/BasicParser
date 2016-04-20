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
        public static EvaluatableCollection<double> Evaluatables = new EvaluatableCollection<double>(new List<IEvaluatable<double>>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b),
            new Operator<double>("^", 2, Associativity.L, Math.Pow),
            new Function<double>("sin", 3, Math.Sin),
            new Function<double>("cos", 3, Math.Cos),
            new Function<double>("tan", 3, Math.Tan),
            new Function<double>("asin", 3, Math.Asin),
            new Function<double>("acos", 3, Math.Acos),
            new Function<double>("atan", 3, Math.Atan),
            new Function<double>("f",3,(a, b) => a - b)
        }); 


    }
}