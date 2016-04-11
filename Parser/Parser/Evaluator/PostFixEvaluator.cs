using System;
using System.Collections.Generic;
using System.Globalization;
using Parser.Converter;
using Parser.Enums;
using Parser.Evaluator.Exceptions;
using Parser.Evaluator.Helpers;
using Parser.Interface;
using Parser.OperatorTypes;

namespace Parser.Evaluator
{
    /*
    Evaluates a space delimeted string of a mathematical function written in reverse polish notation

        
    */
    public class PostFixEvaluator : IEvaluator<double>
    {
        private OperatorCollection Ops { get; }


        public PostFixEvaluator(OperatorCollection ops)
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

        private static void HandleSpecialUnaryOperator(Stack<double> output, IOperator op)
        {
            var unOp = op as BaseOperator<double>;
            if (unOp == null)
                OnCastError(op, typeof (BaseOperator<>));
            else
            {
                var arguments = OutputHelper.GetArgumentList(1, output);
                output.Push(unOp.UnaryFunc(arguments[0]));
            }
        }

        private static void HandleDefaultOperatorBehaviour(Stack<double> output, IOperator op)
        {
            var arguments = OutputHelper.GetArgumentList(op.InputArgs, output);


            switch (op.InputArgs)
            {
                case 1:

                    var unOp = op as UnaryOperator<double>;
                    if (unOp == null)
                        OnCastError(op, typeof (UnaryOperator<>));
                    else
                    {
                        output.Push(unOp.Function(arguments[0]));
                    }
                    break;
                case 2:

                    var biOp = op as Operator<double>;
                    if (biOp == null)
                        OnCastError(op, typeof (Operator<>));
                    else
                    {
                        output.Push(biOp.Associativity == Associativity.L
                            ? biOp.Function(arguments[1], arguments[0])
                            : biOp.Function(arguments[0], arguments[1]));
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }


        private static void OnCastError(IOperator op, Type t)
        {
            throw new InvalidCastException($"Could not cast {op.Symbol} as {t}");
        }
    }
}