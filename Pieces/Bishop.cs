using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Bishop: ChessPieceRaw
    {
        public override string GetIcon()
        {
            return GetIconPrefix() + "bishop.png";
        }

        public Bishop(Team team) : base(team)
        {
        }
    }
}
