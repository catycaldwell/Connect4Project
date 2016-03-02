using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public class PlaceGamePieceResponse
    {
        public PositionStatus PositionStatus { get; set; }
        public int ColumnNumber { get; set; }
        public bool IsWinningMove { get; set; }
    }
}
