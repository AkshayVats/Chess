using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess
{
    //will also hold the board
    class TurnManager:ITurnManager, IPieceMover
    {
        public static int MoveId
        {
            get;
            private set;
        }
        ChessPieceRaw[,] _board = new ChessPieceRaw[8,8];
        private BoardUi _ui;
        Player[] players = new Player[2];
        private int _turn = 0;

        //Information to undo moves
        private struct Move
        {
            public GridCell from;
            public GridCell to;
            public ChessPieceRaw removedPiece;
        }
        private Stack<Move> _moves = new Stack<Move>();
        private Stack<int> _moveIndex = new Stack<int>();

        public TurnManager(BoardUi ui)
        {
            _ui = ui;
            InitBoard();
        }

        private ChessPieceRaw this[int row, int col]
        {
            get { return _board[row, col]; }
        }

        private ChessPieceRaw this[GridCell cell]
        {
            get { return this[cell.Row, cell.Column]; }
        }
        
        private void MovePiece(ChessPieceRaw piece, GridCell to)
        {
            
            //piece.Cell != to
            if (to==GridCell.NullCell)
                _moves.Push(new Move() { from = piece.Cell, to = to, removedPiece = piece });
            else
            {
                _moves.Push(new Move() { from = piece.Cell, to = to, removedPiece = this[to] });
                _board[to.Row, to.Column] = piece;
            }
            
            if(piece.Cell!=GridCell.NullCell)
                _board[piece.Cell.Row, piece.Cell.Column] = null;
            if (to != GridCell.NullCell)
                piece.Move(piece.Cell, to);
        }
        private void UndoMove(ChessPieceRaw piece, GridCell to)
        {
            _board[to.Row, to.Column] = piece;
            if (piece.Cell != GridCell.NullCell)
                _board[piece.Cell.Row, piece.Cell.Column] = null;
            piece.UndoMove(piece.Cell, to);
        }
        private void InitBoard()
        {
            Type[] heroes = {typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook)};
            for (int i = 0; i < 8; i++)
            {
                if (i == 4)//King
                {
                    MovePiece(Activator.CreateInstance(heroes[i], Team.Black, _board, this) as ChessPieceRaw, new GridCell(0, i));
                    MovePiece(Activator.CreateInstance(heroes[i], Team.White, _board, this) as ChessPieceRaw, new GridCell(7, i));
                }
                else
                {
                    MovePiece(Activator.CreateInstance(heroes[i], Team.Black, _board) as ChessPieceRaw, new GridCell(0, i));
                    MovePiece(Activator.CreateInstance(heroes[i], Team.White, _board) as ChessPieceRaw, new GridCell(7, i));
                }
                MovePiece(new Pawn(Team.Black, _board, this), new GridCell(1, i));
                MovePiece(new Pawn(Team.White, _board, this), new GridCell(6, i));
            }
            _ui.Render(_board);
        }

        public void SetupPlayers(Player white, Player black)
        {
            players[0] = white;
            players[1] = black;
            _ui.SetUiManager(this);
        }

        public bool IconClicked(ChessPiece piece)
        {
            if (players[_turn] is HumanPlayer && ((int)piece.Team ==_turn))
            {
                (players[_turn] as HumanPlayer).NotifyIconClicked(piece);
                return true;
            }
            return false;
        }

        public bool MovePiece(GridCell from, GridCell to)
        {
            //Called by BoardUi, AI
            //Move piece
            //Update ui
            //Check for mate or stalemate
            //Change Turn
            //Notify Player
            if (((int) this[from].Team == _turn) && (this[to] == null || (int) this[to].Team != _turn)&&this[from].PossibleMoves().Contains(to))
            {
                _moveIndex.Push(_moves.Count);

                MovePieceUnconditioned(from, to);
                MoveId++;
                SwapTurn();
                _ui.Render(_board);
                return true;
            }
            return false;
        }

        private void SwapTurn()
        {
            _turn = _turn == 0 ? 1 : 0;
        }

        public void MovePieceUnconditioned(GridCell @from, GridCell to)
        {
            MovePiece(this[from], to);
            //players[_turn].NotifyMove(from, to);
        }
        bool IPieceMover.UndoMove()
        {
            if (_moveIndex.Count == 0) return false;
            while (_moves.Count > _moveIndex.Peek())
            {
                var move = _moves.Pop();
                if(move.to!=GridCell.NullCell)
                    UndoMove(this[move.to], move.from);
                if(move.removedPiece!=null)
                    _board[move.removedPiece.Cell.Row, move.removedPiece.Cell.Column] = move.removedPiece;
            }
            _moveIndex.Pop();
            return true;
        }
        bool ITurnManager.UndoMove()
        {
            var r = ((IPieceMover)this).UndoMove();
            if (r)
            {
                MoveId--;
                SwapTurn();
                _ui.Render(_board);
            }
            return r;
        }

        public void SpawnPiece(ChessPiece piece, GridCell cell)
        {
            MovePiece( (ChessPieceRaw)piece, cell);
        }
    }
}
