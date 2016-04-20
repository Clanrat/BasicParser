using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Collections;
using Parser.Enums;
using Parser.Evaluator;
using Parser.Interface;
using Parser.OperatorTypes;
using Parser.OperatorTypes.BaseTypes.Operator;

namespace TestParser
{
    [TestClass]
    public class EvaluatorTest
    {
        public List<IEvaluatable<double>> Ops = new List<IEvaluatable<double>>
        {
            new Operator<double>("+", 1, Associativity.B, (a, b) => a + b, true, a => +a),
            new Operator<double>("-", 1, Associativity.L, (a, b) => a - b, true, a => -a),
            new Operator<double>("*", 2, Associativity.B, (a, b) => a * b),
            new Operator<double>("/", 2, Associativity.L, (a, b) => a / b)

        };

        [TestMethod]
        public void TestBasicEval()
        {
            var evaluator = new PostFixEvaluator(new EvaluatableCollection<double>(Ops));
            var result = evaluator.Eval("1 2 +");
            var expected = 3;
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestUnaryOperators()
        {
            var evaluator = new PostFixEvaluator(new EvaluatableCollection<double>(Ops));
            var result = evaluator.Eval("1 -");
            var expected = -1;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAssociativity()
        {
            var evaluator = new PostFixEvaluator(new EvaluatableCollection<double>(Ops));
            var result = evaluator.Eval("1 2 -");
            var expected = -1;
        }

        [TestMethod]
        public void TestMultipleArgumentEvaluation()
        {
            var operators = new List<IEvaluatable<double>>
            {
                new Operator<double>("f",2, Associativity.L, (a,b) => a * b)
            };

            var evaluator = new PostFixEvaluator(new EvaluatableCollection<double>(operators));
            var result = evaluator.Eval("2 3 f");
            var expected = 6;
            Assert.AreEqual(expected, result);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestInvalidInput()
        {
            var evaluator = new PostFixEvaluator(new EvaluatableCollection<double>(Ops));
            var result = evaluator.Eval("+ 1 +");
            Console.WriteLine(result);
        }
    }
}
