using System;
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
            tree = new DummyTree();
            x = 5;
            y = 9;
        }

        private int countXY()
        {
            return tree.RangeCount(x, y, x, y);
        }

        [TestMethod]
        public void OnePoint_1_EmptyTree()
        {
            Assert.AreEqual(0, countXY());
        }

        [TestMethod]
        public void OnePoint_2_OneBadPoint()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(0, countXY());
        }

        [TestMethod]
        public void OnePoint_3_OneSemiBadPoint()
        {
            tree.Insert(x, 0);
            Assert.AreEqual(0, countXY());
        }

        [TestMethod]
        public void OnePoint_4_OneSemiBadPoint()
        {
            tree.Insert(0, y);
            Assert.AreEqual(0, countXY());
        }

        [TestMethod]
        public void OnePoint_5_OneGoodPoint()
        {
            tree.Insert(x, y);
            Assert.AreEqual(1, countXY());
        }
    }
}
