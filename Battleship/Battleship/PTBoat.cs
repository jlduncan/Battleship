using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class PTBoat : Ship
    {
       

        //Properties

        //Constructor
        public PTBoat()
            : base()
        {
            HITPOINTS = 2;
            MaxHP = HITPOINTS;
            HP = MaxHP;
            ID = 3;
            IsSunk = false;
        }//end constructor

        //Methods
    }//end class PTBoat
}//end namespace
