using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Pawn:ChessPieceRaw
    {
        int _enPassantMove = -1;
        GridCell _enPassantCaptureCell;
        private IPieceMover _mover; //To capture en passant pawn

        public override string GetIcon()
        {
            return GetIconPrefix() + "pawn.png";
        }

        private bool IsFirstRank()
        {
            return (Team == Team.Black && Cell.Row == 1) || (Team == Team.White && Cell.Row == 6);
        }

        public override IEnumerable<GridCell> PossibleMoves()
        {
            int nextRow = 1;
            if (Team == Team.White) nextRow = -1;
            if (_enPassantMove == TurnManager.MoveId)
                yield return new GridCell(Cell.Row+nextRow, _enPassantCaptureCell.Column);
            GridCell tmp;
            tmp = new GridCell(Cell.Row + nextRow, Cell.Column);
            if (IsValid(tmp) == true) yield return tmp;
            if (IsFirstRank() && IsValid(tmp) == true)           //bug fixed...cannot move double if already blocked!
            {
                tmp = new GridCell(Cell.Row + 2*nextRow, Cell.Column);
                if (IsValid(tmp) == true) yield return tmp;
            }
            tmp = new GridCell(Cell.Row + nextRow, Cell.Column - 1);
            if (IsValid(tmp) == null) yield return tmp;
            tmp = new GridCell(Cell.Row + nextRow, Cell.Column + 1);
            if (IsValid(tmp) == null) yield return tmp;
        }
        
        public void AllowEnPassant(GridCell captureCell)
        {
            _enPassantCaptureCell = captureCell;
            _enPassantMove = TurnManager.MoveId + 1;
        }
        public Pawn(Team team, ChessPiece[,] board, IPieceMover mover) : base(team, board)
        {
            _mover = mover;
        }

        public override void Move(GridCell from, GridCell to)
        {
            int nextRow = 1;
            if (Team == Team.White) nextRow = -1;

            if (IsFirstRank())
            {
                var tmp = new GridCell(Cell.Row + nextRow*2, Cell.Column + 1);
                if (IsValid(tmp) == null && (Board[tmp.Row, tmp.Column] is Pawn))
                    (Board[tmp.Row, tmp.Column] as Pawn).AllowEnPassant(to);
                tmp = new GridCell(Cell.Row + nextRow*2, Cell.Column - 1);
                if (IsValid(tmp) == null && (Board[tmp.Row, tmp.Column] is Pawn))
                    (Board[tmp.Row, tmp.Column] as Pawn).AllowEnPassant(to);

            }
            base.Move(from, to);
            if (_enPassantMove == TurnManager.MoveId)   //at this move en passant is allowed
            {
                if (_enPassantCaptureCell.Column == Cell.Column) //en passant was used
                {
                    _mover.MovePieceUnconditioned(_enPassantCaptureCell, GridCell.NullCell);
                }
                else _enPassantMove = -1;
            }
        }
        public override void UndoMove(GridCell current, GridCell previous)
        {
            base.UndoMove(current, previous);
            if (Math.Abs(current.Row - previous.Row) == 2) //double move
            {
                int nextRow = 1;
                if (Team == Team.White) nextRow = -1;
                var tmp = new GridCell(Cell.Row + nextRow * 2, Cell.Column + 1);
                if (IsValid(tmp) == null && (Board[tmp.Row, tmp.Column] is Pawn))
                    (Board[tmp.Row, tmp.Column] as Pawn).UndoAllowEnPassant();
                tmp = new GridCell(Cell.Row + nextRow * 2, Cell.Column - 1);
                if (IsValid(tmp) == null && (Board[tmp.Row, tmp.Column] is Pawn))
                    (Board[tmp.Row, tmp.Column] as Pawn).UndoAllowEnPassant();
            }
            //if (_enPassantMove == TurnManager.MoveId)
                //UndoAllowEnPassant();
        }

        private void UndoAllowEnPassant()
        {
            _enPassantMove = -1;
        }
    }
}
