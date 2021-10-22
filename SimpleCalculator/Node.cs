using System;
using DesignPatterns;

namespace SimpleCalculator
{
    //represents a node in the expression tree that will be built
    public  class Node : IElement
    {
        //not going to use this Evaluate, but don't want an abstract class
        //since the concrete Visitor will cast IElement to an actual Node class
        public virtual decimal Evaluate() { throw new Exception("Node.Evaluate should never be called!!"); }

        //After expression tree is build, we will use the Visitor design pattern
        //to visit each node and print to the console the node id
        //for each node in the tree
        public int NodeId { get; set; }
        public bool IsLeaf { get; set; }
        public decimal? NodeValue { get; set; }

        public Node()
        {
            IsLeaf = false;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
