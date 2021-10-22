using DesignPatterns;

namespace SimpleCalculator
{
    public class ExpressionTreeUtilities
    {
        /// <summary>
        /// Used to traverse a binary expression tree and apply 'visitor' operations on each node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="visitor"></param>
        private static void TraverseExpressionTree(Node node, IVisitor visitor)
        {
            if (node is NumberNode)
            {
                var numberNode = (NumberNode) node;
                numberNode.Accept(visitor);
                return;
            }
            else if (node is BinaryNode)
            {
                var binaryNode = (BinaryNode)node;
                binaryNode.Accept(visitor);
                TraverseExpressionTree(binaryNode.LeftSideOfOp, visitor);
                TraverseExpressionTree(binaryNode.RightSideOfOp, visitor);
            }            
        }

        /// <summary>
        /// called by the user to perform and operation on each node of an expression tree
        /// </summary>
        /// <param name="visitor">A visitor that will perform an operation on each node of the tree</param>
        /// <param name="rootNode">root node of the expression tree</param>
        public static void PerformOperation(IVisitor visitor, Node rootNode) {
            TraverseExpressionTree(rootNode, visitor);
        }
    }
}
