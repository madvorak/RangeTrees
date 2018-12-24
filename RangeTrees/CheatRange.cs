using System;
using System.Collections.Generic;

namespace RangeTrees
{
    public class CheatRange : IRangeTree
    {
        private List<Tuple<int, int>> points = new List<Tuple<int, int>>();

        public void Insert(int x, int y)
        {
            points.Add(new Tuple<int, int>(x, y));
        }

        public int RangeCount(int xMin, int xMax, int yMin, int yMax)
        {
            int found = 0;
            foreach (var p in points)
            {
                if (p.Item1 >= xMin && p.Item1 <= xMax && p.Item2 >= yMin && p.Item2 <= yMax)
                {
                    found++;
                }
            }
            return found;
        }
    }
}
