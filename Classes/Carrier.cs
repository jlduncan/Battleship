using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Carrier : Ship
    {

        
        //Properties
        
        //Constructor
        public Carrier()
            : base()
        {
            HITPOINTS = 5;
            MaxHP = HITPOINTS;
            HP = MaxHP;
            ID = 7;
            IsSunk = false;
            
        }//end constructor

        //Methods
    }//end class Carrier
}//end namespace
