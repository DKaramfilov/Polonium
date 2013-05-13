﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;
using System.Collections.Generic;

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

        [TestMethod]
        public void PrintBoardTest()
        {
            char[,] testBoard = new char[10, 10];
            for (int row = 0; row < testBoard.GetLength(0); row++)
            {
                for (int col = 0; col < testBoard.GetLength(1); col++)
                {
                    testBoard[row, col] = '#';
                }
            }
            string result = MinesweeperMain.PrintBoard(testBoard);

            Assert.AreEqual<string>(
                "\n    0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 | # # # # # # # # # # |\r\n1 | # # # # # # # # # # |\r\n2 | # # # # # # # # # # |\r\n3 | # # # # # # # # # # |\r\n4 | # # # # # # # # # # |\r\n5 | # # # # # # # # # # |\r\n6 | # # # # # # # # # # |\r\n7 | # # # # # # # # # # |\r\n8 | # # # # # # # # # # |\r\n9 | # # # # # # # # # # |\r\n   ---------------------\n\r\n",
                result);
        }

        [TestMethod]
        public void PrintScoreBoardTest()
        {
            List<ScoreRecord> testRecords = new List<ScoreRecord>();
            testRecords.Add(new ScoreRecord("Mitko", 230));
            testRecords.Add(new ScoreRecord("Gosho", 220));
            testRecords.Add(new ScoreRecord("Pesho", 210));
            testRecords.Add(new ScoreRecord("Joro", 260));
            string result = MinesweeperMain.PrintScoreBoard(testRecords);

            Assert.AreEqual<string>(
                "\nScoreboard:\r\n1. Mitko --> 230 cells\n2. Gosho --> 220 cells\n3. Pesho --> 210 cells\n4. Joro --> 260 cells\n\r\n",
                result);
        }

        [TestMethod]
        public void PrintScoreBoardTestNoRecords()
        {
            List<ScoreRecord> testRecords = new List<ScoreRecord>();
            
            string result = MinesweeperMain.PrintScoreBoard(testRecords);

            Assert.AreEqual<string>(
                "\nScoreboard:\r\nNo records to display!\n\r\n",
                result);
        }
    }
}