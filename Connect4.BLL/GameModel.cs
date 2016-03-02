using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BLL
{
    public class GameModel
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public Board GameBoard { get; set; }
    }
}
