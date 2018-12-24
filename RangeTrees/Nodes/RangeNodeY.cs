namespace RangeTrees.Nodes
{
    internal class RangeNodeY : RangeNodeBase<RangeNodeY>
    {
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }

        public RangeNodeY(int x, int y)
        {
            CoordX = x;
            CoordY = y;
        }

        public void Insert(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int Query(int yMin, int yMax)
        {
            if (CoordY < yMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.Query(yMin, yMax);
            }
            if (CoordY > yMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.Query(yMin, yMax);
            }

            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.queryLeft(yMin);
            }
            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.queryRight(yMax);
            }
            int me = (CoordY >= yMin && CoordY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryLeft(int yMin)
        {
            if (CoordY < yMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.queryLeft(yMin);
            }

            int rightCount = rightChild.Size;
            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.queryLeft(yMin);
            }
            int me = (CoordY >= yMin) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryRight(int yMax)
        {
            if (CoordY > yMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.queryRight(yMax);
            }

            int leftCount = leftChild.Size;
            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.queryRight(yMax);
            }
            int me = (CoordY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }
    }
}
