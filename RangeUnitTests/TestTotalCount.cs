using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestTotalCount
    {
        private static Random rng = new Random();
        private IRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new DummyTree();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(0, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void TestMethod2()
        {
            tree.Insert(0, 0);
            Assert.AreEqual(1, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void TestMethod3()
        {
            int count = 42;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, i);
            }
            Assert.AreEqual(count, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void TestMethod4()
        {
            int count = 123456;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(rng.Next(count), rng.Next(count));
            }
            Assert.AreEqual(count, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }
    }
}
