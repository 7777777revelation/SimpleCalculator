using System;

namespace SimpleCalculator
{
    /// <summary>
    /// this class generates the binary expression tree
    /// </summary>
    public class ExpressionTreeGenerator
    {
        private AtomicElementGenerator _atomicElementGenerator;
        private static int _nodeIdCounter = 0;

        public ExpressionTreeGenerator(AtomicElementGenerator atomicElementGenerator)
        {
            _atomicElementGenerator = atomicElementGenerator;
        }

        public Node ParseExpression()
        {
            var expr = ParseAddSubtract();

            // Check everything was consumed
            if (_atomicElementGenerator.ExpressionAtomType != AtomicElementType.EOL)
                throw new Exception("Unexpected characters at end of expression");

            return expr;
        }

        /// <summary>
        /// This function represents the lowest precedence operators (add and subtract).  It will call the routine for higher precedence operators (multiply and divide) so that the tree is built with the correct precedence
        /// </summary>
        /// <returns></returns>
        Node ParseAddSubtract()
        {
            // Parse the left hand side
            var leftSideOfOp = ParseMultiplyDivide();

            while (true)
            {
                // Work out the operator
                Func<decimal, decimal, decimal> op = null;
                if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Add)
                {
                    op = (a, b) => a + b;
                }
                else if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Subtract)
                {
                    op = (a, b) => a - b;
                }

                // Binary operator found?
                if (op == null)
                    return leftSideOfOp;             // no

                // Skip the operator
                _atomicElementGenerator.NextExpressionAtom();

                // Parse the right hand side of the expression
                var rightSideOfOp = ParseMultiplyDivide();

                // Create a binary node and use it as the left-hand side from now on
                leftSideOfOp = new BinaryNode(leftSideOfOp, rightSideOfOp, op);
                leftSideOfOp.NodeId = ++_nodeIdCounter;
                
            }
        }

        /// <summary>
        /// This routine represents multiply and divide, and calls the routine for unary operators (i.e. '+6' or '-6') since the unary operators have higher precedence than multiply and divide.
        /// This ensures that the tree will be build with the proper precedence.
        /// </summary>
        /// <returns></returns>
        Node ParseMultiplyDivide()
        {
            // Parse the left hand side
            var leftSideOfOp = ParseUnary();

            while (true)
            {
                // Work out the operator
                Func<decimal, decimal, decimal> op = null;
                if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Multiply)
                {
                    op = (a, b) => a * b;
                }
                else if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Divide)
                {
                    op = (a, b) => a / b;
                }

                // Binary operator found?
                if (op == null)
                    return leftSideOfOp;             // no

                // Skip the operator
                _atomicElementGenerator.NextExpressionAtom();

                // Parse the right hand side of the expression
                var rightSideOfOp = ParseUnary();

                // Create a binary node and use it as the left-hand side from now on
                leftSideOfOp = new BinaryNode(leftSideOfOp, rightSideOfOp, op);
                leftSideOfOp.NodeId = ++_nodeIdCounter;
                
            }
        }


        /// <summary>
        /// This routine represents unary operators for plus and minus (i.e. '+6' or '-6')
        /// </summary>
        /// <returns></returns>
        Node ParseUnary()
        {
            while (true)
            {
                // Positive operator is a no-op so just skip it
                if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Add)
                {
                    // Skip
                    _atomicElementGenerator.NextExpressionAtom();
                    continue;
                }

                // Negative operator
                if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Subtract)
                {
                    // Skip
                    _atomicElementGenerator.NextExpressionAtom();

                    // Parse right side of operation 
                    // Note this recurses to self to support negative of a negative
                    var rightSideOfOp = ParseUnary();

                    // Create unary node
                    var node = new UnaryNode(rightSideOfOp, (a) => -a);
                    node.NodeId = ++_nodeIdCounter;
                    return node;
                }

                // No positive/negative operator so parse a leaf node
                return ParseLeaf();
            }
        }


        /// <summary>
        /// This routine builds the 'leaf' nodes that have no children
        /// </summary>
        /// <returns></returns>
        Node ParseLeaf()
        {
            // Is it a number?
            if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.Number)
            {
                var node = new NumberNode(_atomicElementGenerator.NumericAtom);
                node.NodeId = ++_nodeIdCounter;
                //node.IsLeaf = true;
                //node.NodeValue = _atomicElementGenerator.NumericAtom;
                _atomicElementGenerator.NextExpressionAtom();
                return node;
            }

            // Parenthesis?
            if (_atomicElementGenerator.ExpressionAtomType == AtomicElementType.OpenParen)
            {
                // Skip '('
                _atomicElementGenerator.NextExpressionAtom();

                // Parse a top-level expression
                var node = ParseAddSubtract();

                // Check and skip ')'
                if (_atomicElementGenerator.ExpressionAtomType != AtomicElementType.CloseParen)
                    throw new Exception("Missing close parenthesis");
                _atomicElementGenerator.NextExpressionAtom();

                // Return
                return node;
            }
            
            throw new Exception($"Unexpected expression atom: {_atomicElementGenerator.NumericAtom}");
        }

        private static Node GenerateExpressionTree(AtomicElementGenerator atomicElementGenerator)
        {
            var expressionTreeGenerator = new ExpressionTreeGenerator(atomicElementGenerator);
            return expressionTreeGenerator.ParseExpression(); 
        }

        /// <summary>
        /// This routine is called by the user to generate a binary expression tree from an arithmetic expression string.
        /// The root node of the tree is returned, for which the user can run operations on all nodes of the tree, or
        /// evaluate the result of the arithmetic expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Node GenerateExpressionTree(string expression)
        {
            return GenerateExpressionTree(new AtomicElementGenerator(expression));
        }
    }
}
