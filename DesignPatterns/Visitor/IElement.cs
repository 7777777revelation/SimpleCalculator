using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// represents the interface of an element in a structure that would be 'visited' by a 'visitor' to perform
    /// and operation on the element
    /// </summary>
    public interface IElement
    {
        void Accept(IVisitor visitor);
    }
}
