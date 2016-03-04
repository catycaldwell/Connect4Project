namespace Connect4.BLL
{
    public class BoardPosition
    {
        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }

        public BoardPosition(int row, int column)
        {
            RowPosition = row;
            ColumnPosition = column;
        }
    }
}
