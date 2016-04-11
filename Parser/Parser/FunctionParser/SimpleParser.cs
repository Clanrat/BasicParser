using System;
using System.Collections.Generic;
using Parser.Collections;
using Parser.Converter;
using Parser.Evaluator;
using Parser.Interface;

namespace Parser.FunctionParser
{
    public class SimpleParser
    {
        private IEvaluator<double> Evaluator { get; }
        private IPostFixConverter Converter { get; }

        public SimpleParser(IEvaluator<double> evaluator, IPostFixConverter converter)
        {
            Evaluator = evaluator;
            Converter = converter;
        }

        public SimpleParser(OperatorCollection<double> ops)
        {
            Evaluator = new PostFixEvaluator(ops);
            Converter = new PostFixConverter(ops);
        }

        public SimpleParser(List<IOperator<double>> ops)
        {
            Evaluator = new PostFixEvaluator(new OperatorCollection<double>(ops));
            Converter = new PostFixConverter(new OperatorCollection<double>(ops));
        }

        public SimpleParser()
        {
            var operators = DefaultOperators.Operators;
            Evaluator = new PostFixEvaluator(operators);
            Converter = new PostFixConverter(operators);
        }

        public double Parse(string input)
        {

            var convertedInput = Converter.Convert(input);
            var result = Evaluator.Eval(string.Join(" ",convertedInput));

            return result;
        }

        public double Parse(string input, params string[] args)
        {
            if (args.Length%2 != 0)
            {
                throw new ArgumentException("Missing input argument");
            }

            for (var index = 0; index < args.Length; index += 2)
            {
                input = input.Replace(args[index], args[index + 1]);
            }

            return Parse(input);
        }
    }
}