using System;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes
{
    public abstract class BaseOperator<T> : IOperator
    {
        public string Symbol { get; }
        public int Precedence { get; }
        public Associativity Associativity { get; }
        public abstract int InputArgs { get; }
        public abstract bool SpecialUnary { get; }
        public abstract Func<T, T> UnaryFunc { get; } 

        protected BaseOperator(string symbol, int precedence, Associativity associativity)
        {

            Symbol = symbol;

            if (precedence <= 0)
                throw new ArgumentException("Precedence must be above 0");

            Precedence = precedence;

            Associativity = associativity;
        }

    }
}