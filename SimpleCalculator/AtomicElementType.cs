using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    /// <summary>
    /// The types that an 'atomic' unit of the expression can be
    /// </summary>
    public enum AtomicElementType
    {
        EOL,
        Add,
        Subtract,
        Multiply,
        Divide,
        OpenParen,
        CloseParen,
        Number
    }
}
