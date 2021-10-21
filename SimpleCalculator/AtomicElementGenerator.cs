using System.Text;

namespace SimpleCalculator
{
    /// <summary>
    /// This class generate each "atomic" piece of the expression; For example, a number, such as -213 would be an atomic element, or an operator, such
    /// as '+' or '*'
    /// </summary>
    public class AtomicElementGenerator
    {
        private string _expression;
        private int _currentAtomicElementIndex = 0;
        private AtomicElementType _currentAtomicElementType;
        private decimal _numericAtom;
        private char _currentCharacter;

        public AtomicElementGenerator(string expression)
        {
            _expression = expression;
            NextCharacter();
            NextExpressionAtom();
        }

        public AtomicElementType ExpressionAtomType
        {
            get { return _currentAtomicElementType; }                       
        }

        /// <summary>
        /// The actual value of a number in the expression
        /// </summary>
        public decimal NumericAtom
        {
            get { return _numericAtom;  }
        }

        // read the next character from the expression and store
        // it in _currentCharacter.  If end of line, then store '\n'
        private void NextCharacter()
        {
            if (_currentAtomicElementIndex == _expression.Length)
            {
                _currentCharacter = '\n';
                return;
            }
            else
            {
                _currentCharacter = _expression[_currentAtomicElementIndex];
                _currentAtomicElementIndex++;
            }
        }

        //get the next expression atom from the expression
        public void NextExpressionAtom()
        {
            if (_currentCharacter == '\n')
            {
                _currentAtomicElementType = AtomicElementType.EOL;
                return;
            }

            while (char.IsWhiteSpace(_currentCharacter))
            {
                NextCharacter();
            }

            //special characters
            switch (_currentCharacter)
            {
                case '+':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.Add;                    
                    return;
                case '-':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.Subtract;
                    return;

                case '*':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.Multiply;
                    return;

                case '/':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.Divide;
                    return;

                case '(':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.OpenParen;
                    return;

                case ')':
                    NextCharacter();
                    _currentAtomicElementType = AtomicElementType.CloseParen;
                    return;
                default:
                    break;
            }

            // Is this a number?
            if (char.IsDigit(_currentCharacter) || _currentCharacter == '.')
            {
                // Capture digits/decimal point
                var sb = new StringBuilder();
                bool haveDecimalPoint = false;
                while (char.IsDigit(_currentCharacter) || (!haveDecimalPoint && _currentCharacter == '.'))
                {
                    sb.Append(_currentCharacter);
                    haveDecimalPoint = _currentCharacter == '.';
                    NextCharacter();
                }

                // Parse it
                _numericAtom = decimal.Parse(sb.ToString());
                _currentAtomicElementType = AtomicElementType.Number;
                return;
            }
        }
    }
}
