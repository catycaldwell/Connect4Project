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

            AddPieceToBoard(columnNumber, rowNumber, isPlayerOnesTurn);

            // check for victory
            //CheckForVictory(columnNumber, response);

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

        private void AddPieceToBoard(int column, int row, bool isPlayerOnesTurn)
        {
            var boardPositionToAdd = new BoardPosition {ColumnPosition = column, RowPosition = row};
            BoardHistory.Add(boardPositionToAdd,
                isPlayerOnesTurn ? PositionHistory.Player1Piece : PositionHistory.Player2Piece);
        }
    }
}
