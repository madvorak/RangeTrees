﻿namespace RangeTrees.Nodes
{
    internal class RangeNodeX : RangeNodeBase<RangeNodeX>
    {
        private RangeNodeY tree;
        private int middleX;
        private int storedY;
        
        public RangeNodeX(int x, int y)
        {
            tree = new RangeNodeY(x, y);
            middleX = x;
            storedY = y;
        }

        public void Insert(int x, int y)
        {
            if (x <= middleX)
            {
                if (leftChild == null)
                {
                    leftChild = new RangeNodeX(x, y);
                }
                else
                {
                    leftChild.Insert(x, y);
                }
            }
            else
            {
                if (rightChild == null)
                {
                    rightChild = new RangeNodeX(x, y);
                }
                else
                {
                    rightChild.Insert(x, y);
                }
            }

            tree.Insert(x, y);

            Size++;
            if (!IsBalanced())
            {
                // TODO
            }
        }

        public int Query(int xMin, int xMax, int yMin, int yMax)
        {
            if (middleX < xMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.Query(xMin, xMax, yMin, yMax);
            }
            if (middleX > xMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.Query(xMin, xMax, yMin, yMax);
            }

            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.queryLeft(xMin, yMin, yMax);
            }
            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.queryRight(xMax, yMin, yMax);
            }
            int me = (storedY >= yMin && storedY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryLeft(int xMin, int yMin, int yMax)
        {
            if (middleX < xMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.queryLeft(xMin, yMin, yMax);
            }

            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.tree.Query(yMin, yMax);
            }
            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.queryLeft(xMin, yMin, yMax);
            }
            int me = (storedY >= yMin && storedY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }

        private int queryRight(int xMax, int yMin, int yMax)
        {
            if (middleX > xMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.queryRight(xMax, yMin, yMax);
            }

            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.tree.Query(yMin, yMax);
            }
            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.queryRight(xMax, yMin, yMax);
            }
            int me = (storedY >= yMin && storedY <= yMax) ? 1 : 0;
            return leftCount + me + rightCount;
        }
    }
}
