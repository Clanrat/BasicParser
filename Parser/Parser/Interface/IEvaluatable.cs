using Parser.Enums;

namespace Parser.Interface
{
    public interface IEvaluatable<T>
    {
        string Symbol { get; }

        int Precedence { get; }

        Associativity Associativity { get;}

        int InputArgs { get; }

        bool SpecialUnary { get; }

        T Evaluate(params T[] args);
    }
}