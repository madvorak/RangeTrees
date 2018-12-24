using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestOnePoint
    {
        private IRangeTree tree;
        private int x;
        private int y;

        [TestInitialize]
        public void Init()
        {
            tree = new CheatTree();
            x = 5;
            y = 9;
        }

        private int countMyPoint()
        {
            return tree.RangeCount(x, x, y, y);
        }

        [TestMethod]
        public void OnePoint_01_EmptyTree()
        {
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_02_OneBadPoint()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_03_OneSemiBadPoint()
        {
            tree.Insert(x, 0);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_04_OneSemiBadPoint()
        {
            tree.Insert(0, y);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_05_OneGoodPoint()
        {
            tree.Insert(x, y);
            Assert.AreEqual(1, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_06_OneAlmostGoodPoint()
        {
            tree.Insert(x + 1, y);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_07_OneAlmostGoodPoint()
        {
            tree.Insert(x - 1, y);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_08_OneAlmostGoodPoint()
        {
            tree.Insert(x, y + 1);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_09_OneAlmostGoodPoint()
        {
            tree.Insert(x, y - 1);
            Assert.AreEqual(0, countMyPoint());
        }

        [TestMethod]
        public void OnePoint_10_Lattice()
        {
            int limit = 100;
            for (int i = x - limit; i <= x + limit; i++)
            {
                for (int j = y - limit; j <= y + limit; j++)
                {
                    tree.Insert(i, j);
                }
            }
            Assert.AreEqual(1, countMyPoint());
        }
    }
}
