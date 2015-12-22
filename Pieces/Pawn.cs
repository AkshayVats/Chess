using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Pawn:ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "pawn.png";
        }

        public Pawn(Team team) : base(team)
        {
        }
    }
}
