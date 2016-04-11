using System;
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

        public override Func<T, T> UnaryFunc
        {
            get
            {
                if(!SpecialUnary)
                    throw new ArgumentException("Trying to use operator in unary function but operator cannot be unary");
                return _unFunc;
            }
        }

        public Func<T, T, T> Function { get; }

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