using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest
{
    [TestClass]
    public class MinesweeperMainTest
    {
        [TestMethod]
        public void IsInsideBoardTestOutsideIndex()
        {
            var result = MinesweeperMain.IsInsideBoard(100, 10);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsInsideBoardTestNegativeIndex()
        {
            var result = MinesweeperMain.IsInsideBoard(-1, 100);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsInsideBoardTestValidData()
        {
            var result = MinesweeperMain.IsInsideBoard(1, 100);
            Assert.IsTrue(result);
        }
    }
}
