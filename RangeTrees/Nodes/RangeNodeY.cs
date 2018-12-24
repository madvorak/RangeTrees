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

            int leftCount = leftChild.queryLeft(yMin);
            int rightCount = rightChild.queryRight(yMax);
            int me = (CoordY >= yMin && CoordY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryLeft(int yMin)
        {
            throw new System.NotImplementedException();
        }

        private int queryRight(int yMax)
        {
            throw new System.NotImplementedException();
        }
    }
}
