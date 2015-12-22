using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    //base class for all chess pieces
    abstract class ChessPiece
    {
        public Team Team { get; set; }      //which team this piece belongs

        protected string GetIconPrefix()    //based on team, white or black (icons are named as wrook or brook)
        {
            return Team == Team.Black ? "b" : "w";
        }
        public abstract string GetIcon();
    }
}
