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

        private void InitBoard()
        {
            Type[] heroes = {typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen), typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook)};
            for (int i = 0; i < 8; i++)
            {
                _board[0, i] = Activator.CreateInstance(heroes[i], Team.Black) as ChessPieceRaw;
                _board[7, i] = Activator.CreateInstance(heroes[i], Team.White) as ChessPieceRaw;
                _board[1,i]=new Pawn(Team.Black);
                _board[6,i]=new Pawn(Team.White);
            }
            _ui.Render(_board);
        }

        public void SetupPlayers(Player white, Player black)
        {
            players[0] = white;
            players[1] = black;
            _ui.SetUiManager(this);
        }

        public void IconClicked(ChessPiece piece)
        {
            if (players[_turn] is HumanPlayer && (piece.Team== Team.Black^_turn==0))
                _ui.SelectIcon(piece);
        }

        public bool MovePiece(GridCell from, GridCell to)
        {
            //Called by BoardUi, AI
            //Move piece
            //Update ui
            //Check for mate or stalemate
            //Change Turn
            //Notify Player
            return false;
        }
    }
}
