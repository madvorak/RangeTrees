using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            IRangeTree tree = new BBalphaRangeTree();
            tree.Insert(3, 5);
            tree.Insert(0, 8);
            tree.Insert(4, 10);
            Console.WriteLine(tree.RangeCount(0, 0, 10, 10));
            Console.ReadKey();
        }
    }
}
