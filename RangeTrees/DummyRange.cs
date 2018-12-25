namespace RangeTrees
{
    public class DummyRange : IRangeTree
    {
        private int counter = 0;

        public void Insert(int x, int y)
        {
            counter++;
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            // return counter;
            return 0;
        }
    }
}
