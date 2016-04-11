using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Converter;
using Parser.Converter.Exceptions;
using Parser.Enums;
using Parser.Interface;
using Parser.OperatorTypes;

namespace TestParser
{
    [TestClass]
    public class PostFixConverterTest
    {
        public List<IOperator> Ops = new List<IOperator>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b)
            
        };

        

        [TestMethod]
        public void TestBasicConvert()
        {
            var converter = new PostFixConverter(new OperatorCollection(Ops));
            var result = converter.Convert("1+2");
            var expected = "1 2 +";
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestOperatorPrecedence()
        {
            var converter = new PostFixConverter(new OperatorCollection(Ops));

            var resultWithoutParen = converter.Convert("1+2*3");
            var expectedWithoutParen = "1 2 3 * +";

            var resultWithParen = converter.Convert("(1+2)*3");
            var expectedWithParen = "1 2 + 3 *";
            
            Assert.AreEqual(expectedWithoutParen, resultWithoutParen);
            Assert.AreEqual(expectedWithParen, resultWithParen);
        }

        [TestMethod]
        public void TestDecimal()
        {
            var converter = new PostFixConverter(new OperatorCollection(Ops));
            var result = converter.Convert("1.02 + 2.3 + 4");
            var expected = "1.02 2.3 + 4 +";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiCharOperator()
        {
            var operators = new List<IOperator>
            {
                new Operator<double>("+",1, Associativity.B, (a,b) => a * b),
                new Operator<double>("*",2, Associativity.B, (a,b) => a * b),
                new Operator<double>("sin",1, Associativity.R, (a,b) => a * b)
            };
            var converter = new PostFixConverter(new OperatorCollection(operators));
            var result = converter.Convert("2+sin(2)");
            var expected = "2 2 sin +";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiMatchedOperator()
        {
            var operators = new List<IOperator>
            {
                new Operator<double>("*",2, Associativity.B, (a,b) => a * b),
                new Operator<double>("**",2, Associativity.B, (a,b) => a * b)
            };

            var converter = new PostFixConverter(new OperatorCollection(operators));
            var result = converter.Convert("2**3*5");
            var expected = "2 3 ** 5 *";
            Assert.AreEqual(expected, result);
        }

        [ExpectedException(typeof (InvalidFormatException))]
        [TestMethod]
        public void TestMultipleDecimalPoints()
        {
            var converter = new PostFixConverter(new OperatorCollection(Ops));
            var result = converter.Convert("1.00.2 + 5");
        }



        [ExpectedException(typeof (MissMatchedParenthesisException))]
        [TestMethod]
        public void TestMissmatchedParen()
        {
            var converter = new PostFixConverter(new OperatorCollection(Ops));
            var result = converter.Convert("1+2)");
            Console.WriteLine(result);

        }



        [ExpectedException(typeof (UnknownOperatorException))]
        [TestMethod]
        public void TestUnknownOperator()
        {
            var converter = new PostFixConverter( new OperatorCollection(Ops));
            var result = converter.Convert("5hello5");
        }
    }
}
