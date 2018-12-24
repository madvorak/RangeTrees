using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class TestRectangle
    {
        private IRangeTree tree;
        private int xLow, xHigh;
        private int yLow, yHigh;

        [TestInitialize]
        public void Init()
        {
            tree = new DummyRange();
            xLow = 300;
            xHigh = 400;
            yLow = 600;
            yHigh = 900;
        }

        private int countRectangle()
        {
            return tree.RangeCount(xLow, xHigh, yLow, yHigh);
        }

        [TestMethod]
        public void Rectangle_01_EmptyTree()
        {
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_02_LeftBottom()
        {
            tree.Insert(xLow, yLow);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_03_LeftTop()
        {
            tree.Insert(xLow, yHigh);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_04_RightBottom()
        {
            tree.Insert(xHigh, yLow);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_05_RightTop()
        {
            tree.Insert(xHigh, yHigh);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_06_Bottom()
        {
            tree.Insert((xLow + xHigh) / 2, yLow);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_07_Top()
        {
            tree.Insert((xLow + xHigh) / 2, yHigh);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_08_Left()
        {
            tree.Insert(xLow, (yLow + yHigh) / 2);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_09_Right()
        {
            tree.Insert(xHigh, (yLow + yHigh) / 2);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_10_Centre()
        {
            tree.Insert((xLow + xHigh) / 2, (yLow + yHigh) / 2);
            Assert.AreEqual(1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_11_Under()
        {
            tree.Insert((xLow + xHigh) / 2, yLow - 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_12_Above()
        {
            tree.Insert((xLow + xHigh) / 2, yHigh + 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_13_TooLeft()
        {
            tree.Insert(xLow - 1, (yLow + yHigh) / 2);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_14_TooRight()
        {
            tree.Insert(xHigh + 1, (yLow + yHigh) / 2);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_15_TooLeftBottom()
        {
            tree.Insert(xLow - 1, yLow - 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_16_TooLeftTop()
        {
            tree.Insert(xLow - 1, yHigh + 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_17_TooRightBottom()
        {
            tree.Insert(xHigh + 1, yLow - 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_18_TooRightTop()
        {
            tree.Insert(xHigh + 1, yHigh + 1);
            Assert.AreEqual(0, countRectangle());
        }

        [TestMethod]
        public void Rectangle_19_ManyPointsLattice()
        {
            const int limit = 200;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - limit * xStep;
            int xStop = xHigh + limit * xStep;
            int yStart = yLow - limit * yStep;
            int yStop = yHigh + limit * yStep;

            for (int i = xStart; i <= xStop; i += xStep)
            {
                for (int j = yStart; j <= yStop; j += yStep)
                {
                    tree.Insert(i, j);
                }
            }
            Assert.AreEqual(4, countRectangle());
        }

        [TestMethod]
        public void Rectangle_20_ManyPointsRandom()
        {
            const int count = 10000;
            const int limit = 10;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - limit * xStep;
            int xStop = xHigh + limit * xStep;
            int yStart = yLow - limit * yStep;
            int yStop = yHigh + limit * yStep;
            Random rng = new Random();

            int inside = 0;
            for (int i = 0; i < count; i++)
            {
                int x = rng.Next(xStop - xStart) + xStart;
                int y = rng.Next(yStop - yStart) + yStart;
                tree.Insert(x, y);
                if (x >= xLow && x <= xHigh && y >= yLow && y <= yHigh)
                {
                    inside++;
                }
            }
            Assert.AreEqual(inside, countRectangle());
        }
    }
}
