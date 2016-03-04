using System;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            Animations.DisplayStartScreen();

            var gf = new GameFlow();
            gf.PlayGame();

            Console.ReadLine();
        }
    }
}
