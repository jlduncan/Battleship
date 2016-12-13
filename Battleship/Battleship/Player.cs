using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    //the base Player class.  CPU and Human implement their methods in such a radically different manner from each other
    //that I thought giving them their own derived classes was warranted. JLD 11/10/2014 10:15 pm

    public abstract class Player
    {
        //Constants
        protected int GRIDSIZE = 12;
        public const int EMPTY = 0;
        public const int MISS = 1;
        public const int HIT = 2;
        public const int PTBOAT = 3;
        public const int CRUISER = 4;
        public const int SUBMARINE = 5;
        public const int BATTLESHIP = 6;
        public const int CARRIER = 7;
        public int turnCounter = 0;

        //Properties
        //let's give 'em some ships. Elvis needs boats!
        public PTBoat ptboat = new PTBoat(); //{ get; set; }
        public Cruiser cruiser = new Cruiser(); //{ get; set; }
        public Submarine submarine = new Submarine(); //{ get; set; }
        public Battleship battleship = new Battleship(); //{ get; set; }
        public Carrier carrier = new Carrier(); //{ get; set; }
        public int[,] GridArray { get; set; }
        public int NumberOfShips { get; set; }
        public bool hitIndicator { get; set; }

        //Constructor
        public Player()
        {
            GridArray = new int [GRIDSIZE, GRIDSIZE];
            BuildGrid(GridArray);
            
        }//end constructor

        
        protected void BuildGrid(int [,] array)//used in the constructor to build the grid
        {
            int row = array.GetLength(0);
            int column = array.GetLength(1);
            for (int i = 0; i < row; i++)
                for (int j = 0; 
                    j < column; j++)
                    array[i, j] = 0;
        }//end method BuildGrid
        

        
    }//end abstract class Player
}//end namespace
