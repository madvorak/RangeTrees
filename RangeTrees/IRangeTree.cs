namespace RangeTrees
{
    interface IRangeTree
    {
        void Insert(int x, int y);
        int RangeCount(int xMin, int xMax, int yMin, int yMax);
    }
}
