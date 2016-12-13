using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    class HumanPlayer : Player
    {
        //Properties
        public int TestProperty { get; set; }
        public int PlayerHitLog = 0;
        //Instance Variables

        //Constructor
        public HumanPlayer()
            : base()
        {
            TestProperty = 10;
            NumberOfShips = 5;
        }//end constructor

        //Methods
        public bool Place(bool vertical, Ship ship, int M, int N)
        {
            bool occupied = false;
            
               
               //won't actually accomidate the ship, this section of the codes restarts until it gets it right
               int[,] testGrid = new int[ship.MaxHP, 2];
                   
                   
                   occupied = false;
                   //deducting the size from any coordinate calculations (so the ship fits in the grid!)
                   if (vertical == true)
                   {
                       //the following generates the point-of-origin coordinates of the ship. Rest of the ship
                       //fills in the coordinates to the right (++N) of the origin.

                       
                       for (int i = 0; i < testGrid.GetLength(0); i++)
                       {
                           testGrid[i, 0] = M;
                           testGrid[i, 1] = N;
                           N++;
                       }//end for
                   }//end if
                   else
                   {
                       //the following generates the point-of-origin coordinates of a vertical ship. Rest of the ship
                       //fills in the coordinates to the bottom (++M) of the origin.

                       for (int i = 0; i < testGrid.GetLength(0); i++)
                       {
                           testGrid[i, 0] = M;
                           testGrid[i, 1] = N;
                           M++;
                       }//end for
                   }//end else

                   for (int i = 0; i < testGrid.GetLength(0); i++)
                   {
                       int gridM = testGrid[i, 0];
                       int gridN = testGrid[i, 1];
                       try
                       {
                       if (this.GridArray[gridM, gridN] != 0)
                       {
                           occupied = true;
                           break;
                       }//end if
                       }
                         catch (IndexOutOfRangeException)
                       {
                             MessageBox.Show("Please place the ship within the grid area");
                             occupied = true;
                             break;
                         }
                       
                   }//end for

                   if (occupied == true)//is there another ship there?
                   {
                       return false;
                   }//end else if
                   else //neither apply, so we can change the flag value of the GridArray coordinates to that of the ship
                   {
                       for (int i = 0; i < testGrid.GetLength(0); i++)
                       {
                           int gridM = testGrid[i, 0];
                           int gridN = testGrid[i, 1];
                           try 
                           {
                               this.GridArray[gridM, gridN] = ship.ID; 
                           }
                           catch(IndexOutOfRangeException)
                           {
                               MessageBox.Show("Please place ship wwithin the grid area.");
                               
                           }    
                       }//end for
                       return true;
                   }//end else
              
           
            
        }//end method Place

        public void Attack(CPUPlayer defender, int M, int N)
        {

            hitIndicator = false;
            if (defender.GridArray[M, N] == EMPTY)
            {
                defender.GridArray[M, N] = MISS;
                
                //output miss message here & change return type as necessary
            }//end if
            else
            {
                switch (defender.GridArray[M,N])
                {
                    case EMPTY: defender.GridArray[M, N] = MISS;
                        //output miss message here & change return type as necessary
                        break;
                    case PTBOAT: defender.GridArray[M, N] = HIT;
                        defender.ptboat.GetsHit();
                        hitIndicator = true;
                        if (defender.ptboat.IsSunk == true)
                            {
                                defender.NumberOfShips--;
                            }//end if
                        break;
                    case CRUISER: defender.GridArray[M, N] = HIT;
                        defender.cruiser.GetsHit();
                        hitIndicator = true;
                        if (defender.cruiser.IsSunk == true)
                            {
                                defender.NumberOfShips--;
                            }//end if
                        break;
                    case SUBMARINE: defender.GridArray[M, N] = HIT;
                        defender.submarine.GetsHit();
                        hitIndicator = true;
                        if (defender.submarine.IsSunk == true)
                            {
                                defender.NumberOfShips--;
                            }//end if
                        break;
                    case BATTLESHIP: defender.GridArray[M, N] = HIT;
                        defender.battleship.GetsHit();
                        hitIndicator = true;
                        if (defender.battleship.IsSunk == true)
                            {
                                defender.NumberOfShips--;
                            }//end if
                        break;
                    case CARRIER: defender.GridArray[M, N] = HIT;
                        defender.carrier.GetsHit();
                        hitIndicator = true;
                        if (defender.carrier.IsSunk == true)
                            {
                                defender.NumberOfShips--;
                            }//end if
                        break;
                    default:
                        break;
                }//end switch
               
            }//end else

        }

    }//end class HumanPlayer
}//end namespace
