using System.Collections.Generic;
using System.Linq;

namespace Connect4.BLL
{
    public class Board
    {
        // see Connect4BoardVisualization.txt for how positions are set up on the board

        public Dictionary<BoardPosition, PositionHistory> BoardHistory;
        private readonly Dictionary<int, int> _winningPositionValues;

        public Board()
        {
            BoardHistory = new Dictionary<BoardPosition, PositionHistory>();
            //fill the board with empty positions
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    BoardHistory.Add(new BoardPosition(i, j), PositionHistory.Empty);
                }
            }
            _winningPositionValues = new Dictionary<int, int>();
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
            if (CustomComparer.PositionHistoryCompare(BoardHistory, topPositionInColumn, PositionHistory.Player1Piece) ||
                CustomComparer.PositionHistoryCompare(BoardHistory, topPositionInColumn, PositionHistory.Player2Piece))
            {
                response.PositionStatus = PositionStatus.ColumnFull;
                return response;
            }

            var rowNumber = DetermineRowNumber(columnNumber);
            if (rowNumber == 0) //error, somehow a bad column input got through or board was drawn incorrectly
            {
                return null;
            }

            var position = new BoardPosition(rowNumber, columnNumber);

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
                if (CustomComparer.PositionHistoryCompare(BoardHistory, loopPosition,
                    PositionHistory.Empty))
                {
                    return i;
                }
            }
            return 0;
        }

        public BoardPosition AddPieceToBoard(BoardPosition boardPositionToAdd, bool isPlayerOnesTurn)
        {
            CustomRemoveFromDictionary(boardPositionToAdd);
            BoardHistory.Add(boardPositionToAdd,
                isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece);
            return boardPositionToAdd;
        }

        private void CustomRemoveFromDictionary(BoardPosition position)
        {
            foreach (var key in BoardHistory.Keys.Where(key => key.RowPosition == position.RowPosition &&
                                                               key.ColumnPosition == position.ColumnPosition))
            {
                BoardHistory.Remove(key);
                break;
            }
        }

        private PlaceGamePieceResponse CheckForVictory(BoardPosition position, bool isPlayerOnesTurn)
        {
            var response = new PlaceGamePieceResponse();
            var playerVictory =
                PlayerVictoryCheck(isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece,
                    position);

            response.PositionStatus = playerVictory ? PositionStatus.WinningMove : PositionStatus.Ok;
            response.BoardPosition = position;
            response.WinningPositionValues = _winningPositionValues;
            return response;
        }

        private bool PlayerVictoryCheck(PositionHistory pieceToLookFor, BoardPosition position)
        {
            var piecesInARow = 1;

            // starting with the right/left check
            // check the right
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 1))
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition + 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 2))
                {
                    _winningPositionValues.Add(position.RowPosition, position.ColumnPosition + 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 3))
                    {
                        _winningPositionValues.Add(position.RowPosition, position.ColumnPosition + 3);
                        piecesInARow++;
                    }
                }
            }
            // then left
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -1))
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition - 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -2))
                {
                    _winningPositionValues.Add(position.RowPosition, position.ColumnPosition - 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -3))
                    {
                        _winningPositionValues.Add(position.RowPosition, position.ColumnPosition - 3);
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition);
                return true;
            }
            _winningPositionValues.Clear();
            piecesInARow = 1; // reset count for next line check

            // check diagonal /
            // check upper right
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, 1))
            {
                _winningPositionValues.Add(position.RowPosition + 1, position.ColumnPosition + 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, 2))
                {
                    _winningPositionValues.Add(position.RowPosition + 2, position.ColumnPosition + 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, 3))
                    {
                        _winningPositionValues.Add(position.RowPosition + 3, position.ColumnPosition + 3);
                        piecesInARow++;
                    }
                }
            }
            // then lower left
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, -1))
            {
                _winningPositionValues.Add(position.RowPosition - 1, position.ColumnPosition - 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, -2))
                {
                    _winningPositionValues.Add(position.RowPosition - 2, position.ColumnPosition - 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, -3))
                    {
                        _winningPositionValues.Add(position.RowPosition - 3, position.ColumnPosition - 3);
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition);
                return true;
            }
            _winningPositionValues.Clear();
            piecesInARow = 1;

            // check top/bottom line
            // check top
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, 0))
            {
                _winningPositionValues.Add(position.RowPosition + 1, position.ColumnPosition);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, 0))
                {
                    _winningPositionValues.Add(position.RowPosition + 2, position.ColumnPosition);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, 0))
                    {
                        _winningPositionValues.Add(position.RowPosition + 3, position.ColumnPosition);
                        piecesInARow++;
                    }
                }
            }
            // then bottom
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, 0))
            {
                _winningPositionValues.Add(position.RowPosition - 1, position.ColumnPosition);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, 0))
                {
                    _winningPositionValues.Add(position.RowPosition - 2, position.ColumnPosition);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, 0))
                    {
                        _winningPositionValues.Add(position.RowPosition - 3, position.ColumnPosition);
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition);
                return true;
            }
            _winningPositionValues.Clear();
            piecesInARow = 1;

            // check diagonal \
            // check upper left
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, -1))
            {
                _winningPositionValues.Add(position.RowPosition + 1, position.ColumnPosition - 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, -2))
                {
                    _winningPositionValues.Add(position.RowPosition + 2, position.ColumnPosition - 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, -3))
                    {
                        _winningPositionValues.Add(position.RowPosition + 3, position.ColumnPosition - 3);
                        piecesInARow++;
                    }
                }
            }
            // then lower right
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, 1))
            {
                _winningPositionValues.Add(position.RowPosition - 1, position.ColumnPosition + 1);
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, 2))
                {
                    _winningPositionValues.Add(position.RowPosition - 2, position.ColumnPosition + 2);
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, 3))
                    {
                        _winningPositionValues.Add(position.RowPosition - 3, position.ColumnPosition + 3);
                        piecesInARow++;
                    }
                }
            }

            if (piecesInARow >= 4)
            {
                _winningPositionValues.Add(position.RowPosition, position.ColumnPosition);
                return true;
            }

            _winningPositionValues.Clear();
            return false;
        }
    }
}
