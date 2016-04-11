using Parser.Converter;
using Parser.Enums;

namespace Parser.Interface
{
    public interface IOperator
    {
        string Symbol { get; }

        int Precedence { get; }

        Associativity Associativity { get;}

        int InputArgs { get; }

        bool SpecialUnary { get; }
    }
}