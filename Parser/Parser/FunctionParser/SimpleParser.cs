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
    }
}