using System;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes.BaseTypes
{
    public abstract class BaseEvaluatable<T> : IEvaluatable<T>
    {
        private readonly Func<T, T> _unFunc;
        public string Symbol { get; }
        public int Precedence { get; }
        public Associativity Associativity { get; }
        public int InputArgs { get; }
        public bool SpecialUnary { get; }
        private Func<T, T> UnaryFunc
        {
            get
            {
                if (!SpecialUnary)
                    throw new ArgumentException($"Trying to use evaluatable {Symbol} in unary function but evaluatable cannot be unary");
                return _unFunc;
                
            }
        }
        public T Evaluate(params T[] args)
        {
            return ValidateAndExecute(args);
        }
        private T ValidateAndExecute(T[] args)
        {
            if (args.Length > InputArgs)
                throw new ArgumentException($"Too many arguments for evaluatable {Symbol}");
            if (args.Length == 1 && SpecialUnary)
            {
                return UnaryFunc(args[0]);
            }

            if (args.Length == InputArgs)
            {
                return UseOperator(args);
            }

            throw new ArgumentException($"Too few arguments for evaluatable {Symbol}");
        }
        protected BaseEvaluatable(string symbol, int precedence, Associativity associativity, int inputArgs, bool specialUnary, Func<T, T> unaryFunc = null)
        {
            Symbol = symbol;
            if (precedence <= 0)
                throw new ArgumentException("Precedence must be above 0");

            Precedence = precedence;
            Associativity = associativity;

            InputArgs = inputArgs;
            SpecialUnary = specialUnary;

            if (SpecialUnary && unaryFunc == null)
            {
                throw new ArgumentException($"Evaluatable {Symbol} is special unary but missing a special unary function");
            }
            _unFunc = unaryFunc;

        }
        protected BaseEvaluatable(EvaluatableParamters p, Func<T, T> unaryFunc = null) : this(p.Symbol, p.Precedence, p.Associativity, p.InputArgs, p.SpecialUnary, unaryFunc)
        {
            
        } 
        protected abstract T UseOperator(T[] args);
    }
}