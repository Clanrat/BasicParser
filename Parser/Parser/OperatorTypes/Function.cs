using System;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes.BaseTypes.Function;

namespace Parser.OperatorTypes
{
    public class Function<T> : IEvaluatable<T>
    {
        private readonly IEvaluatable<T> _func;
        public string Symbol => _func.Symbol;
        public int Precedence => _func.Precedence;
        public Associativity Associativity => _func.Associativity;
        public int InputArgs => _func.InputArgs;
        public bool SpecialUnary => _func.SpecialUnary;
        public T Evaluate(params T[] args)
        {
            return _func.Evaluate(args);
        }

        public Function(IEvaluatable<T> function)
        {
            _func = function;
        } 
        public Function(string symbol, int precedence, Func<T, T> function)
        {
            _func = new Function1Arg<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T, T> function)
        {
            _func = new Function1Arg<T>(p, function);
        }
        public Function(string symbol, int precedence, Func<T,T,T> function)
        {
            _func = new Function2Args<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T,T,T> function)
        {
            _func = new Function2Args<T>(p, function);
        }
        public Function(string symbol, int precedence, Func<T,T,T,T> function)
        {
            _func = new Function3Args<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T,T,T,T> function)
        {
            _func = new Function3Args<T>(p, function);
        }
        public Function(string symbol, int precedence, Func<T, T, T, T, T> function)
        {
            _func = new Function4Args<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T, T, T, T, T> function)
        {
            _func = new Function4Args<T>(p, function);
        }
        public Function(string symbol, int precedence, Func<T, T, T, T, T, T> function)
        {
            _func = new Function5Args<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T, T, T, T, T, T> function)
        {
            _func = new Function5Args<T>(p, function);
        }
        public Function(string symbol, int precedence, Func<T, T, T, T, T, T, T> function)
        {
            _func = new Function6Args<T>(symbol, precedence, function);
        }
        public Function(EvaluatableParamters p, Func<T, T, T, T, T, T, T> function)
        {
            _func = new Function6Args<T>(p, function);
        }
    }
}