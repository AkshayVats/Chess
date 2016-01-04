using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Rook: Queen
    {
        //To ckeck castling
        public int MoveCount { get; private set; }
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

        public override void Move(GridCell from, GridCell to)
        {
            base.Move(from, to);
            if (from == GridCell.NullCell) return; //just placed on the board
            MoveCount++;
        }
        public override void UndoMove(GridCell current, GridCell previous)
        {
            base.UndoMove(current, previous);
            MoveCount--;
        }
    }
}
