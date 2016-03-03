using Connect4.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            //var gf = new GameFlow();
            //gf.PlayGame();
            Board gameBoard = new Board();
            BoardUI.DisplayGameBoard(gameBoard);

            Console.ReadLine();
        }
    }
}
