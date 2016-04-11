using System;
using System.Collections.Generic;

namespace Parser.Evaluator.Helpers
{
    internal static class OutputHelper
    {
        //Gets the next numberOfArguments items from the output stack
        public static List<T> GetArgumentList<T>(int numberOfArguments, Stack<T> output)
        {
            var result = new List<T>();
            
            if(output.Count < numberOfArguments)
                throw new ArgumentException($"Need atleast {numberOfArguments} got {output.Count}");

            for (var i = 0; i < numberOfArguments; i++)
            {
                result.Add(output.Pop());
            }

            return result;
        } 
         
    }
}