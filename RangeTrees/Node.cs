namespace RangeTrees
{
    class Node
    {
        public bool IsX { get; }
        public int Value { get; }
        public Node LeftChild { get; private set; }
        public Node RightChild { get; private set; }
        public Node LinkToNextTree { get; private set; }
        public int ElementCount { get; private set; }

        public Node(bool isX, int x, int y)
        {
            if (isX)
            {
                Value = x;
                LinkToNextTree = new Node(false, x, y);
            }
            else
            {
                Value = y;
                LinkToNextTree = null;
            }

            IsX = isX;
            LeftChild = null;
            RightChild = null;
            ElementCount = 1;
        }

        public void Insert(int x, int y)
        {
            Node place = null;

            if (IsX)
            {
                if (x < Value)
                {
                    place = LeftChild;
                }
                else
                {
                    place = RightChild;
                }
            }
            else
            {
                if (y < Value)
                {
                    place = LeftChild;
                }
                else
                {
                    place = RightChild;
                }
            }

            if (place == null)
            {
                place = new Node(IsX, x, y);
                // TODO which pointer overwrite?
            }
            else
            {
                place.Insert(x, y);
            }

            // TODO backpropagation of +1 count
            // TODO BB-alpha balancing
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            // TODO what if any child is NULL
            if (IsX)
            {
                if (xMax < Value)
                {
                    return LeftChild.RangeCount(xMin, xMax, yMin, yMax);
                }
                if (xMin > Value)
                {
                    return RightChild.RangeCount(xMin, xMax, yMin, yMax);
                }

                return LeftChild.CountLeftX(xMin, yMin, yMax) + RightChild.CountRightX(xMax, yMin, yMax);
            }
            else
            {
                if (yMax < Value)
                {
                    return LeftChild.RangeCount(xMin, xMax, yMin, yMax);
                }
                if (yMin > Value)
                {
                    return RightChild.RangeCount(xMin, xMax, yMin, yMax);
                }

                return LeftChild.CountLeftY(yMin) + RightChild.CountRightY(yMax);
            }
        }

        private int CountLeftX(int xMin, int yMin, int yMax)
        {
            if (xMin > Value)
            {
                return RightChild.CountLeftX(xMin, yMin, yMax);
            }
            else
            {
                // TODO the +1 should depend on Y
                return LeftChild.CountLeftX(xMin, yMin, yMax) + RightChild.RangeCount(0, 0, yMin, yMax) + 1;
            }
        }

        private int CountRightX(int xMax, int yMin, int yMax)
        {
            // TODO
            return 0;
        }

        private int CountLeftY(int yMin)
        {
            if (yMin > Value)
            {
                return RightChild.CountLeftY(yMin);
            }
            else
            {
                return LeftChild.CountLeftY(yMin) + RightChild.ElementCount + 1;
            }
        }

        private int CountRightY(int yMax)
        {
            // TODO
            return 0;
        }
    }
}
