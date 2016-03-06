using Connect4.BLL;
using System;

namespace Connect4
{
    internal class BoardUI
    {
        // see Connect4BoardVisualization.txt for how the boardpositions are layed out on the gameboard.

        public static void DisplayGameBoard(Board gameBoard)
        {
            Console.Write("   ");
            for (int i = 0; i < 7; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("    {0}   ", i + 1);
                Console.ResetColor();
            }
            Console.Write("\n");

            for (int i = 0; i < 11; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n   |");
                Console.ResetColor();

                if (i % 2 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string displayChar;
                        var position = new BoardPosition(Math.Abs((i/2)-6), j + 1);

                        if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player1Piece))
                        {
                            displayChar = "X";
                            Console.Write("   ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(displayChar);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("   |");
                            Console.ResetColor();
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player2Piece))
                        {
                            displayChar = "X";
                            Console.Write("   ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(displayChar);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("   |");
                            Console.ResetColor();
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player1PieceHighlighted))
                        {
                            displayChar = "X";
                            Console.Write("   ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(displayChar);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("   |");
                            Console.ResetColor();
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player2PieceHighlighted))
                        {
                            displayChar = "X";
                            Console.Write("   ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(displayChar);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("   |");
                            Console.ResetColor();
                        }
                        else
                        {
                            displayChar = " ";
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("   {0}   |", displayChar);
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-------|");
                        Console.ResetColor();
                    }
                }
            }
            Console.Write("\n   ");
            for (int i = 0; i < 57; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("_");
                Console.ResetColor();
            }
        }
    }
}
