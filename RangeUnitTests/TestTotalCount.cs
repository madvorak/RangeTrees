using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestTotalCount
    {
        [TestMethod]
        public void TestMethod1()
        {
            IRangeTree range = new BBalphaRangeTree();
            Assert.AreEqual(0, range.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void TestMethod2()
        {
            IRangeTree range = new BBalphaRangeTree();
            range.Insert(0, 0);
            Assert.AreEqual(1, range.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void TestMethod3()
        {
            IRangeTree range = new BBalphaRangeTree();
            int count = 42;
            for (int i = 0; i < count; i++)
            {
                range.Insert(i, i);
            }
            Assert.AreEqual(count, range.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }
    }
}
