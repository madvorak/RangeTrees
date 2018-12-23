using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestTotalCount
    {
        private IRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new DummyTree();
        }

        private int countAll()
        {
            return tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue);
        }

        [TestMethod]
        public void TotalCount_1_EmptyTree()
        {
            Assert.AreEqual(0, countAll());
        }

        [TestMethod]
        public void TotalCount_2_OnePoint()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(1, countAll());
        }

        [TestMethod]
        public void TotalCount_3_Diagonal()
        {
            int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_4_Duplicate()
        {
            int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(0, 0);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_5_RandomPoints()
        {
            Random rng = new Random();
            int count = 123456;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(rng.Next(count), rng.Next(count));
            }
            Assert.AreEqual(count, countAll());
        }
    }
}
