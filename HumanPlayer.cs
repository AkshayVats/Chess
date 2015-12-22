using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess
{
    class HumanPlayer:Player
    {
        private bool _turn;
        private Team Team;
        public HumanPlayer()
        {
        }


        public void MakeMove()
        {
            //TODO: update ui to show its your turn
            throw new NotImplementedException();
        }

        public void NotifyMove(GridCell @from, GridCell to)
        {
            throw new NotImplementedException();
        }
    }
}
