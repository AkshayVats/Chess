using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    //base class for all chess pieces
    interface ChessPiece
    {
        Team Team { get; }      //which team this piece belongs
        string GetIcon();
        GridCell Cell { get; }
        IEnumerable<GridCell> PossibleMoves();
    }
}
