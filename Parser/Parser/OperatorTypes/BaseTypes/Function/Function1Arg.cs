using System;
using Parser.Enums;

namespace Parser.OperatorTypes.BaseTypes.Function
{
    public class Function1Arg<T> : BaseFunction<T>
    {

        private readonly Func<T, T> _func; 

        public Function1Arg(string symbol, int precedence, Func<T, T> function) : base(symbol, precedence, 1)
        {
            _func = function;
        }

        public Function1Arg(EvaluatableParamters p, Func<T, T> function) : base(p)
        {
            _func = function;
        } 

        protected override T UseOperator(T[] args)
        {
            return _func(args[0]);
        }
    }
}