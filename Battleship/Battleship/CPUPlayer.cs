using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Battleship
{
    class CPUPlayer : Player
    {
        //Properties
        int TestProperty { get; set; }
        bool OnTarget { get; set; }

        public int[] ReturnCoordinateValues { get; set; }
        //used in the Attack method's advanced targeting algorithm, denotes 
        //if there's been a hit on a previous run-thrugh of the Attack method

        

        //Instance Variables
        int[,] targets = { { 0, 0}, { 0, 0}, { 0, 0}, { 0, 0},{ 0, 0}};
        int targetCounter1 = 0;//assists in iterating through targeting array
        bool onDirection = false; //used in the targeting logic
        int positiveCounter = 0;
        int negativeCounter = 0; //these are used in the targeting logic once the direction of the ship has been established
        string direction = null;
        bool positive = false;
        bool negative = false;
        bool sunkThisTurn = false;//denotes whether a ship has been sunk this turn
        //Constructor
        public CPUPlayer()
            : base()
        {
            TestProperty = 10;
            NumberOfShips = 5;
            OnTarget = false;
            ReturnCoordinateValues = new int [2];
            
        }//end constructor

        //Methods
        public void Place()
        {

            Random random = new Random();
            Ship[] ships = { base.ptboat, base.cruiser, base.submarine, base.battleship, base.carrier };
            bool occupied = false;

            foreach (Ship s in ships)
            {
                
                bool redo = false;//this is a test condition - if the position randomly chosen for the ship
                //won't actually accomidate the ship, this section of the codes restarts until it gets it right
                do
                {
                    int[,] testGrid = new int[s.MaxHP, 2];
                    int direction = random.Next(0, 2);//0 = horizontal , 1 = vertical, denotes how the ship is positioned
                    redo = false;
                    occupied = false;
                    //deducting the size from any coordinate calculations (so the ship fits in the grid!)
                    if (direction == 0)
                    {
                        //the following generates the point-of-origin coordinates of the ship. Rest of the ship
                        //fills in the coordinates to the right (++N) of the origin.

                        int gridM = random.Next(0, GRIDSIZE);//horizontal position on grid array
                        int gridN = random.Next(0, GRIDSIZE - s.MaxHP);//veritical position on grid array, eliminates values where there could be a space conflict
                        for (int i = 0; i < testGrid.GetLength(0); i++)
                        {
                            testGrid[i, 0] = gridM;
                            testGrid[i, 1] = gridN;
                            gridN++;
                        }//end for
                    }//end if
                    else
                    {
                        //the following generates the point-of-origin coordinates of a vertical ship. Rest of the ship
                        //fills in the coordinates to the bottom (++M) of the origin.

                        int gridM = random.Next(0, GRIDSIZE - s.MaxHP);//horizontal position on grid array
                        int gridN = random.Next(0, GRIDSIZE);//veritical position on grid array
                        for (int i = 0; i < testGrid.GetLength(0); i++)
                        {
                            testGrid[i, 0] = gridM;
                            testGrid[i, 1] = gridN;
                            gridM++;
                        }//end for
                    }//end else

                    //let's check our grid to ensure we haven't already placed another ship there...
                    for (int i = 0; i < testGrid.GetLength(0); i++)
                    {
                        int gridM = testGrid[i, 0];
                        int gridN = testGrid[i, 1];
                        if (this.GridArray[gridM, gridN] != 0)
                        {
                            occupied = true;
                            break;
                        }//end if
                    }//end for

                    if (occupied == true)//is there another ship there?
                    {
                        redo = true;
                    }//end else if
                    else //neither apply, so we can change the flag value of the GridArray coordinates to that of the ship
                    {
                        for (int i = 0; i < testGrid.GetLength(0); i++)
                        {
                            int gridM = testGrid[i, 0];
                            int gridN = testGrid[i, 1];
                            this.GridArray[gridM, gridN] = s.ID;
                        }//end for
                    }//end else
                } while (redo == true);

            }//end foreach


        }//end method Place
        
        public void Attack(HumanPlayer defender)
        {
            int M = 0;
            int N = 0;
            

            bool redo = false;
            hitIndicator = false;
            sunkThisTurn = false;
            Random randomNumber = new Random();//random number object
            
                do
                {
                    redo = false;

                    if (OnTarget == true && onDirection == false)
                    {
                        bool reloop = false;
                        do
                        {
                            //check against the Targets array
                            targetCounter1++;
                            if ((targets[targetCounter1, 0] < 0 || targets[targetCounter1, 0] >= GRIDSIZE) ||
                                (targets[targetCounter1, 1] < 0 || targets[targetCounter1, 1] >= GRIDSIZE))
                            {
                                //array element is out-of-bounds
                                reloop = true;
                                
                            }//end if
                            else
                            {
                                M = targets[targetCounter1, 0];
                                N = targets[targetCounter1, 1];
                                reloop = false;
                            }
                        } while (reloop == true);
                        
                    }//end if

                    if (OnTarget == true && onDirection == true)
                    {
                        //iterate through positive counter first, then negative. we'll be applying the counter to
                        //N if vertical, or to M if horizontal
                        
                       
                        if (positive == true)
                        {
                            positiveCounter++;
                            if (direction == "horizontal")
                            {
                                M = targets[0, 0] + positiveCounter;
                                N = targets[0, 1];
                                
                            }//end if
                            if (direction == "vertical")
                            {
                                M = targets[0, 0];
                                N = targets[0, 1] + positiveCounter;
                                
                            }//end if
                            
                        }//end if
                        else
                        {
                                                        
                            negativeCounter++;
                            if (direction == "horizontal")
                            {
                                M = targets[0, 0] - negativeCounter;
                                N = targets[0, 1];
                            }//end if
                            if (direction == "vertical")
                            {
                                M = targets[0, 0];
                                N = targets[0, 1] - negativeCounter;
                            }//end if
                        
                        }//end else
                        
                        
                    }//end if

                    if (OnTarget==false)
                    {
                        M = randomNumber.Next(0, GRIDSIZE);
                        N = randomNumber.Next(0, GRIDSIZE);
                    }//end if
                    
                    //hitIndicator = false;

                    try
                    {
                        switch (defender.GridArray[M, N])//there's probably a much more elegant way of going about this...
                        //may attempt to improve in the next refactoring
                        {
                            case EMPTY: defender.GridArray[M, N] = MISS;
                                
                                break;
                            case HIT://has already been attempted
                            case MISS: redo = true;//so has this
                                if (positive == true)
                                {
                                    //this means working through the positive side of the targeting direction has hit a
                                    //dead end without sinking the ship, let's try working our way through the negative side instead
                                    positive = false;
                                    negative = true;
                                }//end if
                                else if (negative == true)
                                {
                                    //hmm..what's going on here?  well, we've already exhausted the possibilities on the 
                                    //positive side of the array, and now the negative values are exhausted.  We're probably
                                    //dealing with two ships.  Let's change directions then!
                                    if (direction == "horizontal")
                                    {
                                        direction = "vertical";
                                    }//end if
                                    else
                                    {
                                        direction = "horizontal";
                                    }//end else
                                    negative = false;
                                    positive = true;
                                    positiveCounter = 0;
                                    negativeCounter = 0;
                                    targetCounter1 = 0;
                                }//end else if
                                break;
                            case PTBOAT: defender.GridArray[M, N] = HIT;
                                defender.ptboat.GetsHit();
                                hitIndicator = true;
                                if (hitIndicator == true)
                                {
                                    
                                }
                                if (defender.ptboat.IsSunk == true)
                                {
                                    defender.NumberOfShips--;
                                    ResetValues();

                                }//end if
                                break;
                            case CRUISER: defender.GridArray[M, N] = HIT;
                                defender.cruiser.GetsHit();
                                hitIndicator = true;
                                if (defender.cruiser.IsSunk == true)
                                {
                                    defender.NumberOfShips--;
                                    ResetValues();
                                }//end if
                                break;
                            case SUBMARINE: defender.GridArray[M, N] = HIT;
                                defender.submarine.GetsHit();
                                hitIndicator = true;
                                if (defender.submarine.IsSunk == true)
                                {
                                    defender.NumberOfShips--;
                                    ResetValues();

                                }//end if
                                break;
                            case BATTLESHIP: defender.GridArray[M, N] = HIT;
                                defender.battleship.GetsHit();
                                hitIndicator = true;
                                if (defender.battleship.IsSunk == true)
                                {
                                    defender.NumberOfShips--;
                                    ResetValues();
                                }//end if
                                break;
                            case CARRIER: defender.GridArray[M, N] = HIT;
                                defender.carrier.GetsHit();
                                hitIndicator = true;
                                if (defender.carrier.IsSunk == true)
                                {
                                    defender.NumberOfShips--;
                                    ResetValues();
                                }//end if
                                break;
                            default:
                                break;

                        }//end switch
                    }//end try
                    catch (IndexOutOfRangeException)
                    {
                        if (positive == true)
                        {
                            //this means working through the positive side of the targeting direction has hit a
                            //dead end without sinking the ship, let's try working our way through the negative side instead
                            positive = false;
                            negative = true;
                        }//end if
                        else if (negative == true)
                        {
                            //hmm..what's going on here?  well, we've already exhausted the possibilities on the 
                            //positive side of the array, and now the negative values are exhausted.  We're probably
                            //dealing with two ships.  Let's change directions then!
                            if (direction == "horizontal")
                            {
                                direction = "vertical";
                            }//end if
                            else
                            {
                                direction = "horizontal";
                            }//end else
                            negative = false;
                            positive = true;
                            positiveCounter = 0;
                            negativeCounter = 0;
                        }//end else if
                        redo = true;
                    }//end catch

                    ReturnCoordinateValues[0] = N;
                    ReturnCoordinateValues[1] = M;
                    if (defender.GridArray[M, N] > MISS)
                    {
                        defender.GridArray[M, N] = HIT;
                        //now we have a target, ha ha ha!
                        if (OnTarget == true && onDirection == false)//this represents a second hit, thus likely
                            //establishing the direction of the ship
                        {
                            if (targets[targetCounter1, 0] == targets[0, 0])
                            {
                                direction = "vertical";

                            }//end if
                            else
                            {
                                direction = "horizontal";
                            }//end else
                            onDirection = true;
                            positive = true;//start with the positive values
                            if (M > targets[0, 0] || N > targets[0, 1])
                            {
                                positiveCounter++;

                            }//end if
                            if (M > targets[0, 0] || N > targets[0, 1])
                            {
                                negativeCounter++;
                            }
                        }//end if
                        if (OnTarget == false && sunkThisTurn == false)
                        {
                            //filling up our initial targets array, used to determine the directionality of the ship
                            targets[0, 0] = M;
                            targets[0, 1] = N;

                            targets[1, 0] = M - 1;//horizontal negative
                            targets [1, 1] = N ;
                            targets [2, 0] = M;//vertical negative
                            targets[2, 1] = N - 1;
                            targets[3, 0] = M + 1;//horizontal positive
                            targets[3, 1] = N;
                            targets[4, 0] = M;//vertical positive
                            targets[4, 1] = N + 1;

                            //OnTarget = true;
                        }//end if
                        

                    }//end if
                } while (redo == true);//end do...while
            

        }//end method Attack

        private void ResetValues()
        {
            OnTarget = false;
            onDirection = false;
            direction = null;
            positiveCounter = 0;
            negativeCounter = 0;
            positive = false;
            negative = false;
            sunkThisTurn = true;
            targetCounter1 = 0;
        }//end method ResetValues

    }//end class CPUPlayer
}//end namespace
