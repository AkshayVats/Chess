using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class King: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "king.png";
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int row = Cell.Row + i;
                    int col = Cell.Column + j;
                    var tmp = new GridCell(row, col);
                    if (IsValid(tmp)!=false)
                    yield return tmp;
                }
        }

        public King(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
