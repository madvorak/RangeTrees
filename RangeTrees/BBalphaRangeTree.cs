using RangeTrees.Nodes;

namespace RangeTrees
{
    public class BBalphaRangeTree : IRangeTree
    {
        private RangeNodeX root;

        public void Insert(int x, int y)
        {
            if (root == null)
            {
                root = new RangeNodeX(x, y);
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
                return root.Query(xMin, xMax, yMin, yMax);
            }
        }
    }
}
