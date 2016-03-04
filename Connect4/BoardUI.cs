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
                Console.Write("    {0}   ", i + 1);
            }
            Console.Write("\n");

            for (int i = 0; i < 11; i++)
            {
                Console.Write("\n   |");

                if (i % 2 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string displayChar;
                        var position = new BoardPosition(Math.Abs((i/2)-6), j + 1);

                        if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player1Piece))
                        {
                            displayChar = "1";
                            Console.Write("   {0}   |", displayChar);
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player2Piece))
                        {
                            displayChar = "2";
                            Console.Write("   {0}   |", displayChar);
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player1PieceHighlighted))
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            displayChar = "1";
                            Console.Write("   {0}   |", displayChar);
                            Console.ResetColor();
                        }
                        else if (CustomComparer.PositionHistoryCompare(gameBoard.BoardHistory, position,
                            PositionHistory.Player2PieceHighlighted))
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            displayChar = "2";
                            Console.Write("   {0}   |", displayChar);
                            Console.ResetColor();
                        }
                        else
                        {
                            displayChar = "~";
                            Console.Write("   {0}   |", displayChar);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Console.Write("-------|");
                    }
                }
            }
            Console.Write("\n   ");
            for (int i = 0; i < 57; i++)
            {
                Console.Write("_");
            }
        }
    }
}
