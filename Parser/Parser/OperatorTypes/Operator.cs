using System;
using Parser.Enums;

namespace Parser.OperatorTypes
{
    public class Operator<T> : BaseOperator<T>
    {
        private readonly Func<T, T, T> _func; 
        private readonly Func<T, T> _unFunc;
        public override int InputArgs { get; }
        public override bool SpecialUnary { get; }
        protected override Func<T, T> UnaryFunc
        {
            get
            {
                if(!SpecialUnary)
                    throw new ArgumentException("Trying to use operator in unary function but operator cannot be unary");
                return _unFunc;
            }
        }
        public Operator(string symbol, int precedence, Associativity associativity, Func<T, T, T> function, bool specialUnary=false, Func<T, T> unaryFunc=null) : base(symbol, precedence, associativity)
        {

            SpecialUnary = specialUnary;

            if(specialUnary && unaryFunc == null)
                throw new ArgumentException("Expected A special unary function");

            _unFunc = unaryFunc;

            _func = (arg1, arg2) => Associativity == Associativity.L ? function(arg2, arg1) : function(arg1, arg2);

            InputArgs = 2;
        }

        protected override T UseOperator(T[] args)
        {
            return _func(args[0], args[1]);
        }

    }
}