using System;
using Parser.Enums;

namespace Parser.OperatorTypes
{
    public class UnaryOperator<T> : BaseOperator<T>
    {
        public override int InputArgs { get; }
        public override bool SpecialUnary { get; }

        public override T Evaluate(params T[] args)
        {
            if(args.Length > 1)
                throw new ArgumentException($"Too many arguments for operator {Symbol}");
            return Function(args[0]);
        }

        protected override Func<T, T> UnaryFunc => Function;

        private Func<T, T> Function { get; }

        public UnaryOperator(string symbol, int precedence, Associativity associativity, Func<T, T> function): base(symbol, precedence, associativity)
        {
            Function = function;

            SpecialUnary = false; //It's always unary

            InputArgs = 1;
        }
    }
}