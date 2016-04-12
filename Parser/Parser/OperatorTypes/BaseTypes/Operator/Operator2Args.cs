using System;
using Parser.Enums;

namespace Parser.OperatorTypes.BaseTypes.Operator
{
    public class Operator2Args<T> : BaseEvaluatable<T>
    {
        private readonly Func<T, T, T> _func; 

        public Operator2Args(string symbol, int precedence, Associativity associativity, Func<T, T, T> function, bool specialUnary=false, Func<T, T> unaryFunc=null) : base(symbol, precedence, associativity, 2, specialUnary, unaryFunc)
        {           
            _func = (arg1, arg2) => Associativity == Associativity.L ? function(arg2, arg1) : function(arg1, arg2);   
        }

        public Operator2Args(EvaluatableParamters p, Func<T, T, T> function, Func<T, T> unaryFunc = null) : base(p, unaryFunc)
        {
            _func = (arg1, arg2) => Associativity == Associativity.L ? function(arg2, arg1) : function(arg1, arg2);
        }

        protected override T UseOperator(T[] args)
        {
            return _func(args[0], args[1]);
        }

    }
}