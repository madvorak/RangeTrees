using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_3_AxisX
    {
        private IRangeTree tree;
        private int x;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
            x = 77;
        }

        private int countAxisX()
        {
            return tree.RangeCount(x, x, int.MinValue, int.MaxValue);
        }

        [TestMethod]
        public void AxisX_1_EmptyTree()
        {
            Assert.AreEqual(0, countAxisX());
        }

        [TestMethod]
        public void AxisX_2_OneBadPoint()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(0, countAxisX());
        }

        [TestMethod]
        public void AxisX_3_OneGoodPoint()
        {
            tree.Insert(x, 0);
            Assert.AreEqual(1, countAxisX());
        }

        [TestMethod]
        public void AxisX_4_Diagonal()
        {
            const int limit = 100;
            for (int i = x - limit; i <= x + limit; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(1, countAxisX());
        }

        [TestMethod]
        public void AxisX_5_ManyGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(x, i);
            }
            Assert.AreEqual(count, countAxisX());
        }

        [TestMethod]
        public void AxisX_6_ManyAlmostGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(x - 1, i);
            }
            Assert.AreEqual(0, countAxisX());
        }

        [TestMethod]
        public void AxisX_7_ManyAlmostGoodPoints()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(x + 1, i);
            }
            Assert.AreEqual(0, countAxisX());
        }

        [TestMethod]
        public void AxisX_8_Duplicate()
        {
            const int count = 50;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(x, 0);
            }
            Assert.AreEqual(count, countAxisX());
        }
    }
}
