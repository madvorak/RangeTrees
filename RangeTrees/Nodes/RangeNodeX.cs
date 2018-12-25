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
            int[] pointsX = new int[Size];
            int[] pointsY = new int[Size];
            int index = 0;
            traverse(pointsX, pointsY, ref index);

            // 2. find the middle point and put it into this
            int middleIndex = pointsX.Length / 2;
            middleX = pointsX[middleIndex];
            storedY = pointsY[middleIndex]; 
            // TODO what about Y coordinates are not sorted !?
            // should I sort them here?
            // OMG nooooo !!!!!!! must sort Y coordinates only when right before calling build od Y-tree
            // but isn't it inefficient ?????? yes, it is!
            // possible solution: traverse Y-tree, find median by X, then "split" by the X and keep order by Y
            // median by X (and medians in respective subtrees) can be found by X-tree traverse without any complicated algorithm
            // ys values will have to be filtered every time recursion is called

            // 3. recursively build left subtree from the first half of the array including Y-trees
            leftChild = build(pointsX, pointsY, 0, middleIndex - 1);

            // 4. recursively build right subtree from the second half of the array including Y-trees
            rightChild = build(pointsX, pointsY, middleIndex + 1, pointsX.Length - 1);
        }

        private void traverse(int[] xs, int[] ys, ref int index)
        {
            if (leftChild != null)
            {
                leftChild.traverse(xs, ys, ref index);
            }

            xs[index] = middleX;
            ys[index] = storedY;
            index++;

            if (rightChild != null)
            {
                rightChild.traverse(xs, ys, ref index);
            }
        }

        private static RangeNodeX build(int[] xs, int[] ys, int start, int finish)
        {
            if (start > finish)
            {
                return null;
            }

            int middle = (start + finish) / 2;
            int[] sortedYs = ys.Take(finish + 1).Skip(start).OrderBy(k => k).ToArray();
            // TODO sort without LINQ

            return new RangeNodeX(xs[middle], ys[middle])
            {
                leftChild = build(xs, ys, start, middle - 1),     // may be null
                rightChild = build(xs, ys, middle + 1, finish),   // may be null
                tree = RangeNodeY.Build(sortedYs, 0, sortedYs.Length - 1),
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
