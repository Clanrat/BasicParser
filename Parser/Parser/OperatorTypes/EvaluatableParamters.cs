using Parser.Enums;

namespace Parser.OperatorTypes
{
    public struct EvaluatableParamters
    {
        public string Symbol { get; set; }
        public int Precedence { get; set; }
        public Associativity Associativity { get; set;}
        public int InputArgs { get; set; }
        public bool SpecialUnary { get; set; }
        public EvaluatableParamters(string symbol, int precedence, Associativity associativity, int inputArgs,
            bool specialUnary = false)
        {
            Symbol = symbol;
            Precedence = precedence;
            Associativity = associativity;
            InputArgs = inputArgs;
            SpecialUnary = specialUnary;
        }
        public static EvaluatableParamters GetFunctionParameters(string symbol, int precedence, int inputArgs)
        {
            return new EvaluatableParamters(symbol, precedence, Associativity.L, inputArgs);
        }
        public static EvaluatableParamters GetOperatorParameters(string symbol, int precedence,
            Associativity associativity, int inputArgs,
            bool specialUnary = false)
        {
            return new EvaluatableParamters(symbol, precedence, associativity, inputArgs, specialUnary);
        }
    }
}