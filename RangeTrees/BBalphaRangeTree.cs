using RangeTrees.Nodes;

namespace RangeTrees
{
    public class BBalphaRangeTree : IRangeTree
    {
        private RangeNodeX root;
        private VisitsRegister visitsRegister;

        public BBalphaRangeTree()
        {
            root = null;
            visitsRegister = VisitsRegister.Instance;
        }

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
            visitsRegister.InsertEnded();
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            int result;
            if (root == null)
            {
                result = 0;
            }
            else
            {
                result = root.Query(xMin, xMax, yMin, yMax);
            }
            visitsRegister.QueryEnded();
            return result;
        }

        // for testing purposes only
        public bool isConsistent()
        {
            if (root == null)
            {
                return true;
            }
            else
            {
                return root.IsTheWholeTreeConsistent();
            }
        }
    }
}
