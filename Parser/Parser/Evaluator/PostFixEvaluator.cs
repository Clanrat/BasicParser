using System.Collections.Generic;
using Parser.Converter;

namespace Parser.Evaluator
{
    public class PostFixEvaluator
    {
        public OperatorCollection Ops { get; }

        private Stack<double> _output;

        public PostFixEvaluator(OperatorCollection ops)
        {
            Ops = ops;
        }

        public double Eval(string input)
        {


            return 0;
        }
    }
}