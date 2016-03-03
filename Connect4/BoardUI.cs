using Connect4.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    internal class BoardUI
    {
        // I'm envisioning the board with row 1 on the bottom, up to row 6 on top

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
                        var position = new BoardPosition(j, i);

                        if (gameBoard.BoardHistory.ContainsKey(position) &&
                            gameBoard.BoardHistory[position] == PositionHistory.Player1Piece)
                        {
                            displayChar = "1";
                            Console.Write("   {0}   |", displayChar);
                        }
                        else if (gameBoard.BoardHistory.ContainsKey(position) &&
                            gameBoard.BoardHistory[position] == PositionHistory.Player2Piece)
                        {
                            displayChar = "2";
                            Console.Write("   {0}   |", displayChar);
                        }
                        else
                        {
                            displayChar = String.Format("{0},{1}", j, i);
                            Console.Write("   {0}   |", displayChar);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Console.Write("       |");
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
