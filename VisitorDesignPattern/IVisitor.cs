using System;
using System.Collections.Generic;
using System.Text;

namespace VisitorDesignPattern
{
    /// <summary>
    /// the interface defining what a 'visitor' needs to implement.  A 'visitor' will 'visit' the elements
    /// in a structure of elements
    /// </summary>
    public interface IVisitor
    {
        void Visit(IElement element);
    }
}
