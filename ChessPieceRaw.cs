using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    abstract class ChessPieceRaw:ChessPiece
    {
        public Team Team { get; }      //which team this piece belongs

        protected string GetIconPrefix()    //based on team, white or black (icons are named as wrook or brook)
        {
            return Team == Team.Black ? "b" : "w";
        }
        public abstract string GetIcon();
        public GridCell Cell { get; set; }

        protected ChessPieceRaw(Team team)
        {
            Team = team;
        }
    }
}
