using System;

namespace Parser.Converter.Exceptions
{
    public abstract class PostFixConverterException : Exception
    {
        protected PostFixConverterException(string message) : base(message)
        {
            
        }

        protected PostFixConverterException() : base()
        {
            
        }

    }

    public class MissMatchedParenthesisException : PostFixConverterException
    {
        public MissMatchedParenthesisException(string message) : base(message)
        {
        }

        public MissMatchedParenthesisException() : base()
        {
            
        }

    }

    public class InvalidFormatException : PostFixConverterException
    {
        public InvalidFormatException(string message) : base(message)
        {
            
        }

        public InvalidFormatException() : base()
        {
            
        }
    }

    public class UnexpectedOperatorException : PostFixConverterException
    {
        public UnexpectedOperatorException(string message) : base(message)
        {
            
        }
    }
}