using System;

namespace Parser.Evaluator.Exceptions
{
    public class EvaluatorException : Exception
    {
        public EvaluatorException(string message) : base(message)
        {
            
        }

        protected EvaluatorException()
        {
            
        }
    }

    public class InvalidInputException : EvaluatorException
    {
        
    }

}