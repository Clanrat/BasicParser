using System;
using System.Runtime.InteropServices;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes.BaseTypes.Operator;

namespace Parser.OperatorTypes
{
    public class Operator<T> : IEvaluatable<T>
    {
        private readonly IEvaluatable<T> _op;
        public string Symbol => _op.Symbol;
        public int Precedence => _op.Precedence;
        public Associativity Associativity => _op.Associativity;
        public int InputArgs => _op.InputArgs;
        public bool SpecialUnary => _op.SpecialUnary;
        public T Evaluate(params T[] args)
        {
            return _op.Evaluate(args);
        }

        public Operator(IEvaluatable<T> op)
        {
            _op = op;
        }

        public Operator(string symbol, int precedence, Associativity associativity, Func<T, T> function)
        {
            _op = new Operator1Arg<T>(symbol, precedence, associativity, function);
        }
        public Operator(EvaluatableParamters p, Func<T, T> function)
        {
            _op = new Operator1Arg<T>(p, function);
        }
        public Operator(string symbol, int precedence, Associativity associativity, Func<T,T,T> function, bool specialUnary = false, Func<T, T> unaryFunc = null)
        {
            _op = new Operator2Args<T>(symbol, precedence, associativity, function, specialUnary, unaryFunc);
        }
        public Operator(EvaluatableParamters p, Func<T, T, T> function, Func<T, T> unaryFunc = null)
        {
            _op = new Operator2Args<T>(p, function, unaryFunc);
        }

    }
}