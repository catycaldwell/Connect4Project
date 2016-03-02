using Connect4.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class GameFlow
    {
        private GameModel _model = new GameModel();

        public void PlayGame()
        {
            GetPlayersNames();
        }

        private void GetPlayersNames()
        {
            Console.Write("Player 1, What is your name? : ");
            _model.Player1Name = Console.ReadLine();
            if (_model.Player1Name == String.Empty)
            {
                _model.Player1Name = "Player1";
            }

            Console.Write("\n\nPlayer 2, what is your name? : ");
            _model.Player2Name = Console.ReadLine();
            if (_model.Player2Name == String.Empty)
            {
                _model.Player2Name = "Player2";
            }

            Console.Clear();
        }
    }
}
