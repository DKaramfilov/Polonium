using System;
using System.Collections.Generic;

// testvano e - ba4ka, ne pipaj!!!!!!!

namespace Minesweeper
{
    public class MinesweeperMain
    {
        static void Main(string[] args) //moved const on top, 
        {
            const int MaxRevealedCells = 35;
            const int BoardRows = 5;//added constant to manage "Magic" numbers
            const int BoardCols = 10;//added constant to manage "Magic" numbers
            const int MinesCount = 15;//same

            string selectedCommand = string.Empty;
            char[,] whiteBoard = CreateWhiteBoard(BoardRows, BoardCols);
            char[,] minesBoard = CreateMinesBoard(BoardRows, BoardCols,MinesCount);
            char[] separators = new char[]{',', ' ', ';', '/', '\\'}; //added separators for command separating
            int movesCounter = 0;
            int rowIndex = 0;
            int colIndex = 0;
            bool newGame = true; // rename from ??welcome??
            bool maxRevealedCellsReached = false;// rename from "flag"
            bool mineHasBlown = false; // rename for consistency
            List<ScoreRecord> champions = new List<ScoreRecord>(6);
            
            do
            {
                if (newGame)
                {
                    //Console.Clear();
                    Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines." +
                    " Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
                    PrintBoard(whiteBoard);
                    newGame = false;
                }

                Console.Write("Enter row and column: ");
                selectedCommand = Console.ReadLine().Trim();

                //refactored to use try catch block and add bool variables for easier understanding and to 
                //use separators chars for separating row and col of command
                if (selectedCommand.Length >= 3)  {
                    try
                    {
                        string[] commandSplit = selectedCommand.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        rowIndex = int.Parse(commandSplit[0]);
                        colIndex = int.Parse(commandSplit[1]);

                        bool validRowIndex = IsInsideBoard(rowIndex, BoardRows);
                        bool validColIndex = IsInsideBoard(colIndex, BoardCols);

                        if (validRowIndex && validColIndex)
                        {
                            selectedCommand = "turn";
                        }
                        
                    }
                    catch(FormatException ex)//explained why not handled
                    {
                        //It will be handled as an Illegal move in swich below
                    }
                }

                switch (selectedCommand)
                {
                    case "turn": //put turn on top of swich-case because it's most common case
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
                            whiteBoard = CreateWhiteBoard(BoardRows, BoardCols);//(DRY) maybe here we should have an Initilize()  method but for now left it like it is 
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

                if (mineHasBlown || maxRevealedCellsReached)//added one check in order to move 7 repeating lines out of next two if-s
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

                    string personName = Console.ReadLine();//7 lines moved outside of two consecutive if-s
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

        private static bool IsInsideBoard(int index, int length)
        {
            bool isInside = true;
            if (index < 0 || index >= length)
            {
                isInside = false;
            }

            return isInside;
        }//added method for checking if index is inside board

        private static void RevealCell(char[,] board,char[,] boomBoard, int rowIndex, int columnIndex)
        {
            char howManyBombs = CountMinesAroundCell(boomBoard, rowIndex, columnIndex);
            boomBoard[rowIndex, columnIndex] = howManyBombs;
            board[rowIndex, columnIndex] = howManyBombs;
        }//renamed method from.....?

        private static void PrintBoard(char[,] board)
        {
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < boardRows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < boardColumns; j++)
                {
                    Console.Write(string.Format("{0} ",board[i, j]));
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine("   ---------------------\n");
        }

        //added a region for better placement of methods connected to scorekeeping
        #region Champions' record adding and printing 

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

        private static void SortChampions(List<ScoreRecord> champions)
        {
            champions.Sort((scoreRecord1, scoreRecord2) => scoreRecord2.PersonName.CompareTo(scoreRecord1.PersonName));
            champions.Sort((scoreRecord1, scoreRecord2) => scoreRecord2.ScorePoints.CompareTo(scoreRecord1.ScorePoints));
        }

        private static void PrintScoreBoard(List<ScoreRecord> topRecords)
        {
            Console.WriteLine("\nScoreboard:");
            if (topRecords.Count > 0)
            {
                for (int i = 0; i < topRecords.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} cells", i + 1, topRecords[i].PersonName, topRecords[i].ScorePoints);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No records to display!\n");
            }
        }
        #endregion

        //added a region for white- and mines-boards generating 
        #region  WhiteBoard and MinesBoard Generating and Calculating

        private static char[,] CreateWhiteBoard(int rows, int cols)
        {
            int boardRows = rows;
            int boardColumns = cols;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i,j] = '?';
                }
            }

            return board;
        }

        private static char[,] CreateMinesBoard(int rows, int cols, int minesCount)
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

        private static void PlaceMinesRandom(char[,] board, List<int> randomNumbers)//added method for easier reading
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

        private static List<int> GenerateRandomNumbers(int minesCount)//added method for easier reading
        {
            Random random = new Random(); // moved outside of while loop
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

        private static void SetMinesNumbersOnBoard(char[,] board) // renamed ,method to explain
        {
            int boardRows = board.GetLength(0);
            int boardColumns = board.GetLength(1);

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    if (board[i,j] != '*')
                    {
                        char minesCount = CountMinesAroundCell(board, i, j);
                        board[i, j] = minesCount;
                    }
                }
            }
        }

        /// <summary>
        /// Counts mines around cell and returns their count
        /// </summary>
        /// <param name="board"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private static char CountMinesAroundCell(char[,] board, int rowIndex, int columnIndex) //renamed method to explain
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
