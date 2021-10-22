using System;
using DesignPatterns;

namespace SimpleCalculator
{
    /// <summary>
    /// represents a 'visitor' that will print the value of each 'leaf' node
    /// </summary>
    public class LeafNodeValuePrinter : IVisitor
    {
        public LeafNodeValuePrinter()
        {
        }

        public void Visit(IElement element)
        {
            Node node = (Node)element;
            if (node.IsLeaf)
                Console.WriteLine("Leaf Node. Value is: {0}", node.NodeValue);
            else
                Console.WriteLine("Non-Leaf Node.");
        }
    }
}
