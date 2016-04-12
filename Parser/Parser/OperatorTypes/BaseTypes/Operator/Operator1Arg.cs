using System;
using Parser.Enums;

namespace Parser.OperatorTypes.BaseTypes.Operator
{
    public class Operator1Arg<T> : BaseEvaluatable<T>
    {
        private readonly Func<T, T> _func;  
        protected override T UseOperator(T[] args)
        {
            return _func(args[0]);
        }

        public Operator1Arg(string symbol, int precedence, Associativity associativity, Func<T, T> function): base(symbol, precedence, associativity, 1, false)
        {
            _func = function;          
        }

        public Operator1Arg(EvaluatableParamters p, Func<T, T> function) : base(p)
        {
            _func = function;
        } 
    }
}