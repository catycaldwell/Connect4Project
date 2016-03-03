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

        private bool _isPlayerOnesTurn = true;
        private bool _gameOver;
        private bool _freshGame = true;

        public void PlayGame()
        {
            _model.GameBoard = new Board();

            if (_freshGame)
            {
                GetPlayerNames();
            }
            PlaceGamePieces();
        }

        private void GetPlayerNames()
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

        private void PlaceGamePieces()
        {
            while (!_gameOver)
            {
                while (_isPlayerOnesTurn && !_gameOver)
                {
                    TakeTurn(_model.Player1Name, _model.GameBoard, _model.Player2Name);
                }
                while (!_isPlayerOnesTurn && !_gameOver)
                {
                    TakeTurn(_model.Player2Name, _model.GameBoard, _model.Player1Name);
                }
            }
        }

        private void TakeTurn(string currentPlayerName, Board board, string opposingPlayerName)
        {
            string columnInput;

            bool inputIsValid;
            do
            {
                BoardUI.DisplayGameBoard(board);

                Console.WriteLine();
                Console.Write("{0}, select a column : ", currentPlayerName);
                columnInput = Console.ReadLine();
                //TODO method to validate columnInput and set input to valid
                inputIsValid = true;

                if (!inputIsValid)
                {
                    Console.WriteLine("That is not a valid column!");
                    Console.WriteLine("Press enter to try again");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (!inputIsValid);

            var column = int.Parse(columnInput);
            var response = board.PlaceGamePiece(column, true); //TODO fix this to input a players turn

            //TODO finish writing this method
        }
    }
}
