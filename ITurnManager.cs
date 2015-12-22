using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    interface ITurnManager
    {
        bool IconClicked(ChessPiece piece); //return if the piece is of playing team
        bool MovePiece(GridCell from, GridCell to);
    }
}
