using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public static class CustomComparer
    {
        public static bool PositionHistoryCompare(Dictionary<BoardPosition, PositionHistory> boardHistory, BoardPosition position, PositionHistory history)
        {
            foreach (var key in boardHistory.Keys)
            {
                if (key.RowPosition == position.RowPosition &&
                    key.ColumnPosition == position.ColumnPosition)
                {
                    var historyToCompare = boardHistory[key];

                    if (historyToCompare == history)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool PositionHistoryCompare(Dictionary<BoardPosition, PositionHistory> boardHistory, BoardPosition position, PositionHistory history, int addToRow, int addToColumn)
        {
            foreach (var key in boardHistory.Keys)
            {
                if (key.RowPosition == position.RowPosition + addToRow &&
                    key.ColumnPosition == position.ColumnPosition + addToColumn)
                {
                    var historyToCompare = boardHistory[key];

                    if (historyToCompare == history)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
