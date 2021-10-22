using System;

namespace SimpleCalculator
{
    class Program
    {
        /// <summary>
        /// The main applicaton.  Prompt the user for an arithmetic expression, build and expression tree, run a couple operations (demonstrating the Vistior Design Pattern), and then
        /// evaluate the expression tree and return the result to the user.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Type your arithmetic expression and press enter
            Console.WriteLine("Enter expression to calculate:");

            // Create a string variable and get user input from the keyboard and store it in the variable
            string expression = Console.ReadLine();

            try
            {
                var expressionTreeRootNode = ExpressionTreeGenerator.GenerateExpressionTree(expression);

                //at this point, expression tree has been built;  We can now use the Visitor design pattern to 
                //perform operations on every node in the tree:

                Console.WriteLine("\r\nOperation for NodeIdPrinter:\r\n");
                var nodeIdPrinterVisitor = new NodeIdPrinter();
                ExpressionTreeUtilities.PerformOperation(nodeIdPrinterVisitor, expressionTreeRootNode);

                Console.WriteLine("\r\nOperation for LeafNodeValuePrinter:\r\n");
                var leafNodeValuePrinterVisitor = new LeafNodeValuePrinter();
                ExpressionTreeUtilities.PerformOperation(leafNodeValuePrinterVisitor, expressionTreeRootNode);

                //Evaluate the expression tree and calculate the value of the arithmetic expression
                var result = Math.Round(expressionTreeRootNode.Evaluate(), 2, MidpointRounding.AwayFromZero);
                Console.WriteLine("\r\nExpression '{0}' calculates to: {1}.\r\n", expression, result);
            }
            catch (Exception exc)
            {
                Console.WriteLine("\r\nUnable to calculate expression '{0}': {1}\r\n", expression, exc.Message);
            }

            Console.WriteLine("Please hit 'Enter' to exit the program.");
            Console.ReadLine();
        }
    }
}



