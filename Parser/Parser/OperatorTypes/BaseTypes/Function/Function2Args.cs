using System;
using Parser.Enums;

namespace Parser.OperatorTypes.BaseTypes.Function
{
    public class Function2Args<T> : BaseFunction<T>
    {
        private readonly Func<T, T, T> _func;
        public Function2Args(string symbol, int precedence, Func<T, T, T> function) : base(symbol, precedence, 2)
        {
            _func = function;
        }

        public Function2Args(EvaluatableParamters p, Func<T, T, T> function) : base(p)
        {
            _func = function;
        }

        protected override T UseOperator(T[] args)
        {
            return _func(args[0], args[1]);
        }
    }
}