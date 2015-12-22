using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Bishop:ChessPiece
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "bishop.png";
        }
    }
}
