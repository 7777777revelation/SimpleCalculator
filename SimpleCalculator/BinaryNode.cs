using System;

namespace SimpleCalculator
{
    /// <summary>
    /// A node in the expression tree can be a 'leaf', or can have children;  A binary node has two children, both the left and right side of an operation.
    /// </summary>
    public class BinaryNode : Node
    {
        public Node LeftSideOfOp;                              // Left hand side of the operation
        public Node RightSideOfOp;                              // Right hand side of the operation
        Func<decimal, decimal, decimal> _operation;       // The callback operator

        public BinaryNode(Node leftSideOfOp, Node rightSideOfOp, Func<decimal, decimal, decimal> operation)
        {
            LeftSideOfOp = leftSideOfOp;
            RightSideOfOp = rightSideOfOp;
            _operation = operation;
        }

        /// <summary>
        /// Evaluates the actual value of this node
        /// </summary>
        /// <returns></returns>
        public override decimal Evaluate()
        {
            // Evaluate both sides
            var leftSideValue = LeftSideOfOp.Evaluate();
            var rightSideValue = RightSideOfOp.Evaluate();

            // Evaluate and return
            var result = _operation(leftSideValue, rightSideValue);
            return result;
        }
    }
}
