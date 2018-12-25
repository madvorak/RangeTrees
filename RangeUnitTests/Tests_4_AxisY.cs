using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_4_AxisY
    {
        private IRangeTree tree;
        private int y;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
            y = 33;
        }

        private int countAxisY()
        {
            return tree.RangeCount(int.MinValue, int.MaxValue, y, y);
        }

        [TestMethod]
        public void AxisY_1_EmptyTree()
        {
            Assert.AreEqual(0, countAxisY());
        }

        [TestMethod]
        public void AxisY_2_OneBadPoint()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(0, countAxisY());
        }

        [TestMethod]
        public void AxisY_3_OneGoodPoint()
        {
            tree.Insert(0, y);
            Assert.AreEqual(1, countAxisY());
        }

        [TestMethod]
        public void AxisY_4_Diagonal()
        {
            const int limit = 100;
            for (int i = y - limit; i <= y + limit; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(1, countAxisY());
        }

        [TestMethod]
        public void AxisY_5_ManyGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, y);
            }
            Assert.AreEqual(count, countAxisY());
        }

        [TestMethod]
        public void AxisY_6_ManyAlmostGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, y - 1);
            }
            Assert.AreEqual(0, countAxisY());
        }

        [TestMethod]
        public void AxisY_7_ManyAlmostGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, y + 1);
            }
            Assert.AreEqual(0, countAxisY());
        }

        [TestMethod]
        public void AxisY_8_Duplicate()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(0, y);
            }
            Assert.AreEqual(count, countAxisY());
        }
    }
}
