using System;
using Parser.Enums;

namespace Parser.OperatorTypes.BaseTypes
{
    public abstract class BaseFunction<T> : BaseEvaluatable<T>
    {
        protected BaseFunction(string symbol, int precedence, int inputArgs) : base(symbol, precedence, Associativity.L, inputArgs, false)
        {
        }

        protected BaseFunction(EvaluatableParamters p) : base(p)
        {
        }

        protected abstract override T UseOperator(T[] args);
    }
}