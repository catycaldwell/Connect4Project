using Connect4.BLL;
using System;

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
            //TODO start splash screen

            if (_freshGame)
            {
                GetPlayerNames();
            }
            PlaceGamePieces();
            PromptPlayAgain();
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
                    TakeTurn(_model.Player1Name);
                }
                while (!_isPlayerOnesTurn && !_gameOver)
                {
                    TakeTurn(_model.Player2Name);
                }
            }
        }

        private void TakeTurn(string currentPlayerName) // only need a name, only one board. turn bool is global
        {
            string columnInput;

            bool inputIsValid;
            do
            {
                BoardUI.DisplayGameBoard(_model.GameBoard);

                Console.WriteLine();
                Console.Write("{0}, select a column : ", currentPlayerName);
                columnInput = Console.ReadLine();
                inputIsValid = IsValidColumnInput(columnInput);

                if (!inputIsValid)
                {
                    Console.WriteLine("That is not a valid column!");
                    Console.WriteLine("Press enter to try again");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (!inputIsValid);

            var column = int.Parse(columnInput);
            var response = _model.GameBoard.PlaceGamePiece(column, _isPlayerOnesTurn);
            // set the board position in the response, but don't set it until the animation?

            switch (response.PositionStatus)
            {
                case PositionStatus.Invalid:
                    Console.WriteLine("Invalid input! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case PositionStatus.ColumnFull:
                    Console.WriteLine("That column is full! Choose again (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case PositionStatus.Ok:
                    //TODO animation for piece falling into place
                    Console.Clear();
                    Animations.PieceDrop(_model.GameBoard, response.BoardPosition, _isPlayerOnesTurn);
                    if (_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = false;
                    }
                    else if (!_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = true;
                    }
                    Console.Clear();
                    break;

                case PositionStatus.WinningMove:
                    //TODO animation for piece falling into place
                    Console.Clear();
                    BoardUI.DisplayGameBoard(_model.GameBoard);
                    Console.WriteLine();
                    if (_isPlayerOnesTurn)
                    {
                        //TODO flashing victory animation
                        Console.WriteLine("Congratulations {0}, you won!!", _model.Player1Name);
                        Console.WriteLine("Press Enter");
                        Console.ReadLine();
                        Console.Clear();
                        _gameOver = true;
                    }
                    else if (!_isPlayerOnesTurn)
                    {
                        //TODO flashing victory animation
                        Console.WriteLine("Congratulations {0}, you won!!", _model.Player2Name);
                        Console.WriteLine("Press Enter");
                        Console.ReadLine();
                        Console.Clear();
                        _gameOver = true;
                    }
                    break;

                default:
                    Console.WriteLine("Invalid input! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }

        private static bool IsValidColumnInput(string input)
        {
            if (input.Length != 1)
            {
                return false;
            }
            int intInput;
            if (!int.TryParse(input, out intInput))
            {
                return false;
            }
            return intInput >= 1 || intInput <= 7;
        }

        private void PromptPlayAgain()
        {
            Console.Write("Play again? Type y or yes to play again. Type anything else to quit : ");
            var playAgain = Console.ReadLine();

            if (!String.IsNullOrEmpty(playAgain) && (playAgain.ToLower() == "y" || playAgain.ToLower() == "yes"))
            {
                Console.Clear();
                _freshGame = false;
                _gameOver = false;
                PlayGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing! Press Enter to quit.");
                //TODO exit splash screen?
            }
        }
    }
}
