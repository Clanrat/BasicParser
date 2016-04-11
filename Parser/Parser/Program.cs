using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Converter;
using Parser.Enums;
using Parser.FunctionParser;
using Parser.Interface;
using Parser.OperatorTypes;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new SimpleParser();
            do
            {
                Console.WriteLine("Input:");
                var input = Console.ReadLine();
                var result = parser.Parse(input);
                Console.WriteLine($"Result: {result}\n");

            } while (true);
        }
    }
}
