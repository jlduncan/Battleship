using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Cruiser : Ship
    {
        
        //Properties

        //Constructor
        public Cruiser()
            : base()
        {
            HITPOINTS = 3;
            MaxHP = HITPOINTS;
            HP = MaxHP;
            ID = 4;
            IsSunk = false;
            
        }//end constructor

        //Methods
    }//end class Cruiser
}//end namespace
