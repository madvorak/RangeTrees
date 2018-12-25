using System.Collections.Generic;
using System.Linq;

namespace RangeTrees.Nodes
{
    internal class RangeNodeX : RangeNodeBase<RangeNodeX>
    {
        private RangeNodeY tree;
        private int middleX;
        private int storedY;
        
        public RangeNodeX(int x, int y)
        {
            middleX = x;
            storedY = y;
            tree = new RangeNodeY(y);
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

            tree.Insert(y);

            Size++;
            if (!IsBalanced())
            {
                rebuild();
            }
        }

        private void rebuild()
        {
            // 1. traverse X-tree in-order to build an array (list) of points
            List<int> pointsX = new List<int>();
            List<int> pointsY = new List<int>();
            traverse(pointsX, pointsY);

            // 2. find the middle point and put it into this
            int middleIndex = pointsX.Count / 2;
            middleX = pointsX[middleIndex];
            storedY = pointsY[middleIndex]; 
            // TODO what about Y coordinates are not sorted !?
            // should I sort them here?
            // or traverse Y-tree? ... how to handle subtrees then ???
            // OMG nooooo !!!!!!! must sort Y coordinates only when right before calling build od Y-tree
            // but isn't it inefficient ??????

            // 3. rebuild corresponding Y-tree (probably not needed)

            // 4. recursively build left subtree from the first half of the array including Y-trees
            leftChild = build(pointsX, pointsY, 0, middleIndex - 1);

            // 5. recursively build right subtree from the second half of the array including Y-trees
            rightChild = build(pointsX, pointsY, middleIndex + 1, pointsX.Count - 1);
        }

        private void traverse(List<int> xs, List<int> ys)
        {
            if (leftChild != null)
            {
                leftChild.traverse(xs, ys);
            }

            xs.Add(middleX);
            ys.Add(storedY);

            if (rightChild != null)
            {
                rightChild.traverse(xs, ys);
            }
        }

        private static RangeNodeX build(List<int> xs, List<int> ys, int start, int finish)
        {
            if (start > finish)
            {
                return null;
            }

            int middle = (start + finish) / 2;
            List<int> sortedYs = ys.Take(finish + 1).Skip(start).OrderBy(k => k).ToList();
            // TODO sort without LINQ

            return new RangeNodeX(xs[middle], ys[middle])
            {
                leftChild = build(xs, ys, start, middle - 1),     // may be null
                rightChild = build(xs, ys, middle + 1, finish),   // may be null
                tree = RangeNodeY.Build(sortedYs, 0, sortedYs.Count - 1),
                Size = finish - start + 1
            };
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

        // for testing purposes only
        protected override bool areInternalsConsistent()
        {
            if (leftChild != null)
            {
                if (leftChild.middleX > this.middleX)
                {
                    return false;
                }
            }
            if (rightChild != null)
            {
                if (rightChild.middleX < this.middleX)
                {
                    return false;
                }
            }
            return (this.Size == tree.Size) && tree.IsTheWholeTreeConsistent();
        }
    }
}
