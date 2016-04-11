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
            var Ops = new List<IOperator>
            {   
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b),
            new Operator<double>("^", 2, Associativity.L, Math.Pow)

            };

            var parser = new SimpleParser(Ops);
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
