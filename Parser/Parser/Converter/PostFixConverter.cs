using System.Collections.Generic;
using System.Text.RegularExpressions;
using Parser.Collections;
using Parser.Converter.Exceptions;
using Parser.Converter.Helpers;
using Parser.Enums;
using Parser.Interface;

namespace Parser.Converter
{

    /*
    Uses the shunting yard algorithm:
    https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    Some lookahead as well to support Evaluatables with multiple 
    characters and to correctly parse numbers with multiple digits 
    and with decimal points
   
    Returns the result of the convertion as a string with each token separated by a space
    
    Usage:

    var converter = new PostFixConverter(Evaluatables);
    var result = converter.Convert("5+5");
    Console.PrintLine(result);

    Output:
    5 5 +
    */
    public class PostFixConverter : IPostFixConverter
    {
        private char[] Separators { get;}
        private Stack<string> Hold { get; } = new Stack<string>();
        private EvaluatableCollection<double> Evaluatables { get;}
        public PostFixConverter(EvaluatableCollection<double> evaluatables, string decimalSeparators=".")
        {
            Evaluatables = evaluatables;
            Separators = decimalSeparators.ToCharArray();
        }

        public List<string> Convert(string input)
        {
            var inputHelper = new InputHelper<double>(Evaluatables);
            input = inputHelper.PreProcessInput(input);
            
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
                    //Checks for operator, returns a stack of possible Evaluatables, the top member of the token stack will be the one used
                    var operatorTokens = CheckIfOperator(partialToken, inputQueue); 

                    
                    if (operatorTokens.Count != 0)
                    {
                        var operatorToken = operatorTokens.Pop();
                        QueueHelper.RemoveFromQueue(operatorToken.Length - 1, inputQueue);
                      
                        FoundOperator(operatorToken, result); //Push and pop Evaluatables from the holding stack
                    }
                    else
                    {
                        FoundSpecialChar(partialToken, result); //If it doesn't find an operator it's most likely a parenthesis
                    }
                }

            } while (inputQueue.Count != 0);

            while (Hold.Count != 0) //Pop the remaining Evaluatables to the output list
            {
                var item = Hold.Pop();
                if (!Evaluatables.OperatorExists(item)) 
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
                    Pop Evaluatables from the stack until a matching parenthesis is found
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

                case ',':
                    var topOfstack = Hold.Peek();
                    while (topOfstack != "(")
                    {
                        if (Hold.Count != 0)
                        {
                            result.Add(Hold.Pop());
                            topOfstack = Hold.Peek();
                        }
                        else
                        {
                            throw new MissMatchedParenthesisException();
                        }
                    }

                    break;
                default:
                    throw new UnexpectedOperatorException($"Unexpected Operator {partialToken}");
            }
        }

        /*
        Checks if the current token is part of a larger operator name 
        returns a stack of matched Evaluatables with the one with the longest name on the top
        Looks ahead in the inputQueue to find the entire operator
        */
        private Stack<string> CheckIfOperator(char partialToken, IEnumerable<char> inputQueue) 
        {
            var matchedOperators = new Stack<string>();
            var operatorToken = partialToken.ToString();
            foreach (var token in inputQueue)
            {
                if (Evaluatables.OperatorExists(operatorToken))
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
        Pushes the current operator token to the stack and pops Evaluatables from the stack
        */
        private void FoundOperator(string token, List<string> result)
        {
            var op = Evaluatables[token];
            if (Hold.Count != 0)
            {                
                while (Hold.Count != 0)
                {
                    var topOfStack = Hold.Peek();
                    if (!Evaluatables.OperatorExists(topOfStack))
                        break;
                    var operatorTop = Evaluatables[topOfStack];
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