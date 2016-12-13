using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    //something I've wrestled with a bit is whether or not to express the individual ships as their own derived classes 
    // of Ship or instantiating five Ships with the characteristics of each individual ship at runtime... Yep, still 
    // wrestling with it. Thinking there may a few more items than would "look good" to just throw into a new
    //object so I suppose I'll just keep them in their own classes for now. -JLD 11/10/2014 10:12 pm
    abstract public class Ship
    {
        //Constants
        protected int HITPOINTS = 0;

        //Enumerations

        //Properties
        public int HP { get; set; } //current hitpoints of the ship. Changes when it gets hit.
        public int MaxHP { get; set; } //maximum hitpoints - what each ship starts out with.
        public int ID { get; set; }//identification code for the ship
        public bool IsSunk { get; set; }//has this ship been sunk?

        //Constructor
        public Ship()
        {

        }//end constructor

        //Methods

        public void GetsHit()
        {
            HP--;
            if (HP == 0)
            {
                Sunk();
            }//end if
        }//end method GetsHit
        public void Sunk()
        {
            IsSunk = true;
        }//end method Sunk

    }//end class Ship
}//end namespace
