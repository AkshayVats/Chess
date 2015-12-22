using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess
{
    //will also hold the board
    class TurnManager:ITurnManager
    {
        ChessPieceRaw[,] _board = new ChessPieceRaw[8,8];
        private BoardUi _ui;
        Player[] players = new Player[2];
        private int _turn = 0;
        public TurnManager(BoardUi ui)
        {
            _ui = ui;
            InitBoard();
        }

        private ChessPieceRaw this[int row, int col]
        {
            get { return _board[row, col]; }
            set
            {
                _board[row, col] = value;
                if(value!=null)
                    value.Cell = new GridCell(row, col);
            }
        }

        private ChessPieceRaw this[GridCell cell]
        {
            get { return this[cell.Row, cell.Column]; }
            set { this[cell.Row, cell.Column] = value; }
        }
        private void InitBoard()
        {
            Type[] heroes = {typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook)};
            for (int i = 0; i < 8; i++)
            {
                this[0, i] = Activator.CreateInstance(heroes[i], Team.Black, _board) as ChessPieceRaw;
                this[7, i] = Activator.CreateInstance(heroes[i], Team.White, _board) as ChessPieceRaw;
                this[1,i]=new Pawn(Team.Black, _board);
                this[6,i]=new Pawn(Team.White, _board);
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
                this[to] = this[from];
                this[from] = null;
                _ui.Render(_board);
                _turn = _turn == 0 ? 1 : 0;
                players[_turn].NotifyMove(from, to);
                return true;
            }
            return false;
        }
    }
}
