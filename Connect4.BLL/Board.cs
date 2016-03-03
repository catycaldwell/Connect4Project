using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public class Board
    {
        // see Connect4BoardVisualization.txt for how positions are set up on the board

        public Dictionary<BoardPosition, PositionHistory> BoardHistory;

        public Board()
        {
            BoardHistory = new Dictionary<BoardPosition, PositionHistory>();
        }

        public PlaceGamePieceResponse PlaceGamePiece(int columnNumber, bool isPlayerOnesTurn)
        {
            var response = new PlaceGamePieceResponse();

            if (!IsValidColumn(columnNumber))
            {
                response.PositionStatus = PositionStatus.Invalid;
                return response;
            }

            // check for full column
            var topPositionInColumn = new BoardPosition(6, columnNumber);
            if (BoardHistory.ContainsKey(topPositionInColumn) &&
                BoardHistory[topPositionInColumn] != PositionHistory.Empty)
            {
                response.PositionStatus = PositionStatus.ColumnFull;
                return response;
            }

            var rowNumber = DetermineRowNumber(columnNumber);
            if (rowNumber == 0) //error, somehow a bad column input got through or board was drawn incorrectly
            {
                return null;
            }

            var position = AddPieceToBoard(columnNumber, rowNumber, isPlayerOnesTurn);

            // check for victory, victory communicated through positionstatus enum
            response = CheckForVictory(position, isPlayerOnesTurn);

            return response;
        }

        private static bool IsValidColumn(int columnNumber)
        {
            return columnNumber > 0 && columnNumber <= 7;
        }

        private int DetermineRowNumber(int columnNumber)
        {
            for (int i = 1; i < 7; i++)
            {
                var loopPosition = new BoardPosition(i, columnNumber);
                if (BoardHistory[loopPosition] == PositionHistory.Empty)
                {
                    return i;
                }
            }
            return 0;
        }

        private BoardPosition AddPieceToBoard(int column, int row, bool isPlayerOnesTurn)
        {
            //TODO do I have to remove old entry with the same key and positionhistory.empty??
            var boardPositionToAdd = new BoardPosition(row, column);
            BoardHistory.Remove(boardPositionToAdd);
            BoardHistory.Add(boardPositionToAdd,
                isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece);
            return boardPositionToAdd;
        }

        private PlaceGamePieceResponse CheckForVictory(BoardPosition position, bool isPlayerOnesTurn)
        {
            var response = new PlaceGamePieceResponse();
            var playerVictory =
                PlayerVictoryCheck(isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece,
                    position);

            response.PositionStatus = playerVictory ? PositionStatus.WinningMove : PositionStatus.Ok;
            return response;
        }

        private bool PlayerVictoryCheck(PositionHistory pieceToLookFor, BoardPosition position)
        {
            var piecesInARow = 1;
            // return true if victory

            //TODO fix these iterations into a loop
            //TODO get a list of winning positions

            // starting with the right/left check
            // check the right
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition + 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition + 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition + 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition + 2)] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition + 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition + 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then left
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition - 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition - 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition - 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition - 2)] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition, position.ColumnPosition - 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition, position.ColumnPosition - 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                return true;
            }
            piecesInARow = 1; // reset count for next line check

            // check diagonal /
            // check upper right
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 1, position.ColumnPosition + 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition + 1, position.ColumnPosition + 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 2, position.ColumnPosition + 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition + 2, position.ColumnPosition + 2)] ==
                    pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 3, position.ColumnPosition + 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition + 3, position.ColumnPosition + 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower left
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 1, position.ColumnPosition - 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition - 1, position.ColumnPosition - 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 2, position.ColumnPosition - 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition - 2, position.ColumnPosition - 2)] ==
                    pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 3, position.ColumnPosition - 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition - 3, position.ColumnPosition - 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                return true;
            }
            piecesInARow = 1;

            // check top/bottom line
            // check top
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 1, position.ColumnPosition)) &&
                BoardHistory[new BoardPosition(position.RowPosition + 1, position.ColumnPosition)] == pieceToLookFor)
            {
                piecesInARow++;
                if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 2, position.ColumnPosition)) &&
                    BoardHistory[new BoardPosition(position.RowPosition + 2, position.ColumnPosition)] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 3, position.ColumnPosition)) &&
                        BoardHistory[new BoardPosition(position.RowPosition + 3, position.ColumnPosition)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then bottom
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 1, position.ColumnPosition)) &&
                BoardHistory[new BoardPosition(position.RowPosition - 1, position.ColumnPosition)] == pieceToLookFor)
            {
                piecesInARow++;
                if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 2, position.ColumnPosition)) &&
                    BoardHistory[new BoardPosition(position.RowPosition - 2, position.ColumnPosition)] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 3, position.ColumnPosition)) &&
                        BoardHistory[new BoardPosition(position.RowPosition - 3, position.ColumnPosition)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                return true;
            }
            piecesInARow = 1;

            // check diagonal \
            // check upper left
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 1, position.ColumnPosition - 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition - 1, position.ColumnPosition - 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 2, position.ColumnPosition - 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition + 2, position.ColumnPosition - 2)] ==
                    pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition + 3, position.ColumnPosition - 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition + 3, position.ColumnPosition - 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower right
            if (BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 1, position.ColumnPosition + 1)) &&
                BoardHistory[new BoardPosition(position.RowPosition - 1, position.ColumnPosition + 1)] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 2, position.ColumnPosition + 2)) &&
                    BoardHistory[new BoardPosition(position.RowPosition - 2, position.ColumnPosition + 2)] ==
                    pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition(position.RowPosition - 3, position.ColumnPosition + 3)) &&
                        BoardHistory[new BoardPosition(position.RowPosition - 3, position.ColumnPosition + 3)] ==
                        pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                return true;
            }

            return false;
        }
    }
}
