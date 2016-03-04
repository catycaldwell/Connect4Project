using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Connect4.BLL;

namespace Connect4
{
    public static class Animations
    {
        public static void DisplayStartScreen()
        {
            Console.Write(@"_________                                     __       _____  
\_   ___ \  ____   ____   ____   ____   _____/  |_    /  |  | 
/    \  \/ /  _ \ /    \ /    \_/ __ \_/ ___\   __\  /   |  |_
\     \___(  <_> )   |  \   |  \  ___/\  \___|  |   /    ^   /
 \______  /\____/|___|  /___|  /\___  >\___  >__|   \____   | 
        \/            \/     \/     \/     \/            |__| ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PieceDrop(Board board, BoardPosition position,
            bool isPlayerOnesTurn)
        {
            var history = PositionHistory.Player2Piece;

            if (isPlayerOnesTurn)
            {
                history = PositionHistory.Player1Piece;
            }

            for (int i = 6; i > position.RowPosition; i--)
            {
                var fallingPiece = new BoardPosition(i, position.ColumnPosition);

                ReplaceHistory(board.BoardHistory, fallingPiece, history);
                BoardUI.DisplayGameBoard(board);
                System.Threading.Thread.Sleep(100);

                ReplaceHistory(board.BoardHistory, fallingPiece, PositionHistory.Empty);
                Console.Clear();
            }
        }

        public static void VictoryFlash()
        {

        }

        public static void DisplayExitScreen()
        {

        }

        private static void ReplaceHistory(Dictionary<BoardPosition, PositionHistory> boardHistory,
            BoardPosition position, PositionHistory historyToChangeTo)
        {
            foreach (
                var key in
                    boardHistory.Keys.Where(
                        key => key.RowPosition == position.RowPosition && key.ColumnPosition == position.ColumnPosition)
                )
            {
                boardHistory[key] = historyToChangeTo;
                break;
            }
        }
    }
}
