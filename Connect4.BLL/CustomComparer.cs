using System.Collections.Generic;
using System.Linq;

namespace Connect4.BLL
{
    public static class CustomComparer
    {
        public static bool PositionHistoryCompare(Dictionary<BoardPosition, PositionHistory> boardHistory,
            BoardPosition position, PositionHistory history)
        {
            return (boardHistory.Keys.Where(
                key => key.RowPosition == position.RowPosition && key.ColumnPosition == position.ColumnPosition)
                .Select(key => boardHistory[key])).Any(historyToCompare => historyToCompare == history);
        }

        public static bool PositionHistoryCompare(Dictionary<BoardPosition, PositionHistory> boardHistory,
            BoardPosition position, PositionHistory history, int addToRow, int addToColumn)
        {
            return (boardHistory.Keys.Where(
                key =>
                    key.RowPosition == position.RowPosition + addToRow &&
                    key.ColumnPosition == position.ColumnPosition + addToColumn).Select(key => boardHistory[key])).Any(
                        historyToCompare => historyToCompare == history);
        }
    }
}
