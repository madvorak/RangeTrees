using System.Collections.Generic;

namespace RangeTrees.Nodes
{
    internal class RangeNodeY : RangeNodeBase<RangeNodeY>
    {
        private int coordY;

        public RangeNodeY(int y)
        {
            coordY = y;
        }

        public void Insert(int x, int y)
        {
            if (y <= coordY)
            {
                if (leftChild == null)
                {
                    leftChild = new RangeNodeY(y);
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
                    rightChild = new RangeNodeY(y);
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
            List<int> points = new List<int>();
            traverse(points);

            // 2. find the middle point and put it into this
            int middleIndex = points.Count / 2;
            coordY = points[middleIndex];

            // 3. recursively build left subtree from the first half of the array
            leftChild = build(points, 0, middleIndex - 1);

            // 4. recursively build right subtree from the second half of the array
            rightChild = build(points, middleIndex + 1, points.Count - 1);
        }

        private void traverse(List<int> ys)
        {
            if (leftChild != null)
            {
                leftChild.traverse(ys);
            }

            ys.Add(coordY);

            if (rightChild != null)
            {
                rightChild.traverse(ys);
            }
        }

        private static RangeNodeY build( List<int> ys, int start, int finish)
        {
            if (start > finish)
            {
                return null;
            }

            int middle = (start + finish) / 2;
            return new RangeNodeY(ys[middle])
            {
                leftChild = build(ys, start, middle - 1),     // may be null
                rightChild = build(ys, middle + 1, finish),   // may be null
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

        // for testing purposes only
        protected override bool areInternalsBalanced()
        {
            return true;
        }
    }
}
