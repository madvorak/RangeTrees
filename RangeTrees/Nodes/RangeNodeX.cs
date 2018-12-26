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
            // 1. in-order traverse the X-tree to get an array of points
            int[] pointsX = new int[Size];
            int[] pointsY = new int[Size];
            int index = 0;
            traverse(pointsX, pointsY, ref index);

            // 2. find the middle point and put it into this
            int middleIndex = pointsX.Length / 2;
            middleX = pointsX[middleIndex];
            storedY = pointsY[middleIndex]; 

            // 3. recursively build the left subtree (including Y-trees) from the first half of the array 
            leftChild = build(pointsX, pointsY, 0, middleIndex - 1, out int[] _);

            // 4. recursively build the right subtree (including Y-trees) from the second half of the array
            rightChild = build(pointsX, pointsY, middleIndex + 1, pointsX.Length - 1, out _);
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

        // may return null, but ys_sortedby_Y is never null (can be an array or zero length)
        private static RangeNodeX build(int[] xs_sortedby_X, int[] ys_sortedby_X,
            int start, int finish, out int[] ys_sortedby_Y)
        {
            if (start > finish)
            {
                ys_sortedby_Y = new int[0];
                return null;
            }

            int middle = (start + finish) / 2;

            RangeNodeX node = new RangeNodeX(xs_sortedby_X[middle], ys_sortedby_X[middle])
            {
                leftChild = build(xs_sortedby_X, ys_sortedby_X, start, middle - 1, out int[] leftOut),
                rightChild = build(xs_sortedby_X, ys_sortedby_X, middle + 1, finish, out int[] rightOut),
                Size = finish - start + 1
            };

            ys_sortedby_Y = merge(merge(leftOut, new int[1] { ys_sortedby_X[middle] }), rightOut);
            node.tree = RangeNodeY.Build(ys_sortedby_Y, 0, ys_sortedby_Y.Length - 1);

            return node;
        }

        private static int[] merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int i = 0;
            int j = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] < right[j])
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
