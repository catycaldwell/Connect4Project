using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public static class CustomComparer
    {
        public static bool PositionHistoryCompareCheckKey(Dictionary<BoardPosition, PositionHistory> boardHistory, BoardPosition position, PositionHistory history)
        {
            //iterate through keys and compare each value (is there a better way?)
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

        public static bool PositionHistoryCompareRow(Dictionary<BoardPosition, PositionHistory> boardHistory, PositionHistory history, int row)
        {
            return false; //TODO finish this method
        }

        public static bool PositionHistoryCompareColumn(Dictionary<BoardPosition, PositionHistory> boardHistory, PositionHistory history, int column)
        {
            return false; //TODO finish this method
        }

        public static bool PositionHistoryCompareRowAndColumn(Dictionary<BoardPosition, PositionHistory> boardHistory, PositionHistory history, int row, int column)
        {
            return false; //TODO finish this method
        }
    }
}
