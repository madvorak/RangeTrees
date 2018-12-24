namespace RangeTrees.Nodes
{
    internal class RangeNodeX : RangeNodeBase<RangeNodeX>
    {
        private RangeNodeY tree;
        public int MiddleX { get; private set; }
        
        public RangeNodeX(int x, int y)
        {
            tree = new RangeNodeY(x, y);
            MiddleX = x;
        }

        public void Insert(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int Query(int xMin, int xMax, int yMin, int yMax)
        {
            if (MiddleX < xMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.Query(xMin, xMax, yMin, yMax);
            }
            if (MiddleX > xMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.Query(xMin, xMax, yMin, yMax);
            }

            int leftCount = leftChild.queryLeft(xMin, yMin, yMax);
            int rightCount = rightChild.queryRight(xMax, yMin, yMax);
            int me = (tree.CoordY >= yMin && tree.CoordY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryLeft(int xMin, int yMin, int yMax)
        {
            throw new System.NotImplementedException();
        }

        private int queryRight(int xMax, int yMin, int yMax)
        {
            throw new System.NotImplementedException();
        }
    }
}
