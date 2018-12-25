using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;
using System;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_7_Balance
    {
        private BBalphaRangeTree tree;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
        }

        private bool isOK()
        {
            return tree.isConsistent();
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
        
        [TestMethod]
        public void Balance_5_RebuildAll()
        {
            int count = 800;
            for (int i = 0; i < count; i++)
            {
                tree.Insert(i, i);
            }
            Assert.IsTrue(isOK());
        }

        [TestMethod]
        public void Balance_6_RebuildInRandomSequence()
        {
            int count = 5000;
            Random rng = new Random();
            for (int i = 0; i < count; i++)
            {
                tree.Insert(rng.Next(count), rng.Next(count));
            }
            Assert.IsTrue(isOK());
        }
    }
}
