using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class King: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "king.png";
        }

        public King(Team team) : base(team)
        {
        }
    }
}
