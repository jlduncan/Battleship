using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
     public class Battleship : Ship
    {
       
        //Properties

        //Constructor
        public Battleship()
            : base()
        {
            HITPOINTS = 4;
            MaxHP = HITPOINTS;
            HP = MaxHP;
            ID = 6;
            IsSunk = false;
        }//end constructor

        //Methods
    }//end class Battleship
}//end namespace
