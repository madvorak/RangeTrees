namespace RangeTrees
{
    public class DummyTree : IRangeTree
    {
        public void Insert(int x, int y)
        {
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            return 0;
        }
    }
}
