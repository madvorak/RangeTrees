namespace RangeTrees
{
    class BBalphaRangeTree : IRangeTree
    {
        private Node root;

        public BBalphaRangeTree()
        {
            root = null;
        }

        public void Insert(int x, int y)
        {
            if (root == null)
            {
                root = new Node(true, x, y);
            }
            else
            {
                root.Insert(x, y);
            }
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                return root.ElementCount; // TODO count really
            }
        }
    }
}
