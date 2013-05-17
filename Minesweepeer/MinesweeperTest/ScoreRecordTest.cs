using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest
{
    [TestClass]
    public class ScoreRecordTest
    {
        [TestMethod]
        public void ConstructorTestWithTwoParametrs()
        {
            ScoreRecord sc = new ScoreRecord("Dimitar", 120);
            Assert.AreEqual<string>("Dimitar", sc.PersonName);
            Assert.AreEqual<int>(120, sc.ScorePoints);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorTestWithInvalidName()
        {
            string name = null;

            ScoreRecord sc = new ScoreRecord(name, 120);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTestWithEmptyName()
        {
            string name = string.Empty;

            ScoreRecord sc = new ScoreRecord(name, 120);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTestWithNegativeScore()
        {
            int score = -500;

            ScoreRecord sc = new ScoreRecord("Dimitar", score);
            
        }
    }
}