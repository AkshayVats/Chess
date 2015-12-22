using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Pawn:ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "pawn.png";
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            GridCell tmp;
            if (Team == Team.Black)
            {
                if (Cell.Row == 1)
                {
                    tmp = new GridCell(Cell.Row + 2, Cell.Column);
                    if (IsValid(tmp) == true) yield return tmp;
                }
                tmp = new GridCell(Cell.Row + 1, Cell.Column);
                if (IsValid(tmp) == true) yield return tmp;
                tmp = new GridCell(Cell.Row + 1, Cell.Column - 1);
                if (IsValid(tmp) == null) yield return tmp;
                tmp = new GridCell(Cell.Row + 1, Cell.Column + 1);
                if (IsValid(tmp) == null) yield return tmp;
            }
            else
            {
                if (Cell.Row == 6)
                {
                    tmp = new GridCell(Cell.Row - 2, Cell.Column);
                    if (IsValid(tmp) == true) yield return tmp;
                }
                tmp = new GridCell(Cell.Row - 1, Cell.Column);
                if (IsValid(tmp) == true) yield return tmp;
                tmp = new GridCell(Cell.Row - 1, Cell.Column - 1);
                if (IsValid(tmp) == null) yield return tmp;
                tmp = new GridCell(Cell.Row - 1, Cell.Column + 1);
                if (IsValid(tmp) == null) yield return tmp;
            }
        }

        public Pawn(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
