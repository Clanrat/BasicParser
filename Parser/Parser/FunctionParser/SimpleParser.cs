using System.Collections.Generic;
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

        public SimpleParser(OperatorCollection ops)
        {
            Evaluator = new PostFixEvaluator(ops);
            Converter = new PostFixConverter(ops);
        }

        public SimpleParser(List<IOperator> ops)
        {
            Evaluator = new PostFixEvaluator(new OperatorCollection(ops));
            Converter = new PostFixConverter(new OperatorCollection(ops));
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
            var result = Evaluator.Eval(convertedInput);

            return result;
        }
    }
}