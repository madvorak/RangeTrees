using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTrees
{
    class Program
    {
        private static int currentSize = -1;

        static void Main(string[] args)
        {
            IRangeTree tree = new BBalphaRangeTree();
            
            while (true)
            {
                string[] tokens = Console.ReadLine().Split();
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
                        Console.WriteLine(tree.RangeCount(
                            int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4])));
                        break;
                }
            }
        }

        private static void printResults()
        {
            // TODO
            if (currentSize > 0)
            {
                Console.WriteLine(currentSize);
            }
        }
    }
}
