using System.Collections.Generic;

namespace RangeTrees.Nodes
{
    internal class RangeNodeY : RangeNodeBase<RangeNodeY>
    {
        private int coordX;  // probably useless
        private int coordY;

        public RangeNodeY(int x, int y)
        {
            coordX = x;
            coordY = y;
        }

        public void Insert(int x, int y)
        {
            if (y <= coordY)
            {
                if (leftChild == null)
                {
                    leftChild = new RangeNodeY(x, y);
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
                    rightChild = new RangeNodeY(x, y);
                }
                else
                {
                    rightChild.Insert(x, y);
                }
            }

            Size++;
            if (!IsBalanced())
            {
                rebuild();
            }
        }

        private void rebuild()
        {
            // 1. traverse in-order to build an array (list) of points
            List<int> pointsX = new List<int>();
            List<int> pointsY = new List<int>();
            traverse(pointsX, pointsY);

            // 2. find middle and put it into this
            int middleIndex = pointsX.Count / 2;
            coordX = pointsX[middleIndex];
            coordY = pointsY[middleIndex];

            // 3. recursively build left subtree from the first half of the array
            leftChild = build(pointsX, pointsY, 0, middleIndex - 1);

            // 4. recursively build right subtree from the second half of the array
            rightChild = build(pointsX, pointsY, middleIndex + 1, pointsX.Count - 1);
        }

        private void traverse(List<int> xs, List<int> ys)
        {
            if (leftChild != null)
            {
                leftChild.traverse(xs, ys);
            }

            xs.Add(coordX);
            ys.Add(coordY);

            if (rightChild != null)
            {
                rightChild.traverse(xs, ys);
            }
        }

        private static RangeNodeY build(List<int> xs, List<int> ys, int start, int finish)
        {
            if (start > finish)
            {
                return null;
            }

            int middle = (start + finish) / 2;
            return new RangeNodeY(xs[middle], ys[middle])
            {
                leftChild = build(xs, ys, start, middle - 1),     // may be null
                rightChild = build(xs, ys, middle + 1, finish),   // may be null
                Size = finish - start + 1
            };
        }

        public int Query(int yMin, int yMax)
        {
            if (coordY < yMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.Query(yMin, yMax);
            }
            if (coordY > yMax)
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

            return leftCount + 1 + rightCount;
        }

        private int queryLeft(int yMin)
        {
            if (coordY < yMin)
            {
                if (rightChild == null)
                {
                    return 0;
                }
                return rightChild.queryLeft(yMin);
            }

            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.Size;
            }

            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.queryLeft(yMin);
            }

            return leftCount + 1 + rightCount;
        }

        private int queryRight(int yMax)
        {
            if (coordY > yMax)
            {
                if (leftChild == null)
                {
                    return 0;
                }
                return leftChild.queryRight(yMax);
            }

            int leftCount = 0;
            if (leftChild != null)
            {
                leftCount = leftChild.Size;
            }

            int rightCount = 0;
            if (rightChild != null)
            {
                rightCount = rightChild.queryRight(yMax);
            }

            return leftCount + 1 + rightCount;
        }
    }
}
