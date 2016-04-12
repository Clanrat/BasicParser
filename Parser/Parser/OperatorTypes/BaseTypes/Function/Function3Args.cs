using System;

namespace Parser.OperatorTypes.BaseTypes.Function
{
    public class Function3Args<T>: BaseFunction<T>
    {
        private readonly Func<T, T, T, T> _func;
        public Function3Args(string symbol, int precedence, int inputArgs, Func<T, T, T, T> function) : base(symbol, precedence, inputArgs)
        {
            _func = function;
        }

        public Function3Args(EvaluatableParamters p, Func<T, T, T, T> function) : base(p)
        {
            _func = function;
        }

        protected override T UseOperator(T[] args)
        {
            return _func(args[0], args[1], args[3]);
        }
    }
}