using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    /// <summary>
    /// Class holding an implementation of the game Minesweeper. The goal of the game is to uncover all the squares that
    /// do not contain mines without being "blown up" by clicking on a square with a mine underneath.
    /// </summary>
    public class MinesweeperMain
    {
        public static void Main(string[] args)
        {
            ///<remarks>The added constants help with managing the "Magic numbers"</remarks>
            const int MaxRevealedCells = 35;
            const int BoardRows = 5;
            const int BoardCols = 10;
            const int MinesCount = 15;

            string selectedCommand = string.Empty;
            char[,] whiteBoard = CreateWhiteBoard(BoardRows, BoardCols);
            char[,] minesBoard = CreateMinesBoard(BoardRows, BoardCols, MinesCount);
            char[] separators = new char[] { ',', ' ', ';', '/', '\\' };
            int movesCounter = 0;
            int rowIndex = 0;
            int colIndex = 0;
            bool newGame = true;
            bool maxRevealedCellsReached = false;
            bool mineHasBlown = false;
            List<ScoreRecord> champions = new List<ScoreRecord>(6);
    
            do
            {
                if (newGame)
                {
                    Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines." +
                    " Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
                    PrintBoard(whiteBoard);
                    newGame = false;
                }

                Console.Write("Enter row and column: ");
                selectedCommand = Console.ReadLine().Trim();
                var commandSplit = selectedCommand.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (commandSplit.Length > 1)
                {
                    rowIndex = int.Parse(commandSplit[0]);
                    colIndex = int.Parse(commandSplit[1]);

                    bool validRowIndex = IsInsideBoard(rowIndex, BoardRows);
                    bool validColIndex = IsInsideBoard(colIndex, BoardCols);

                    if (validRowIndex && validColIndex)
                    {
                        selectedCommand = "turn";
                    }
                }

                switch (selectedCommand)
                {
                    ///<remarks>
                    ///"Turn" is on top since it's the most common case
                    ///</remarks>
                    case "turn":
                        {
                            if (minesBoard[rowIndex, colIndex] != '*')
                            {
                                if (minesBoard[rowIndex, colIndex] == '-')
                                {
                                    RevealCell(whiteBoard, minesBoard, rowIndex, colIndex);
                                    movesCounter++;
                                }

                                if (MaxRevealedCells == movesCounter)
                                {
                                    maxRevealedCellsReached = true;
                                }
                                else
                                {
                                    PrintBoard(whiteBoard);
                                }
                            }
                            else
                            {
                                mineHasBlown = true;
                            }

                            break;
                        }

                    case "top":
                        {
                            PrintScoreBoard(champions);
                            break;
                        }

                    case "restart":
                        {
                            whiteBoard = CreateWhiteBoard(BoardRows, BoardCols);
                            minesBoard = CreateMinesBoard(BoardRows, BoardCols, MinesCount);
                            PrintBoard(whiteBoard);
                            mineHasBlown = false;
                            newGame = false;
                            break;
                        }

                    case "exit":
                        {
                            Console.WriteLine("Good bye!");
                            break;
                        }


                    default:
                        {
                            Console.WriteLine("\nIllegal move!\n");
                            break;
                        }
                }

                if (mineHasBlown || maxRevealedCellsReached)
                {
                    if (mineHasBlown)
                    {
                        PrintBoard(minesBoard);
                        Console.Write("\nBooooom! You were killed by a mine. You revealed {0} cells without mines." +
                            "\nPlease enter your name for the top scoreboard: ", movesCounter);
                    }

                    if (maxRevealedCellsReached)
                    {
                        Console.WriteLine("\nYou revealed all 35 cells.");
                        PrintBoard(minesBoard);
                        Console.WriteLine("Please enter your name for the top scoreboard: ");
                    }

                    string personName = Console.ReadLine();
                    AddChampionRecord(champions, personName, movesCounter);
                    PrintScoreBoard(champions);

                    whiteBoard = CreateWhiteBoard(BoardRows, BoardCols);
                    minesBoard = CreateMinesBoard(BoardRows, BoardCols, MinesCount);
                    movesCounter = 0;
                    mineHasBlown = false;
                    newGame = true;
                }
            }while (selectedCommand != "exit");

            Console.WriteLine("Made by Pavlin Panev 2010 - all rights reserved!");
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
        /// <summary>
        /// Method that indicates if the index is currently within the boundaries of the game board
        /// </summary>
        /// <param name="index">Cell on the game board</param>
        /// <param name="length">Indicates the length of the game board</param>
        /// <returns>Index that indicates if the next move is on the game board</returns>
        internal static bool IsInsideBoard(int index, int length)
        {
            bool isInside = true;
            if (index < 0 || index >= length)
            {
                isInside = false;
            }

            return isInside;
        }

        /// <summary>
        /// Method that reveals the whole game board after end of game
        /// </summary>
        /// <param name="board">Current game board</param>
        /// <param name="boomBoard">Placed mines</param>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        internal static void RevealCell(char[,] board, char[,] boomBoard, int rowIndex, int columnIndex)
        {
            char howManyBombs = CountMinesAroundCell(boomBoard, rowIndex, columnIndex);
            boomBoard[rowIndex, columnIndex] = howManyBombs;
            board[rowIndex, columnIndex] = howManyBombs;
        }

        /// <summary>
        /// Method that prints the game board
        /// </summary>
        /// <param name="board">Char array that constructs the game board</param>
        internal static string PrintBoard(char[,] board)
        {
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);
            StringBuilder result = new StringBuilder();

            result.AppendLine("\n    0 1 2 3 4 5 6 7 8 9");
            result.AppendLine("   ---------------------");
            for (int i = 0; i < boardRows; i++)
            {
                result.AppendFormat("{0} | ", i);
               
                for (int j = 0; j < boardColumns; j++)
                {
                    result.AppendFormat(string.Format("{0} ", board[i, j]));
                }
                result.AppendFormat("|");
                result.AppendLine();
            }
            result.AppendLine("   ---------------------\n");
            Console.Write(result.ToString());
            return result.ToString();
        }
        /// <summary>
        /// Scorekeeping methods region
        /// </summary>
        #region Champions' record adding and printing

        /// <summary>
        /// Method that adds the champion's score record to the scoreboard
        /// </summary>
        /// <param name="champions">List containing the names of the players</param>
        /// <param name="personName">A player's name</param>
        /// <param name="movesCount">Number of moves made</param>
        private static void AddChampionRecord(List<ScoreRecord> champions, string personName, int movesCount)
        {
            ScoreRecord newRecord = new ScoreRecord(personName, movesCount);
            if (champions.Count < 5)
            {
                champions.Add(newRecord);
            }
            else
            {
                for (int i = 0; i < champions.Count; i++)
                {
                    if (champions[i].ScorePoints < newRecord.ScorePoints)
                    {
                        champions.Insert(i, newRecord);
                        champions.RemoveAt(champions.Count - 1);
                        break;
                    }
                }
            }

            SortChampions(champions);
        }
        /// <summary>
        /// Method that sorts the players by their score results
        /// </summary>
        /// <param name="champions">List containing the names of the players</param>

        internal static void SortChampions(List<ScoreRecord> champions)
        {
            champions.Sort((scoreRecord1, scoreRecord2) => scoreRecord2.PersonName.CompareTo(scoreRecord1.PersonName));
            champions.Sort((scoreRecord1, scoreRecord2) => scoreRecord2.ScorePoints.CompareTo(scoreRecord1.ScorePoints));
        }
        /// <summary>
        /// Method that prints the score on the scoreboard
        /// </summary>
        /// <param name="topRecords">List with the players' scores</param>
        internal static string PrintScoreBoard(List<ScoreRecord> topRecords)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("\nScoreboard:");
          
            if (topRecords.Count > 0)
            {
                for (int i = 0; i < topRecords.Count; i++)
                {
                    result.AppendFormat("{0}. {1} --> {2} cells\n", i + 1, topRecords[i].PersonName, topRecords[i].ScorePoints);
                }
                result.AppendLine();
            }
            else
            {
                result.AppendLine("No records to display!\n");
            }

            Console.WriteLine(result);
            return result.ToString();
        }
        #endregion
        /// <summary>
        /// Game boards generating methods region
        /// </summary>

        #region  WhiteBoard and MinesBoard Generating and Calculating
        /// <summary>
        /// Method that creates an empty game board
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <returns>Char array as a game board</returns>
        internal static char[,] CreateWhiteBoard(int rows, int cols)
        {
            int boardRows = rows;
            int boardColumns = cols;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }
        /// <summary>
        /// Method that creates mines on the game board
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <param name="minesCount">Number of mines</param>
        /// <returns>Char array as a game board with mines</returns>
        internal static char[,] CreateMinesBoard(int rows, int cols, int minesCount)
        {
            int boardRows = rows;
            int boardColumns = cols;
            char[,] board = new char[boardRows, boardColumns];

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '-';
                }
            }

            List<int> randomNumbers = GenerateRandomNumbers(minesCount);

            PlaceMinesRandom(board, randomNumbers);

            return board;
        }
        /// <summary>
        /// Method that randomly places the mines. 
        /// </summary>
        /// <param name="board">Empty game board</param>
        /// <param name="randomNumbers">List of random numbers</param>
        internal static void PlaceMinesRandom(char[,] board, List<int> randomNumbers)
        {
            int boardColumns = board.GetLength(1);

            foreach (int number in randomNumbers)
            {
                int row = (number / boardColumns);
                int column = (number % boardColumns);
                if (column == 0 && number != 0)
                {
                    row--;
                    column = boardColumns;
                }
                else
                {
                    column++;
                }

                board[row, column - 1] = '*';
            }

        }
        /// <summary>
        /// Method that generates random numbers
        /// </summary>
        /// <param name="minesCount">Number of mines to be generated</param>
        /// <returns>Randomly generated mines</returns>
        internal static List<int> GenerateRandomNumbers(int minesCount)
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();
            while (randomNumbers.Count < minesCount)
            {
                int randomNumber = random.Next(50);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            return randomNumbers;
        }
        /// <summary>
        /// Method that places the mines on the game board
        /// </summary>
        /// <param name="board">Empty game board</param>
        internal static void SetMinesNumbersOnBoard(char[,] board)
        {
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    if (board[i, j] != '*')
                    {
                        char minesCount = CountMinesAroundCell(board, i, j);
                        board[i, j] = minesCount;
                    }
                }
            }
        }

        /// <summary>
        /// Method that returns the number of mines in neighbouring cells
        /// </summary>
        /// <param name="board">Current game board</param>
        /// <param name="rowIndex">Row index of the cell</param>
        /// <param name="columnIndex">Column index of the cell</param>
        /// <returns>Number of mines in neighbouring cells</returns>
        internal static char CountMinesAroundCell(char[,] board, int rowIndex, int columnIndex)
        {
            int minesCount = 0;
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);

            if (rowIndex - 1 >= 0)
            {
                if (board[rowIndex - 1, columnIndex] == '*')
                {
                    minesCount++;
                }
            }

            if (rowIndex + 1 < boardRows)
            {
                if (board[rowIndex + 1, columnIndex] == '*')
                {
                    minesCount++;
                }
            }

            if (columnIndex - 1 >= 0)
            {
                if (board[rowIndex, columnIndex - 1] == '*')
                {
                    minesCount++;
                }
            }

            if (columnIndex + 1 < boardColumns)
            {
                if (board[rowIndex, columnIndex + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((rowIndex - 1 >= 0) && (columnIndex - 1 >= 0))
            {
                if (board[rowIndex - 1, columnIndex - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((rowIndex - 1 >= 0) && (columnIndex + 1 < boardColumns))
            {
                if (board[rowIndex - 1, columnIndex + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((rowIndex + 1 < boardRows) && (columnIndex - 1 >= 0))
            {
                if (board[rowIndex + 1, columnIndex - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((rowIndex + 1 < boardRows) && (columnIndex + 1 < boardColumns))
            {
                if (board[rowIndex + 1, columnIndex + 1] == '*')
                {
                    minesCount++;
                }
            }

            return char.Parse(minesCount.ToString());
        }
        #endregion
    }
}