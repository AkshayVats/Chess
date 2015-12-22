using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Rook: Queen
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "rook.png";
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            return RookMoves();
        }

        public Rook(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
