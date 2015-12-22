using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess
{
    //will also hold the board
    class TurnManager
    {
        ChessPiece[,] _board = new ChessPiece[8,8];
        private BoardUi _ui;
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
                _board[0, i] = Activator.CreateInstance(heroes[i]) as ChessPiece;
                _board[0,i].Team = Team.Black;
                _board[7, i] = Activator.CreateInstance(heroes[i]) as ChessPiece;
                _board[1,i]=new Pawn() {Team = Team.Black};
                _board[6,i]=new Pawn();
            }
            _ui.Render(_board);
        }
    }
}
