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

        public static void VictoryFlash(Board board, Dictionary<int,int> winningMoveValues, bool playerOnesTurn)
        {
            ////TODO so, use the response object to get values for the wining positions. Replace them with highlighted PHistory objects back and forth a couple times.
            //// run 6 toggles, blink three times
            //for (int i = 0; i < 6; i++)
            //{
            ////for each matching position, replace with highlighted history
            //    foreach (var key in from key in board.BoardHistory.Keys
            //        from winningRow in winningMoveValues.Keys
            //        where key.RowPosition == winningRow
            //        from winningColumn in winningMoveValues.Values
            //        where key.ColumnPosition == winningColumn
            //        select key)
            //    {
            //        if (board.BoardHistory[key] == PositionHistory.Player1Piece)
            //        {
            //            board.BoardHistory[key] = PositionHistory.Player1PieceHighlighted;
            //        }
            //        if (board.BoardHistory[key] == PositionHistory.Player2Piece)
            //        {
            //            board.BoardHistory[key] = PositionHistory.Player2PieceHighlighted;
            //        }
            //        if (board.BoardHistory[key] == PositionHistory.Player1PieceHighlighted)
            //        {
            //            board.BoardHistory[key] = PositionHistory.Player1Piece;
            //        }
            //        if (board.BoardHistory[key] == PositionHistory.Player2PieceHighlighted)
            //        {
            //            board.BoardHistory[key] = PositionHistory.Player2Piece;
            //        }

            //        BoardUI.DisplayGameBoard(board);
            //        System.Threading.Thread.Sleep(300);
            //        Console.Clear();
            //    }
            //}
            var winningPositions = new List<BoardPosition>();
            foreach (var positionValue in winningMoveValues)
            {
                var winningPosition = new BoardPosition(positionValue.Key, positionValue.Value);

                winningPositions.Add(winningPosition);
            }

            for (int i = 0; i < 6; i++)
            {
                
            }

            BoardUI.DisplayGameBoard(board);
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
