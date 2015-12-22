using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class AI:Player
    {
        public AI(ITurnManager turnManager)
        {
            
        }
        public void MakeMove()
        {
            throw new NotImplementedException();
            //AI needs to store its own board representation
            //(although we can share the board data from TurnManager, buts lets keeps it separate)
            //For each cell, if the piece on the cell is ours
                //find its all possible moves
                //use minmax
                //need a scoring procedure
        }

        public void NotifyMove(GridCell @from, GridCell to)
        {
            
        }
    }
}
