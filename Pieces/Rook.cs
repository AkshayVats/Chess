using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Rook:ChessPiece
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "rook.png";
        }
    }
}
