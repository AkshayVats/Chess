using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Rook: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "rook.png";
        }

        public Rook(Team team) : base(team)
        {
        }
    }
}
