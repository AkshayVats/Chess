using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    interface Player
    {
        void MakeMove();
        void NotifyMove(GridCell from, GridCell to);    //no validation required, Is the cell valid, etc
    }
}
