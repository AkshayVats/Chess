using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class King: ChessPieceRaw
    {
        //To Check castling
        private int _moveCount;
        

        private IPieceMover _mover; //To move the rook
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
                    if (IsValid(tmp)!=false&&!IsAttacked(tmp))
                    yield return tmp;
                }
            //Check Castling moves
            if (_moveCount==0)
            {
                int row = 7;
                if (Team == Team.Black) row = 0;
                {
                    if (Board[row, 0] is Rook && (Board[row, 0] as Rook).MoveCount==0 && Board[row, 1] == null &&
                        Board[row, 2] == null && Board[row, 3] == null)
                    {
                        //check attack by enemy
                        if(!IsAttacked(row, 2)&&!IsAttacked(row, 3)&&!IsAttacked(row, 4)) yield return new GridCell(row, 2);
                    }
                    if (Board[row, 7] is Rook && (Board[row, 7] as Rook).MoveCount==0 && Board[row, 5] == null &&
                        Board[row, 6] == null )
                    {
                        //check attack by enemy
                        if (!IsAttacked(row, 4) && !IsAttacked(row, 5) && !IsAttacked(row, 6)) yield return new GridCell(row, 6);
                    }
                }
            }
        }

        private bool IsAttacked(int row, int column)
        {
            return IsAttacked(new GridCell(row, column));
        }
        private bool IsAttacked(GridCell cell)
        {
            bool[] flags = new bool[8];
            int[,] offsets = { {0,1}, {0,-1}, {1,0}, {-1,0}, {1,1}, {-1,-1}, {1,-1} , {-1,1} };

            for (int i = 1; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (flags[j]) continue;
                    GridCell g = new GridCell(cell.Row + i*offsets[j, 0], cell.Column + i*offsets[j, 1]);
                    bool? b = IsValid(g);
                    if (b == null)
                    {
                        var piece = Board[g.Row, g.Column];
                        if (i == 1 &&piece is King)
                            return true;
                        if (piece.GetType()==typeof(Queen))
                            return true;
                        if (j <= 3 && (piece is Rook))
                            return true;
                        if (j >= 4  && (piece is Bishop))
                            return true;
                        if (j>=4&& piece is Pawn && piece.PossibleMoves().Contains(cell))
                            return true;
                        flags[j] = true;
                    }
                    else if (b == false) flags[j] = true;
                }
            }
            //Check knight Annex
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                {
                    int s1 = i == 0 ? -1 : 1;
                    int s2 = j == 0 ? -1 : 1;
                    var tmp = new GridCell(2 * s1 + cell.Row, s2 + cell.Column);
                    if (IsValid(tmp) == null && Board[tmp.Row, tmp.Column] is Knight)
                        return true;
                    tmp = new GridCell(s1 + cell.Row, 2 * s2 + cell.Column);
                    if (IsValid(tmp) == null && Board[tmp.Row, tmp.Column] is Knight)
                        return true;
                }
            return false;
        }
        public King(Team team, ChessPiece[,] board, IPieceMover mover) : base(team, board)
        {
            _mover = mover;
        }

        public override void Move(GridCell from, GridCell to)
        {
            base.Move(from, to);
            if (from == GridCell.NullCell) return;  //just placed on the board
            if (CheckCastleMove(from))
            {
                CastleMove();
            }
            _moveCount++;
        }

        private void CastleMove()
        {
            if (Cell.Column == 2)
            {
                _mover.MovePieceUnconditioned(new GridCell(Cell.Row, 0), new GridCell(Cell.Row, 3));
            }
            else
            {
                _mover.MovePieceUnconditioned(new GridCell(Cell.Row, 7), new GridCell(Cell.Row, 5));
            }
        }
        private bool CheckCastleMove(GridCell from)
        {
            return Math.Abs(from.Column - Cell.Column) == 2;
        }

        public override void UndoMove(GridCell current, GridCell previous)
        {
            base.UndoMove(current, previous);
            _moveCount--;
        }

        
    }
}
