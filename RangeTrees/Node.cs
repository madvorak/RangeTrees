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
    }
}
