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

        private class WorkingPoint
        {
            public int X;
            public int Y;

            public WorkingPoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private void rebuild()
        {
            // 1. traverse X-tree in-order to build an array (list) of points
            WorkingPoint[] points = new WorkingPoint[Size];
            int index = 0;
            traverse(points, ref index);

            // 2. find the middle point and put it into this
            int middleIndex = points.Length / 2;
            middleX = points[middleIndex].X;
            storedY = points[middleIndex].Y;

            int [] _;
            // 3. recursively build left subtree from the first half of the array including Y-trees
            leftChild = build(points, 0, middleIndex - 1, out _);

            // 4. recursively build right subtree from the second half of the array including Y-trees
            rightChild = build(points, middleIndex + 1, points.Length - 1, out _);
        }

        private void traverse(WorkingPoint[] pointArray, ref int index)
        {
            if (leftChild != null)
            {
                leftChild.traverse(pointArray, ref index);
            }

            pointArray[index++] = new WorkingPoint(middleX, storedY);

            if (rightChild != null)
            {
                rightChild.traverse(pointArray, ref index);
            }
        }

        private static RangeNodeX build(WorkingPoint[] sortedByX, int start, int finish, out int[] sortedYs)
        {
            if (start > finish)
            {
                sortedYs = new int[0];
                return null;
            }

            int middle = (start + finish) / 2;
            int[] leftOut;
            int[] rightOut;

            RangeNodeX node = new RangeNodeX(sortedByX[middle].X, sortedByX[middle].Y)
            {
                leftChild = build(sortedByX, start, middle - 1, out leftOut),     // may be null
                rightChild = build(sortedByX, middle + 1, finish, out rightOut),   // may be null
                Size = finish - start + 1
            };

            sortedYs = merge(merge(leftOut, new int[1] { sortedByX[middle].Y }), rightOut);
            node.tree = RangeNodeY.Build(sortedYs, 0, sortedYs.Length - 1);

            return node;
        }

        private static int[] merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int i = 0;
            int j = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i]< right[j])
                {
                    result[i + j] = left[i];
                    i++;
                }
                else
                {
                    result[i + j] = right[j];
                    j++;
                }
            }
            while (i < left.Length)
            {
                result[i + j] = left[i];
                i++;
            }
            while (j < right.Length)
            {
                result[i + j] = right[j];
                j++;
            }
            return result;
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
