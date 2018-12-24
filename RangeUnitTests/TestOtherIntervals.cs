using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestOtherIntervals
    {
        private IRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
            tree.Insert(-7, -7);
            tree.Insert(-7, 7);
            tree.Insert(7, -7);
            tree.Insert(7, 7);
        }

        [TestMethod]
        public void OtherIntervals_1_Quadrant()
        {
            Assert.AreEqual(1, tree.RangeCount(int.MinValue, 0, int.MinValue, 0));
        }

        [TestMethod]
        public void OtherIntervals_2_Quadrant()
        {
            Assert.AreEqual(1, tree.RangeCount(int.MinValue, 0, 0, int.MaxValue));
        }

        [TestMethod]
        public void OtherIntervals_3_Quadrant()
        {
            Assert.AreEqual(1, tree.RangeCount(0, int.MaxValue, int.MinValue, 0));
        }

        [TestMethod]
        public void OtherIntervals_4_Quadrant()
        {
            Assert.AreEqual(1, tree.RangeCount(0, int.MaxValue, 0, int.MaxValue));
        }

        [TestMethod]
        public void OtherIntervals_5_HalfPlane()
        {
            Assert.AreEqual(2, tree.RangeCount(int.MinValue, 0, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void OtherIntervals_6_HalfPlane()
        {
            Assert.AreEqual(2, tree.RangeCount(0, int.MaxValue, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void OtherIntervals_7_HalfPlane()
        {
            Assert.AreEqual(2, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, 0));
        }

        [TestMethod]
        public void OtherIntervals_8_HalfPlane()
        {
            Assert.AreEqual(2, tree.RangeCount(int.MinValue, int.MaxValue, 0, int.MaxValue));
        }

        [TestMethod]
        public void OtherIntervals_9_FullPlane()
        {
            Assert.AreEqual(4, tree.RangeCount(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue));
        }
    }
}
