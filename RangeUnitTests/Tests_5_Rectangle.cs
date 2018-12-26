using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTrees;

namespace RangeUnitTests
{
    [TestClass]
    public class Tests_5_Rectangle
    {
        private IRangeTree tree;
        private int xLow, xHigh;
        private int yLow, yHigh;

        [TestInitialize]
        public void Init()
        {
            tree = new BBalphaRangeTree();
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
            const int limit = 50;
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
        public void Rectangle_20_ManyPointsLatticeShuffled()
        {
            const int limit = 100;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - limit * xStep;
            int xStop = xHigh + limit * xStep;
            int yStart = yLow - limit * yStep;
            int yStop = yHigh + limit * yStep;
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();

            for (int i = xStart; i <= xStop; i += xStep)
            {
                for (int j = yStart; j <= yStop; j += yStep)
                {
                    points.Add(new Tuple<int, int>(i, j));
                }
            }

            Random rng = new Random();
            foreach (var point in points.OrderBy(_ => rng.Next()))
            {
                tree.Insert(point.Item1, point.Item2);
            }
            Assert.AreEqual(4, countRectangle());
        }

        [TestMethod]
        public void Rectangle_21_ManyPointsDiagonalShuffled()
        {
            const int limit = 50000;
            const int inner = 10;
            int xStep = (xHigh - xLow) / inner;
            int yStep = (yHigh - yLow) / inner;
            int xStart = xLow - limit * xStep;
            int xStop = xHigh + limit * xStep;
            int yStart = yLow - limit * yStep;
            int yStop = yHigh + limit * yStep;
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();

            int j = yStart;
            for (int i = xStart; i < xStop; i += xStep)
            {
                points.Add(new Tuple<int, int>(i, j));
                j += yStep;
            }
            Assert.AreEqual(yStop, j);

            Random rng = new Random();
            foreach (var point in points.OrderBy(_ => rng.Next()))
            {
                tree.Insert(point.Item1, point.Item2);
            }
            Assert.AreEqual(inner + 1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_22_ManyPointsAntiDiagonalShuffled()
        {
            const int limit = 50000;
            const int inner = 10;
            int xStep = (xHigh - xLow) / inner;
            int yStep = (yHigh - yLow) / inner;
            int xStart = xLow - limit * xStep;
            int xStop = xHigh + limit * xStep;
            int yStart = yLow - limit * yStep;
            int yStop = yHigh + limit * yStep;
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();

            int j = yStop;
            for (int i = xStart; i < xStop; i += xStep)
            {
                points.Add(new Tuple<int, int>(i, j));
                j -= yStep;
            }
            Assert.AreEqual(yStart, j);

            Random rng = new Random();
            foreach (var point in points.OrderBy(_ => rng.Next()))
            {
                tree.Insert(point.Item1, point.Item2);
            }
            Assert.AreEqual(inner + 1, countRectangle());
        }

        [TestMethod]
        public void Rectangle_23_ManyPointsRandom()
        {
            const int count = 100000;
            // pro 1 milion prvku trval 116 sekund
            // pro 65 tisic prvku trval 2.25 sekund
            // to je cca 52x dele
            // podle amortizovane asymptoticke casove slozitosti by to melo trvat cca 24x dele
            // to by asi mohlo byt horsim pomerem cache-missu
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

        [TestMethod]
        public void Rectangle_24_ManyPointsDenseRandom()
        {
            const int count = 50000;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - xStep / 2;
            int xStop = xHigh + xStep / 2;
            int yStart = yLow - yStep / 2;
            int yStop = yHigh + yStep / 2;
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

        [TestMethod]
        public void Rectangle_25_ManyPointsSuperDenseRandom()
        {
            const int count = 50000;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - xStep / 5;
            int xStop = xHigh + xStep / 5;
            int yStart = yLow - yStep / 5;
            int yStop = yHigh + yStep / 5;
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

        [TestMethod]
        public void Rectangle_26_ManyPointsModeratelyDenseRandom()
        {
            const int count = 50000;
            int xStep = xHigh - xLow;
            int yStep = yHigh - yLow;
            int xStart = xLow - xStep;
            int xStop = xHigh + xStep;
            int yStart = yLow - yStep;
            int yStop = yHigh + yStep;
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
