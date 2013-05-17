using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;
using System.Collections.Generic;
using System.IO;

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

        [TestMethod]
        public void TestMain_Input_Exit()
        {
            StringReader strReader = new StringReader("exit\n");
            Console.SetIn(strReader);
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            string[] arguments = null;
            MinesweeperMain.Main(arguments);

            string output = consoleOutput.ToString();
            string expected = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.\r\n\n    0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 | ? ? ? ? ? ? ? ? ? ? |\r\n1 | ? ? ? ? ? ? ? ? ? ? |\r\n2 | ? ? ? ? ? ? ? ? ? ? |\r\n3 | ? ? ? ? ? ? ? ? ? ? |\r\n4 | ? ? ? ? ? ? ? ? ? ? |\r\n   ---------------------\n\r\nEnter row and column: Good bye!\r\nMade by Pavlin Panev 2010 - all rights reserved!\r\nPress any key to exit.\r\n";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestMain_Input_Restart()
        {
            StringReader strReader = new StringReader("restart\nexit\n");
            Console.SetIn(strReader);
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            string[] arguments = null;
            MinesweeperMain.Main(arguments);

            string output = consoleOutput.ToString();
            string expected =
                "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.\r\n\n    0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 | ? ? ? ? ? ? ? ? ? ? |\r\n1 | ? ? ? ? ? ? ? ? ? ? |\r\n2 | ? ? ? ? ? ? ? ? ? ? |\r\n3 | ? ? ? ? ? ? ? ? ? ? |\r\n4 | ? ? ? ? ? ? ? ? ? ? |\r\n   ---------------------\n\r\nEnter row and column: \n    0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 | ? ? ? ? ? ? ? ? ? ? |\r\n1 | ? ? ? ? ? ? ? ? ? ? |\r\n2 | ? ? ? ? ? ? ? ? ? ? |\r\n3 | ? ? ? ? ? ? ? ? ? ? |\r\n4 | ? ? ? ? ? ? ? ? ? ? |\r\n   ---------------------\n\r\nEnter row and column: Good bye!\r\nMade by Pavlin Panev 2010 - all rights reserved!\r\nPress any key to exit.\r\n";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void TestMain_Input_Top()
        {
            StringReader strReader = new StringReader("top\nexit\n");
            Console.SetIn(strReader);
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            string[] arguments = null;
            MinesweeperMain.Main(arguments);

            string output = consoleOutput.ToString();
            string expected =
                "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.\r\n\n    0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 | ? ? ? ? ? ? ? ? ? ? |\r\n1 | ? ? ? ? ? ? ? ? ? ? |\r\n2 | ? ? ? ? ? ? ? ? ? ? |\r\n3 | ? ? ? ? ? ? ? ? ? ? |\r\n4 | ? ? ? ? ? ? ? ? ? ? |\r\n   ---------------------\n\r\nEnter row and column: \nScoreboard:\r\nNo records to display!\n\r\n\r\nEnter row and column: Good bye!\r\nMade by Pavlin Panev 2010 - all rights reserved!\r\nPress any key to exit.\r\n";
            
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void CreateWhiteBoardTest()
        {
            char[,] testBoard = MinesweeperMain.CreateWhiteBoard(5, 5);
            
            for (int row = 0; row < testBoard.GetLength(0); row++)
            {
                for (int col = 0; col < testBoard.GetLength(1); col++)
                {
                    Assert.AreEqual<char>('?', testBoard[row, col]);
                }
            }
        }

        [TestMethod]
        public void GenerateRandomNumbersTest()
        {
            List<int> expectedNumbers = MinesweeperMain.GenerateRandomNumbers(50);
            Assert.AreEqual<int>(50, expectedNumbers.Count);
        }

        [TestMethod]
        public void AddChampionRecordTest()
        {
            List<ScoreRecord> testList = new List<ScoreRecord>(3);

            MinesweeperMain.AddChampionRecord(testList, "Mitko", 10);
            MinesweeperMain.AddChampionRecord(testList, "Joro", 14);
            MinesweeperMain.AddChampionRecord(testList, "Eva", 20);

            string actual = MinesweeperMain.PrintScoreBoard(testList);

            Assert.AreEqual<string>("\nScoreboard:\r\n1. Eva --> 20 cells\n2. Joro --> 14 cells\n3. Mitko --> 10 cells\n\r\n", actual);
        }

        [TestMethod]
        public void AddChampionRecordTestMoreRecords()
        {
            List<ScoreRecord> testList = new List<ScoreRecord>(6);

            MinesweeperMain.AddChampionRecord(testList, "Mitko", 10);
            MinesweeperMain.AddChampionRecord(testList, "Joro", 14);
            MinesweeperMain.AddChampionRecord(testList, "Eva", 20);
            MinesweeperMain.AddChampionRecord(testList, "Marko", 10);
            MinesweeperMain.AddChampionRecord(testList, "Gosho", 14);
            MinesweeperMain.AddChampionRecord(testList, "Pesho", 20);
            MinesweeperMain.AddChampionRecord(testList, "Ivo", 10);
            MinesweeperMain.AddChampionRecord(testList, "Stoyan", 14);
            MinesweeperMain.AddChampionRecord(testList, "Petko", 20);

            string actual = MinesweeperMain.PrintScoreBoard(testList);

            Assert.AreEqual<string>("\nScoreboard:\r\n1. Eva --> 20 cells\n2. Petko --> 20 cells\n3. Pesho --> 20 cells\n4. Stoyan --> 14 cells\n5. Gosho --> 14 cells\n\r\n", actual);
        }

        [TestMethod]
        public void CountMinesAroundCellTestAllMines()
        {
            char[,] testBoard = new char[5, 10];
            for (int row = 0; row < testBoard.GetLength(0); row++)
            {
                for (int col = 0; col < testBoard.GetLength(1); col++)
                {
                    testBoard[row, col] = '*';
                }
            }

            var middle = MinesweeperMain.CountMinesAroundCell(testBoard, 2, 4);
            Assert.AreEqual<char>('8', middle);

            var start = MinesweeperMain.CountMinesAroundCell(testBoard, 0, 0);
            Assert.AreEqual<char>('3', start);

            var end = MinesweeperMain.CountMinesAroundCell(testBoard, 4, 9);
            Assert.AreEqual<char>('3', end);

            var left = MinesweeperMain.CountMinesAroundCell(testBoard, 0, 4);
            Assert.AreEqual<char>('5', left);

            var bottom = MinesweeperMain.CountMinesAroundCell(testBoard, 4, 4);
            Assert.AreEqual<char>('5', bottom);
        }

        [TestMethod]
        public void CountMinesAroundCellTestNoMines()
        {
            char[,] testBoard = new char[5, 10];
            for (int row = 0; row < testBoard.GetLength(0); row++)
            {
                for (int col = 0; col < testBoard.GetLength(1); col++)
                {
                    testBoard[row, col] = '-';
                }
            }

            var middle = MinesweeperMain.CountMinesAroundCell(testBoard, 2, 4);
            Assert.AreEqual<char>('0', middle);

            var start = MinesweeperMain.CountMinesAroundCell(testBoard, 0, 0);
            Assert.AreEqual<char>('0', start);

            var end = MinesweeperMain.CountMinesAroundCell(testBoard, 4, 9);
            Assert.AreEqual<char>('0', end);

            var left = MinesweeperMain.CountMinesAroundCell(testBoard, 0, 4);
            Assert.AreEqual<char>('0', left);

            var bottom = MinesweeperMain.CountMinesAroundCell(testBoard, 4, 4);
            Assert.AreEqual<char>('0', bottom);
        }
    }
}