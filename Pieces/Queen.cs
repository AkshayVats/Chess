using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Queen:ChessPiece
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "queen.png";
        }
    }
}
