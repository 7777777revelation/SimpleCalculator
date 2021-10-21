using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    /// <summary>
    /// represents a unary operator and a number
    /// </summary>
    public class UnaryNode : Node
    {
        Node _rightHandSideOfOp;                              
        Func<decimal, decimal> _operation;              

        public UnaryNode(Node rightHandSideOfOp, Func<decimal, decimal> operation)
        {
            _rightHandSideOfOp = rightHandSideOfOp;
            _operation = operation;
        }

        Node _rightHandOp;                              // Right hand side of the operation
        Func<decimal, decimal> _op;               // The callback operator

        public override decimal Evaluate()
        {
            // Evaluate right side of operation
            var rightHandSideOfOpVal = _rightHandSideOfOp.Evaluate();

            // Evaluate and return
            var result = _operation(rightHandSideOfOpVal);
            return result;
        }
    }
}
