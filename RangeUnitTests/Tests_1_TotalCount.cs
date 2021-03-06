using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_1_TotalCount
    {
        private IRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
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
            const int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_4_DiagonalBackwards()
        {
            const int count = 42;
            for (int i = count; i > 0; i--)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_5_AntiDiagonal()
        {
            const int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, count - i);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_6_AntiDiagonalBackwards()
        {
            const int count = 42;
            for (int i = count; i > 0; i--)
            {
                tree.Insert(i, count - i);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_7_Duplicate()
        {
            const int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(0, 0);
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_8_RandomPoints()
        {
            Random rng = new Random();
            const int count = 100000;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(rng.Next(count), rng.Next(count));
            }
            Assert.AreEqual(count, countAll());
        }

        [TestMethod]
        public void TotalCount_9_RepeatedTest()
        {
            const int count = 10;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(count, countAll());
            Assert.AreEqual(count, countAll());
        }
    }
}
