using System.Collections.Generic;

namespace Connect4.BLL
{
    public class PlaceGamePieceResponse
    {
        public PositionStatus PositionStatus { get; set; }
        public BoardPosition BoardPosition { get; set; }
        public Dictionary<int,int> WinningPositionValues { get; set; } 
    }
}
