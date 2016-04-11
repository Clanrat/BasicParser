using System;
using Parser.Converter;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes
{
    public class UnaryOperator<T> : IOperator
    {
        public string Symbol { get; }

        public int Precedence { get; }

        public Associativity Associativity { get; }

        public bool Unary { get; }

        public Func<T, T> Function { get; } 

        public UnaryOperator(string symbol, int precedence, Associativity associativity, Func<T, T> function)
        {
            Symbol = symbol;

            if (precedence <= 0)
                throw new ArgumentException("Precedence must be above 0");

            Precedence = precedence;

            Associativity = associativity;

            Function = function;

            Unary = false;
        }
    }
}