using System;
using Parser.Converter;
using Parser.Enums;
using Parser.Interface;

namespace Parser.OperatorTypes
{
    public class UnaryOperator<T> : BaseOperator<T>
    {
        private Func<T, T> _func; 
        public override int InputArgs { get; }
        public override bool SpecialUnary { get; }

        public override Func<T, T> UnaryFunc => _func;

        public Func<T, T> Function => _func;

        public UnaryOperator(string symbol, int precedence, Associativity associativity, Func<T, T> function): base(symbol, precedence, associativity)
        {
            _func = function;

            SpecialUnary = false; //It's always unary

            InputArgs = 1;
        }
    }
}