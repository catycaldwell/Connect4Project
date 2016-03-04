using System.Collections.Generic;

namespace Connect4.BLL
{
    public class Board
    {
        // see Connect4BoardVisualization.txt for how positions are set up on the board

        public Dictionary<BoardPosition, PositionHistory> BoardHistory;

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
                if (CustomComparer.PositionHistoryCompare(BoardHistory, loopPosition,
                    PositionHistory.Empty))
                {
                    return i;
                }
            }
            return 0;
        }

        private BoardPosition AddPieceToBoard(int column, int row, bool isPlayerOnesTurn)
        {
            var boardPositionToAdd = new BoardPosition(row, column);
            CustomRemoveFromDictionary(boardPositionToAdd);
            BoardHistory.Add(boardPositionToAdd,
                isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece);
            return boardPositionToAdd;
        }

        private void CustomRemoveFromDictionary(BoardPosition position)
        {
            foreach (var key in BoardHistory.Keys)
            {
                if (key.RowPosition == position.RowPosition &&
                    key.ColumnPosition == position.ColumnPosition)
                {
                    BoardHistory.Remove(key);
                    break;
                }
            }
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
            //TODO fix these iterations into a loop

            // starting with the right/left check
            // check the right
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, 3))
                    {
                        piecesInARow++;
                    }
                }
            }
            // then left
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 0, -3))
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
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, 1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, 2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, 3))
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower left
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, -1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, -2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, -3))
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
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, 0))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, 0))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, 0))
                    {
                        piecesInARow++;
                    }
                }
            }
            // then bottom
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, 0))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, 0))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, 0))
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
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 1, -1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 2, -2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, 3, -3))
                    {
                        piecesInARow++;
                    }
                }
            }
            // then lower right
            if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -1, 1))
            {
                piecesInARow++;

                if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -2, 2))
                {
                    piecesInARow++;

                    if (CustomComparer.PositionHistoryCompare(BoardHistory, position, pieceToLookFor, -3, 3))
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
