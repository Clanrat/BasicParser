using System.Collections.Generic;
using System.Text.RegularExpressions;
using Parser.Converter.Exceptions;
using Parser.Converter.Helpers;
using Parser.Enums;
using Parser.Interface;

namespace Parser.Converter
{

    /*
    Uses the shunting yard algorithm:
    https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    Some lookahead as well to support operators with multiple 
    characters and to correctly parse numbers with multiple digits 
    and with decimal points
   
    Returns the result of the convertion as a string with each token separated by a space
    
    Usage:

    var converter = new PostFixConverter(operators);
    var result = converter.Convert("5+5");
    Console.PrintLine(result);

    Output:
    5 5 +
    */
    public class PostFixConverter : IPostFixConverter
    {
        private char[] Separators { get;}
        private Stack<string> Hold { get; } = new Stack<string>();
        private OperatorCollection<double> Operators { get;}
        public PostFixConverter(OperatorCollection<double> operators, string decimalSeparators=".,")
        {
            Operators = operators;
            Separators = decimalSeparators.ToCharArray();
        }

        public List<string> Convert(string input)
        {
            input = Regex.Replace(input, @"\s+", "");
            return InternalConvert(input);
        }

        private List<string> InternalConvert(string input)
        {
            var result = new List<string>();

            var inputQueue = new Queue<char>(input.ToCharArray());

            do
            {
                var partialToken = inputQueue.Dequeue(); //Get the next in line to be parsed

                var numberToken = CheckIfNumber(partialToken, inputQueue); //Checks if number, empty string if not
                if (numberToken != "")
                {
                    QueueHelper.RemoveFromQueue(numberToken.Length - 1, inputQueue); //Remove the items that have already been parsed from the input queue
                    result.Add(numberToken);
                }
                else
                {
                    //Checks for operator, returns a stack of possible operators, the top member of the token stack will be the one used
                    var operatorTokens = CheckIfOperator(partialToken, inputQueue); 

                    
                    if (operatorTokens.Count != 0)
                    {
                        var operatorToken = operatorTokens.Pop();
                        QueueHelper.RemoveFromQueue(operatorToken.Length - 1, inputQueue);
                      
                        FoundOperator(operatorToken, result); //Push and pop operators from the holding stack
                    }
                    else
                    {
                        FoundSpecialChar(partialToken, result); //If it doesn't find an operator it's most likely a parenthesis
                    }
                }

            } while (inputQueue.Count != 0);

            while (Hold.Count != 0) //Pop the remaining operators to the output list
            {
                var item = Hold.Pop();
                if (!Operators.OperatorExists(item)) 
                    throw new MissMatchedParenthesisException();

                result.Add(item);
            }
            return result;
        }

        private void FoundSpecialChar(char partialToken, List<string> result)
        {
            switch (partialToken)
            {
                case '(': 
                    Hold.Push(partialToken.ToString());
                    return;

                case ')':
                    /*
                    Pop operators from the stack until a matching parenthesis is found
                    */

                    var foundLeftParenthesis = false; 
                    
                    do
                    {
                        var topOfStack = Hold.Pop();
                        if (topOfStack == "(")
                        {
                            foundLeftParenthesis = true;
                            break;
                        }

                        result.Add(topOfStack);
                    } while (Hold.Count != 0);

                    if (!foundLeftParenthesis)
                        throw new MissMatchedParenthesisException();

                    break;
                default:
                    throw new UnexpectedOperatorException($"Unexpected Operator {partialToken}");
            }
        }

        /*
        Checks if the current token is part of a larger operator name 
        returns a stack of matched operators with the one with the longest name on the top
        Looks ahead in the inputQueue to find the entire operator
        */
        private Stack<string> CheckIfOperator(char partialToken, IEnumerable<char> inputQueue) 
        {
            var matchedOperators = new Stack<string>();
            var operatorToken = partialToken.ToString();
            foreach (var token in inputQueue)
            {
                if (Operators.OperatorExists(operatorToken))
                    matchedOperators.Push(operatorToken);

                if (!TokenHelper.IsPartialOperator(token, Separators))
                {
                    break;
                }
                operatorToken += token;
            }
            return matchedOperators;
        }

        /*
        Pushes the current operator token to the stack and pops operators from the stack
        */
        private void FoundOperator(string token, List<string> result)
        {
            var op = Operators[token];
            if (Hold.Count != 0)
            {                
                while (Hold.Count != 0)
                {
                    var topOfStack = Hold.Peek();
                    if (!Operators.OperatorExists(topOfStack))
                        break;
                    var operatorTop = Operators[topOfStack];
                    if ((OperatorHelper.CheckAssociativity(op, Associativity.L) && op.Precedence <= operatorTop.Precedence)
                        || OperatorHelper.CheckAssociativity(op, Associativity.R) && op.Precedence < operatorTop.Precedence)
                        result.Add(Hold.Pop());
                    else
                    {
                        break;
                    }
                    
                }                
            }
            Hold.Push(token);
        }

        /*
        Looks ahead in the input queue to parse an entire number
        */
        private string CheckIfNumber(char partialToken, IEnumerable<char> inputQueue)
        {
            var numberToken = partialToken.ToString();
            if (!(char.IsDigit(partialToken) || partialToken == '.'))
            {
                return "";
            }
            
            foreach (var token in inputQueue)
            {
                var decimalpointExists = numberToken.Contains(".");
                if (char.IsDigit(token))
                    numberToken += token;
                else if (token == '.' && !decimalpointExists)
                    numberToken += token;
                else if(token == '.' && decimalpointExists)
                    throw new InvalidFormatException("Too many decimal points");
                else
                    return numberToken;
            }

            return numberToken;
        }
    }
}