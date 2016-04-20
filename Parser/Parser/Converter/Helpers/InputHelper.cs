using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Parser.Collections;

namespace Parser.Converter.Helpers
{
    /*
    Class to modify the input for the PostFixConverter so it can be converted more easily 
    Replaces unary operators inside function arguments with (0 operator value) and removes spaces
             
    */
    public class InputHelper<T>
    {

        private string _ops = "(,)"; //Have to recognize function separators (really shouldn't hardcode it here...)
        private string _unOps = "";

        private readonly EvaluatableCollection<T> _evaluatables; 

        public InputHelper(EvaluatableCollection<T> evaluatables)
        {
            _evaluatables = evaluatables;
            GenerateEvaluatablePatterns(_evaluatables);
        } 

        public string PreProcessInput(string input)
        {
            input = ReplaceSpaces(input);
            input = ReplaceUnaryOperators(input);
            return input;
        }

        private static string ReplaceSpaces(string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        private string ReplaceUnaryOperators(string input)
        {
            const string unaryOpReplacement = "(0{0}{1})";
            var pattern = GenerateRegexPattern();
            var unaryOperatorMatches = Regex.Matches(input, @pattern);
            var operatorMatches = Regex.Matches(input,@_ops);
            var inpBuilder = new StringBuilder(input);

            var addedCharacters = 0;
            foreach (Match match in unaryOperatorMatches)
            {
                var nextOperatorIndex = 0;
                foreach (Match opMatch in operatorMatches)
                {
                    if (opMatch.Index <= match.Index) continue;

                    nextOperatorIndex = opMatch.Index;
                    break;
                }
                if (nextOperatorIndex == 0)
                {
                    nextOperatorIndex = input.Length;
                }

                var behindUnaryOperatorStartingIndex = (match.Index) + match.Length;
                var distanceToNextOperator = nextOperatorIndex - behindUnaryOperatorStartingIndex;
                
                var behindUnaryOperator = input.Substring(behindUnaryOperatorStartingIndex, distanceToNextOperator);

                inpBuilder.Remove(match.Index + addedCharacters, match.Length + behindUnaryOperator.Length);
                inpBuilder.Insert(match.Index + addedCharacters, string.Format(unaryOpReplacement, match.Value, behindUnaryOperator));

                addedCharacters += 3;
            }

            return inpBuilder.ToString();
        }

        private string GenerateRegexPattern()
        {
            const string pattern = "(?<={0})({1})";  
            return string.Format(pattern, _ops, _unOps);
        }

        private void GenerateEvaluatablePatterns(EvaluatableCollection<T> evaluatables)
        {
            foreach (var kvp in evaluatables.Where(kvp => kvp.Value.SpecialUnary))
            {
                _unOps += $"({Regex.Escape(kvp.Key)})|";
            }
            _unOps = _unOps.TrimEnd('|');
        }
    }
}