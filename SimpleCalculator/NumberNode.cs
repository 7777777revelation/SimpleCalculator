using System;
using DesignPatterns.Singleton;

namespace SimpleCalculator
{
    /// <summary>
    /// a node in the expression tree that represents a number in the expression
    /// </summary>
    public class NumberNode : Node
    {
        public NumberNode(decimal number)
        {
            base.NodeId = Singleton.GetNextId();
            base.IsLeaf = true;
            base.NodeValue = number;
        }

        public override decimal Evaluate()
        {
            if (null == base.NodeValue)
                throw new Exception("Leaf node should never have null number value.");

            return base.NodeValue ?? 0;
        }
    }
}

