using System;
using System.Linq;
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


        protected override T ValidateAndExecute(T[] args)
        {
            if (args.Length > InputArgs)
                throw new ArgumentException($"Too many arguments for function {Symbol}");
            if (args.Length == 1 && SpecialUnary)
            {
                return UnaryFunc(args[0]);
            }

            if (args.Length == InputArgs)
            {
                return UseOperator(args.Reverse().ToArray()); // arguments get pulled out in reverse from the stack, they need to be reversed for it to work correctly
            }

            throw new ArgumentException($"Too few arguments for function {Symbol}");
        }


        protected abstract override T UseOperator(T[] args);
    }
}