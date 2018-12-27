namespace RangeTrees.Nodes
{
    internal class RangeNodeY : RangeNodeBase<RangeNodeY>
    {
        private int coordY;

        public RangeNodeY(int y)
        {
            coordY = y;
        }

        public void Insert(int y)
        {
            registerVisitByInsert();

            if (y <= coordY)
            {
                if (leftChild == null)
                {
                    leftChild = new RangeNodeY(y);
                }
                else
                {
                    leftChild.Insert(y);
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
                    rightChild.Insert(y);
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
            // 1. in-order traverse to get an array of points
            int[] points = new int[Size];
            int index = 0;
            traverse(points, ref index);

            // 2. find the middle point and put it into this
            int middleIndex = points.Length / 2;
            coordY = points[middleIndex];

            // 3. recursively build the left subtree from the first half of the array
            leftChild = Build(points, 0, middleIndex - 1);

            // 4. recursively build the right subtree from the second half of the array
            rightChild = Build(points, middleIndex + 1, points.Length - 1);
        }

        private void traverse(int[] ys, ref int index)
        {
            if (leftChild != null)
            {
                leftChild.traverse(ys, ref index);
            }

            ys[index++] = coordY;
            registerVisitByInsert();

            if (rightChild != null)
            {
                rightChild.traverse(ys, ref index);
            }
        }

        // may return null
        public static RangeNodeY Build(int[] ys, int start, int finish)
        {
            if (start > finish)
            {
                return null;
            }

            registerVisitByInsert();

            int middle = (start + finish) / 2;
            return new RangeNodeY(ys[middle])
            {
                leftChild = Build(ys, start, middle - 1),
                rightChild = Build(ys, middle + 1, finish),
                Size = finish - start + 1
            };
        }

        public int Query(int yMin, int yMax)
        {
            registerVisitByQuery();

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
            registerVisitByQuery();

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
            registerVisitByQuery();

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
        protected override bool areInternalsConsistent()
        {
            if (leftChild != null)
            {
                if (leftChild.coordY > this.coordY)
                {
                    return false;
                }
            }
            if (rightChild != null)
            {
                if (rightChild.coordY < this.coordY)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
