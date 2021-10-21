using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCalculator;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AtomicElementGeneratorTest()
        {
            //These tests must be grouped together because the AtomicElementGenerator is "stateful" within
            // the context of one expression (it keeps track of where it is within the expression string)

            var expressionString = "30 + 50 - 43.897";
            var generator = new AtomicElementGenerator(expressionString);

            //test for "30"
            Assert.AreEqual(generator.ExpressionAtomType, AtomicElementType.Number);
            Assert.AreEqual(generator.NumericAtom, 30M);
            generator.NextExpressionAtom();

            //test for "+"
            Assert.AreEqual(generator.ExpressionAtomType, AtomicElementType.Add);
            generator.NextExpressionAtom();

            //test for "50"
            Assert.AreEqual(generator.ExpressionAtomType, AtomicElementType.Number);
            Assert.AreEqual(generator.NumericAtom, 50M);
            generator.NextExpressionAtom();

            //test for "-"
            Assert.AreEqual(generator.ExpressionAtomType, AtomicElementType.Subtract);
            generator.NextExpressionAtom();

            //test for "43.897"
            Assert.AreEqual(generator.ExpressionAtomType, AtomicElementType.Number);
            Assert.AreEqual(generator.NumericAtom, 43.897M);
            generator.NextExpressionAtom();
        }


        [TestMethod]
        public void UnaryNode_NegativeOperatorEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("-10");
            decimal test = expressionTreeRootNode.Evaluate();
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), -10M);
        }

        [TestMethod]
        public void UnaryNode_PositiveOperatorEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("+10");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 10M);
        }

        [TestMethod]
        public void UnaryNode_CombinationOfNegativeAndPositiveUnaryOperatorsEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 + -20 - +30");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), -40M);
        }

        [TestMethod]
        public void Addition_AddOperationEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 + 20");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 30M);
        }

        [TestMethod]
        public void Subtraction_SubtractOperationEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 - 20");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), -10M);
        }

        [TestMethod]
        public void AddSubtact_CombinationOfAdditionAndSubtractonEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 + 20 - 40 + 100");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 90M);
        }


        [TestMethod]
        public void Multiply_MultiplicationOperationEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 * 20");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 200M);
        }

        [TestMethod]
        public void Divide_DivisionOperationEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 / 20");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 0.5M);
        }

        [TestMethod]
        public void MultiplyAndDivide_CombinationOfMultiplicationAndDivisionEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("10 * 20 / 50");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), 4M);
        }


        [TestMethod]
        public void Precedence_CombinationOfMultiplicationAndDivisionNoParensEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("1 + 2.3 / 4 * 5 - 6");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), -2.125M);
        }


        [TestMethod]
        public void Precedence_CombinationOfMultiplicationAndDivisionWithParensEvaluatesCorrectly()
        {
            var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree("(1 + 2.3) / 4 * (5 - 6)");
            Assert.AreEqual(expressionTreeRootNode.Evaluate(), -.825M);
        }
    }
}
