using System;

namespace SimpleCalculator
{
    /// <summary>
    /// represents a unary operator and a number
    /// </summary>
    public class UnaryNode : Node
    {
        public Node RightSideOfOp;                              
        Func<decimal, decimal> _operation;              

        public UnaryNode(Node rightHandSideOfOp, Func<decimal, decimal> operation)
        {
            RightSideOfOp = rightHandSideOfOp;
            _operation = operation;
        }

        public override decimal Evaluate()
        {
            // Evaluate right side of operation
            var rightHandSideOfOpVal = RightSideOfOp.Evaluate();

            // Evaluate and return
            var result = _operation(rightHandSideOfOpVal);
            return result;
        }
    }
}
