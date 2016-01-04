using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    interface IPieceMover
    {
        void MovePieceUnconditioned(GridCell from, GridCell to);
        bool UndoMove();
        void SpawnPiece(ChessPiece piece, GridCell cell);
    }
}
