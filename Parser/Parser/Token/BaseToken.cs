using Parser.Enums;
using Parser.Interface;

namespace Parser.Token
{
    public abstract class BaseToken<T> : IToken
    {
        public TokenType Type { get; }
        public T Value { get; }
    }
}