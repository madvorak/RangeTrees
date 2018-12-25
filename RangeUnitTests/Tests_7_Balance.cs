using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;
using System.Reflection;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_7_Balance
    {
        private IRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
        }

        private bool isOK()
        {
            FieldInfo info = typeof(BBalphaRangeTree).GetField("root");
            object root = info.GetValue(tree);
            // TODO access to internal classes and private fields required
            return false;
        }

        [TestMethod]
        public void Balance_1_EmptyTree()
        {
            Assert.IsTrue(isOK());
        }

        [TestMethod]
        public void Balance_2_RebuildNothing()
        {
            tree.Insert(2, 2);
            tree.Insert(1, 1);
            tree.Insert(4, 4);
            tree.Insert(3, 3);
            Assert.IsTrue(isOK());
        }
        [TestMethod]
        public void Balance_3_RebuildY()
        {
            tree.Insert(2, 1);
            tree.Insert(1, 2);
            tree.Insert(4, 3);
            tree.Insert(3, 4);
            Assert.IsTrue(isOK());
        }
        [TestMethod]
        public void Balance_4_RebuildX()
        {
            tree.Insert(1, 2);
            tree.Insert(2, 1);
            tree.Insert(3, 4);
            tree.Insert(4, 3);
            Assert.IsTrue(isOK());
        }
    }
}
