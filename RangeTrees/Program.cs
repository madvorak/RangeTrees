using System;

namespace RangeTrees
{
    internal class Program
    {
        private static IRangeTree tree;
        private static int currentSize = -1;

        static void Main(string[] args)
        {
            tree = new BBalphaRangeTree();

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] tokens = line.Split();
                char operation = tokens[0][0];
                switch (operation)
                {
                    case '#':
                        printResults();
                        tree = new BBalphaRangeTree();
                        currentSize = int.Parse(tokens[1]);
                        break;
                    case 'I':
                        tree.Insert(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                    case 'C':
                        tree.RangeCount(int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]));
                        break;
                }
            }
            printResults();
        }

        private static void printResults()
        {
            // TODO
            if (currentSize > 0)
            {
                Console.WriteLine($"{currentSize} {tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue)}");
            }
        }
    }
}
