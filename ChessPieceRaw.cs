using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    abstract class ChessPieceRaw:ChessPiece
    {
        private ChessPiece[,] _board;
        private Team team;

        public Team Team { get; }      //which team this piece belongs

        protected string GetIconPrefix()    //based on team, white or black (icons are named as wrook or brook)
        {
            return Team == Team.Black ? "b" : "w";
        }
        public abstract string GetIcon();
        public GridCell Cell { get; set; }
        public abstract IEnumerable<GridCell> PossibleMoves();

        protected ChessPieceRaw(Team team, ChessPiece[,] board)
        {
            Team = team;
            _board = board;
        }
        
        /// <summary>
        /// Checks validity of cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>true if the cell is valid and empty
        /// false if it is invalid or occupied by same team piece
        /// null otherwise
        /// </returns>
        protected bool? IsValid(GridCell cell)
        {
            if (cell.Row>=0&&cell.Column>=0&&cell.Row<8&&cell.Column<8)
            {
                if ((_board[cell.Row, cell.Column] != null))
                {
                    if (_board[cell.Row, cell.Column].Team == Team) return false;
                    return null;
                }
                return true;
            }
            return false;
        }
    }
}
