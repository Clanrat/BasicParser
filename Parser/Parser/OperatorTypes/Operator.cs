using System;
using System.Threading;
using Parser.Converter;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes
{
    public class Operator<T> : BaseOperator<T>
    {
        private readonly Func<T, T> _unFunc;
        public override int InputArgs { get; }
        public override bool SpecialUnary { get; }

        public override T Evaluate(params T[] args)
        {
            if(args.Length > InputArgs)
                throw new ArgumentException($"Too many arguments for operator {Symbol}");



            if (args.Length == 1 && SpecialUnary)
            {
                return UnaryFunc(args[0]);
            }

            if(args.Length == InputArgs)
            {
                return Associativity == Associativity.L ? Function(args[1], args[0]) : Function(args[0], args[1]);
            }

            throw new ArgumentException($"Too few arguments for operator {Symbol}");
            
        }

        protected override Func<T, T> UnaryFunc
        {
            get
            {
                if(!SpecialUnary)
                    throw new ArgumentException("Trying to use operator in unary function but operator cannot be unary");
                return _unFunc;
            }
        }

        private Func<T, T, T> Function { get; }

        public Operator(string symbol, int precedence, Associativity associativity, Func<T, T, T> function, bool specialUnary=false, Func<T, T> unaryFunc=null) : base(symbol, precedence, associativity)
        {

            Function = function;

            SpecialUnary = specialUnary;

            if(specialUnary && unaryFunc == null)
                throw new ArgumentException("Expected A special unary function");

            _unFunc = unaryFunc;

            InputArgs = 2;
        }

       
    }
}