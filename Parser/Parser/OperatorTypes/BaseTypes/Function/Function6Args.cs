﻿using System;

namespace Parser.OperatorTypes.BaseTypes.Function
{
    public class Function6Args<T> : BaseFunction<T>
    {
        private readonly Func<T, T, T, T, T, T, T> _func;
        public Function6Args(string symbol, int precedence, Func<T, T, T, T, T, T, T> function ) : base(symbol, precedence, 6)
        {
            _func = function;
        }

        public Function6Args(EvaluatableParamters p, Func<T, T, T, T, T, T, T> function) : base(p)
        {
            _func = function;
        }

        protected override T UseOperator(T[] args)
        {
            return _func(args[0], args[1], args[2], args[3], args[4], args[5]);
        }
    }
}