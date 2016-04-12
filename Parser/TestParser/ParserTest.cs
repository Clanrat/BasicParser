using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Enums;
using Parser.FunctionParser;
using Parser.Interface;
using Parser.OperatorTypes;
using Parser.OperatorTypes.BaseTypes.Operator;

namespace TestParser
{
    [TestClass]
    public class ParserTest
    {
        public List<IEvaluatable<double>> Ops = new List<IEvaluatable<double>>
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

        [TestMethod]
        public void TestSymbolEval()
        {
            var parser = new SimpleParser();
            var result = parser.Parse("2+x+3", "x", "2");
            var expected = 7;
            Assert.AreEqual(expected, result);
        }
    }
}
