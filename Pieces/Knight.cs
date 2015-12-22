using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Knight:ChessPiece
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "knight.png";
        }
    }
}
