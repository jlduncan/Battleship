using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    //this class ties everything together - it "makes the game happen".
    class Game
    {
        //Properties
        public HumanPlayer Human;
        public CPUPlayer Cpu;
     
        //Constructor
        public Game()
        {
            Human = new HumanPlayer();
            Cpu = new CPUPlayer();
            
        }//end constructor

        //Methods
        public void StartGame()
        {
            Cpu.Place();

        }//end method StartGame

        public bool Turn(int M, int N)
        {
            Human.Attack(Cpu, M, N);
            
            if (Cpu.NumberOfShips == 0)
            {
                EndGame(true);
                return true;
            }//end if


            Cpu.Attack(Human);
            
            if (Human.NumberOfShips == 0)
            {
                EndGame(false);
                return true;
            }//end if
            return false;
        }//end method Turn

        //Surrender method gets wired up straight to the "Surrender" button on the GUI
        public void Surrender()
        {
            MessageBox.Show("You Surrendered");

        }//end method Surrender

        public void EndGame(bool humanVictory)
        {
            if (humanVictory == true)
            {
                MessageBox.Show("You Won!");
            }//end if
            else
            {
                MessageBox.Show("You Lost...better luck next time!");
            }
             
            //ouput to screen about victory, re-enable and visible "Start" button, disable all other buttons.
        }//end method EndGame()

    }//end class Game
}//

//-Display ships for each player underneath grid, with method for X-ing them out or otherwise noting that
//  they've been destroyed
//figure out how to display coordinates of turn




