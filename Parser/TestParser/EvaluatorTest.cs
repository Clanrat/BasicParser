﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Converter;
using Parser.Enums;
using Parser.Evaluator;
using Parser.Evaluator.Exceptions;
using Parser.Interface;
using Parser.OperatorTypes;

namespace TestParser
{
    [TestClass]
    public class EvaluatorTest
    {
        public List<IOperator> Ops = new List<IOperator>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b)

        };

        [TestMethod]
        public void TestBasicEval()
        {
            var evaluator = new PostFixEvaluator(new OperatorCollection(Ops));
            var result = evaluator.Eval("1 2 +");
            var expected = 3;
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestUnaryOperators()
        {
            var evaluator = new PostFixEvaluator(new OperatorCollection(Ops));
            var result = evaluator.Eval("1 -");
            var expected = -1;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAssociativity()
        {
            var evaluator = new PostFixEvaluator(new OperatorCollection(Ops));
            var result = evaluator.Eval("1 2 -");
            var expected = -1;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestInvalidInput()
        {
            var evaluator = new PostFixEvaluator(new OperatorCollection(Ops));
            var result = evaluator.Eval("+ 1 +");
            Console.WriteLine(result);
        }
    }
}