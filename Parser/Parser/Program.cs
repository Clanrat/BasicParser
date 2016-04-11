using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Converter;
using Parser.Enums;
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
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b)

            };
            var converter = new PostFixConverter(new OperatorCollection(Ops));
            var result = converter.Convert("1 + 2");
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
