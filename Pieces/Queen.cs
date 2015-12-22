using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Queen: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "queen.png";
        }
        protected IEnumerable<GridCell> RookMoves()
        {
            for (int i = Cell.Row - 1; i >= 0; i--)
            {
                var cell = new GridCell(i, Cell.Column);
                if (IsValid(cell) == false) break;
                yield return cell;
                if (IsValid(cell) == null) break;
            }

            for (int i = Cell.Row + 1; i < 8; i++)
            {
                var cell = new GridCell(i, Cell.Column);
                if (IsValid(cell) == false) break;
                yield return cell;
                if (IsValid(cell) == null) break;
            }

            for (int i = Cell.Column - 1; i >= 0; i--)
            {
                var cell = new GridCell(Cell.Row, i);
                if (IsValid(cell) == false) break;
                yield return cell;
                if (IsValid(cell) == null) break;
            }

            for (int i = Cell.Column + 1; i < 8; i++)
            {
                var cell = new GridCell(Cell.Row, i);
                if (IsValid(cell) == false) break;
                yield return cell;
                if (IsValid(cell) == null) break;
            }
        }

        protected IEnumerable<GridCell> BishopMoves()
        {
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (j == 0 || k == 0) continue;
                    for (int i = 1; i < 8; i++)
                    {
                        var cell = new GridCell(Cell.Row + i * j, Cell.Column + i * k);
                        {
                            if (IsValid(cell) == false) break;
                            yield return cell;
                            if (IsValid(cell) == null) break;
                        }
                    }
                }
        }
        public override IEnumerable<GridCell> PossibleMoves()
        {
            return RookMoves().Concat(BishopMoves());
        }

        public Queen(Team team, ChessPiece[,] board) : base(team, board)
        {
        }
    }
}
