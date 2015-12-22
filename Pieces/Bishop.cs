using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Bishop: Queen
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "bishop.png";
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            return BishopMoves();
        }

        public Bishop(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
