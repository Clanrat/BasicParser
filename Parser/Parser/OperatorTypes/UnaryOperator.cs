using System;
using Parser.Enums;

namespace Parser.OperatorTypes
{
    public class UnaryOperator<T> : BaseOperator<T>
    {
        private readonly Func<T, T> _func;  
        public override int InputArgs { get; }
        public override bool SpecialUnary { get; }

        protected override Func<T, T> UnaryFunc => _func;

        protected override T UseOperator(T[] args)
        {
            return _func(args[0]);
        }

        public UnaryOperator(string symbol, int precedence, Associativity associativity, Func<T, T> function): base(symbol, precedence, associativity)
        {
            _func = function;

            SpecialUnary = false; //It's always unary

            InputArgs = 1;
        }
    }
}