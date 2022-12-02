using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WFSTests
{
    [TestClass]
    public class WFSTests
    {
        [TestMethod]
        public void TestSampleInput1()
        {
            // First Map test
            char[,] map = new char[4, 7]
            {
                {'#','#','#','#','#','#','#'},
                {'#','P','.','G','T','G','#'},
                {'#','.','.','T','G','G','#'},
                {'#','#','#','#','#','#','#'}
            };

            WFS w = new WFS();

            int result = w.GetMaxGold(map, 7, 4);
            int expected = 1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSampleInput2()
        {
            // First Map test
            char[,] map = new char[6, 8]
            {
                {'#','#','#','#','#','#','#','#'},
                {'#','.','.','.','G','T','G','#'},
                {'#','.','.','P','G','.','G','#'},
                {'#','.','.','.','G','#','G','#'},
                {'#','.','.','T','G','.','G','#'},
                {'#','#','#','#','#','#','#','#'}
            };

            WFS w = new WFS();

            int result = w.GetMaxGold(map, 8, 6);
            int expected = 4;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEdgeCase()
        {
            // First Map test
            char[,] map = new char[4, 4]
            {
                {'#','#','#','#'},
                {'#','P','T','#'},
                {'#','G','.','#'},
                {'#','#','#','#'}
            };

            WFS w = new WFS();

            int result = w.GetMaxGold(map, 4, 4);
            int expected = 0;

            Assert.AreEqual(expected, result);
        }

    }
}
