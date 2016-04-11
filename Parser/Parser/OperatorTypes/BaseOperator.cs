using System;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes
{
    public abstract class BaseOperator<T> : IOperator<T>
    {
        public string Symbol { get; }
        public int Precedence { get; }
        public Associativity Associativity { get; }
        public abstract int InputArgs { get; }
        public abstract bool SpecialUnary { get; }
        protected abstract Func<T, T> UnaryFunc { get; }
        public T Evaluate(params T[] args)
        {
            return ValidateAndExecute(args);
        }
        private T ValidateAndExecute(T[] args)
        {
            if (args.Length > InputArgs)
                throw new ArgumentException($"Too many arguments for operator {Symbol}");
            if (args.Length == 1 && SpecialUnary)
            {
                return UnaryFunc(args[0]);
            }

            if (args.Length == InputArgs)
            {
                return UseOperator(args);
            }

            throw new ArgumentException($"Too few arguments for operator {Symbol}");
        }

        protected BaseOperator(string symbol, int precedence, Associativity associativity)
        {
            Symbol = symbol;
            if (precedence <= 0)
                throw new ArgumentException("Precedence must be above 0");
            Precedence = precedence;
            Associativity = associativity;

        }
        protected abstract T UseOperator(T[] args);
    }
}