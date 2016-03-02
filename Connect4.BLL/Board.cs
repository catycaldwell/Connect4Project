using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public class Board
    {
        // I'm envisioning the board with row 1 on the bottom, up to row 6 on top

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
            var topPositionInColumn = new BoardPosition {ColumnPosition = columnNumber, RowPosition = 6};
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
                var loopPosition = new BoardPosition {ColumnPosition = columnNumber, RowPosition = i};
                if (BoardHistory[loopPosition] == PositionHistory.Empty)
                {
                    return i;
                }
            }
            return 0;
        }

        private BoardPosition AddPieceToBoard(int column, int row, bool isPlayerOnesTurn)
        {
            // do I have to remove old entry with the same key and positionhistory.empty??
            var boardPositionToAdd = new BoardPosition {ColumnPosition = column, RowPosition = row};
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

            // starting with the top, check 3 iterations
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition + 1,
                    RowPosition = position.RowPosition
                }) &&
                BoardHistory[
                    new BoardPosition {ColumnPosition = position.ColumnPosition + 1, RowPosition = position.RowPosition}
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition + 2,
                        RowPosition = position.RowPosition
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 2,
                            RowPosition = position.RowPosition
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 3,
                            RowPosition = position.RowPosition
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition + 3,
                                RowPosition = position.RowPosition
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then upper right
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition + 1,
                    RowPosition = position.RowPosition + 1
                }) &&
                BoardHistory[
                    new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition + 1,
                        RowPosition = position.RowPosition + 1
                    }
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition + 2,
                        RowPosition = position.RowPosition + 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 2,
                            RowPosition = position.RowPosition + 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 3,
                            RowPosition = position.RowPosition + 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition + 3,
                                RowPosition = position.RowPosition + 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then right
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition,
                    RowPosition = position.RowPosition + 1
                }) &&
                BoardHistory[
                    new BoardPosition {ColumnPosition = position.ColumnPosition, RowPosition = position.RowPosition + 1}
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition,
                        RowPosition = position.RowPosition + 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition,
                            RowPosition = position.RowPosition + 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition,
                            RowPosition = position.RowPosition + 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition,
                                RowPosition = position.RowPosition + 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower right
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition - 1,
                    RowPosition = position.RowPosition + 1
                }) &&
                BoardHistory[
                    new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition - 1,
                        RowPosition = position.RowPosition + 1
                    }
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition - 2,
                        RowPosition = position.RowPosition + 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 2,
                            RowPosition = position.RowPosition + 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 3,
                            RowPosition = position.RowPosition + 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition - 3,
                                RowPosition = position.RowPosition + 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then bottom
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition - 1,
                    RowPosition = position.RowPosition
                }) &&
                BoardHistory[
                    new BoardPosition {ColumnPosition = position.ColumnPosition - 1, RowPosition = position.RowPosition}
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition - 2,
                        RowPosition = position.RowPosition
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 2,
                            RowPosition = position.RowPosition
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 3,
                            RowPosition = position.RowPosition
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition - 3,
                                RowPosition = position.RowPosition
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower left
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition - 1,
                    RowPosition = position.RowPosition - 1
                }) &&
                BoardHistory[
                    new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition - 1,
                        RowPosition = position.RowPosition - 1
                    }
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition - 2,
                        RowPosition = position.RowPosition - 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 2,
                            RowPosition = position.RowPosition - 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition - 3,
                            RowPosition = position.RowPosition - 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition - 3,
                                RowPosition = position.RowPosition - 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // then left
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition,
                    RowPosition = position.RowPosition - 1
                }) &&
                BoardHistory[
                    new BoardPosition {ColumnPosition = position.ColumnPosition, RowPosition = position.RowPosition - 1}
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition,
                        RowPosition = position.RowPosition - 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition,
                            RowPosition = position.RowPosition - 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition,
                            RowPosition = position.RowPosition - 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition,
                                RowPosition = position.RowPosition - 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }
            // than upper left
            if (
                BoardHistory.ContainsKey(new BoardPosition
                {
                    ColumnPosition = position.ColumnPosition + 1,
                    RowPosition = position.RowPosition - 1
                }) &&
                BoardHistory[
                    new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition + 1,
                        RowPosition = position.RowPosition - 1
                    }
                    ] == pieceToLookFor)
            {
                piecesInARow++;
                if (
                    BoardHistory.ContainsKey(new BoardPosition
                    {
                        ColumnPosition = position.ColumnPosition + 2,
                        RowPosition = position.RowPosition - 2
                    }) &&
                    BoardHistory[
                        new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 2,
                            RowPosition = position.RowPosition - 2
                        }
                        ] == pieceToLookFor)
                {
                    piecesInARow++;
                    if (
                        BoardHistory.ContainsKey(new BoardPosition
                        {
                            ColumnPosition = position.ColumnPosition + 3,
                            RowPosition = position.RowPosition - 3
                        }) &&
                        BoardHistory[
                            new BoardPosition
                            {
                                ColumnPosition = position.ColumnPosition + 3,
                                RowPosition = position.RowPosition - 3
                            }
                            ] == pieceToLookFor)
                    {
                        piecesInARow++;
                    }
                }
            }

            // finally, return true if 4 in a row (or more)
            return piecesInARow >= 4;
        }
    }
}
