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
            var result = Math.Round(expressionTreeRootNode.Evaluate(),2, MidpointRounding.AwayFromZero);
            Console.WriteLine("\r\nExpression '{0}' calculates to: {1}.\r\n", expression, result);

            Console.WriteLine("Please hit 'Enter' to exit the program.");
            Console.ReadLine();
        }
    }
}

//TODO
//DONE 1. finish visitor code  
//DONE2. add unit tests 
// exception for errors
//3. add comments and function headers
//DONE4. change variable names
//DONE 5. round to 2 decimals and change doubles to decimal
//5. do power point
//6. upload to git


