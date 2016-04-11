using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Enums;
using Parser.FunctionParser;
using Parser.Interface;
using Parser.OperatorTypes;

namespace TestParser
{
    [TestClass]
    public class ParserTest
    {
        public List<IOperator<double>> Ops = new List<IOperator<double>>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, false),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, false),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b, false),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b, false)

        };

        [TestMethod]
        public void TestParse()
        {
            var parser = new SimpleParser(Ops);
            var result = parser.Parse("1+2+3");
            var expected = 6;
            Assert.AreEqual(expected, result);
        }
    }
}
