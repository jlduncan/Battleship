using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Submarine : Ship
    {
       
        //Properties

        //Constructor
        public Submarine()
            : base()
        {
            HITPOINTS = 3;
            MaxHP = HITPOINTS;
            HP = MaxHP;
            ID = 5;
            IsSunk = false;
        }//end constructor

        //Methods
    }//end class Submarine
}//end namespace
