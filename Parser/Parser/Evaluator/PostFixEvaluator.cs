using System;
using System.Collections.Generic;
using System.Globalization;
using Parser.Collections;
using Parser.Evaluator.Exceptions;
using Parser.Evaluator.Helpers;
using Parser.Interface;

namespace Parser.Evaluator
{
    /*
    Evaluates a space delimeted string of a mathematical function written in reverse polish notation
    */
    public class PostFixEvaluator : IEvaluator<double>
    {
        private OperatorCollection<double> Ops { get; }


        public PostFixEvaluator(OperatorCollection<double> ops)
        {
            Ops = ops;
        }

        public double Eval(string input)
        {
            var splitInput = input.Split(' ');

            return InternalEval(splitInput);
        }

        /*
        Evaluates the input in the format of an Ienumerable, 
        Iterates over the Ienumerable checking if each token is an operator. 
        If not it gets pushed to the output stack.
        Operators evaluates the items on the output stack and then push them back onto it          
        */
        private double InternalEval(IEnumerable<string> input)
        {
            var output = new Stack<double>();
            foreach (var token in input)
            {
                if (!Ops.OperatorExists(token))
                {
                    output.Push(double.Parse(token, CultureInfo.InvariantCulture));
                    continue;
                }
                EvaluateOperator(token, output);
            }

            return output.Pop();
        }

        private void EvaluateOperator(string token, Stack<double> output)
        {
            var op = Ops[token];
                     
            var outputLeft = output.Count;
            if (outputLeft < op.InputArgs && (outputLeft < 1 && !op.SpecialUnary))
                throw new InvalidInputException();

            if (op.SpecialUnary && outputLeft < op.InputArgs)// Handle unary operator special case (where it's both unary and not)
            {
                HandleSpecialUnaryOperator(output, op);
            }
            else
            {
                HandleDefaultOperatorBehaviour(output, op);
            }            
        }

        private static void HandleSpecialUnaryOperator(Stack<double> output, IOperator<double> op)
        {                        
            var arguments = OutputHelper.GetArgumentList(1, output);
            output.Push(op.Evaluate(arguments[0]));           
        }

        private static void HandleDefaultOperatorBehaviour(Stack<double> output, IOperator<double> op)
        {
            var arguments = OutputHelper.GetArgumentList(op.InputArgs, output);
            switch (op.InputArgs)
            {
                case 1:          
                    output.Push(op.Evaluate(arguments[0]));                    
                    break;
                case 2:
                    output.Push(op.Evaluate(arguments[0], arguments[1]));
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}