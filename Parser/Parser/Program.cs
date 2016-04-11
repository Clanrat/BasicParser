using System;
using Parser.FunctionParser;

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
