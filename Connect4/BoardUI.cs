using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class BoardUI
    {
        // I'm envisioning the board with row 1 on the bottom, up to row 6 on top

        public static void DisplayGameBoard()
        {
            Console.Write("   ");
            for (int i = 0; i < 7; i++)
            {
                Console.Write("    {0}   ", i+1);
            }
            Console.Write("\n");

            for (int i = 0; i < 11; i++)
            {
                Console.Write("\n   |");

                if (i % 2 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string displayChar = "~";
                        Console.Write("   {0}   |", displayChar);
                        // will be a check here for pieces, and changed displaychar accordingly
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
