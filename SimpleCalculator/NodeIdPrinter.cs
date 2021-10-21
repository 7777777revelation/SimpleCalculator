using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDesignPattern;

namespace SimpleCalculator
{
    /// <summary>
    /// represents a 'visitor' that prints the id of each node
    /// </summary>
    public class NodeIdPrinter : IVisitor
    {
        public NodeIdPrinter()
        {
        }

        public void Visit(IElement element)
        {
            Node node = (Node)element;
            Console.WriteLine("Node Id: {0}", node.NodeId);
        }

    }
}


