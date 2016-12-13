using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form1 : Form
    {
        //local variables

        Game newGame;
        private bool vertical;//used in human player's ship placement
        private Ship placedShip;//the current ship that's being placed on the board
        private bool greyOut = false;//this is used to determine if a ship has actually been placed, and therefore
        //the ship's radio button can be disabled
        private int shipCounter = 0;//number of ships that have been placed
        private bool CountIt = false;//This bool is used to toggle the turn counter
        public int counter = 0;
        public int PlayerHitLog = 0;
       

        public Form1()
        {
            InitializeComponent();
            Setup();


        }//end Form1

        public void Setup()//this method is to set up the gameboard before beginning game
        {
            
            pbHeader.Image = Properties.Resources.Battleship;
            pbHumanPTBoat.Image = Properties.Resources.PT;
            pbHumanCruiser.Image = Properties.Resources.CR;
            pbHumanCarrier.Image = Properties.Resources.CA;
            pbHumanSub.Image = Properties.Resources.SB;
            pbHumanBattleship.Image = Properties.Resources.BA;
            pbCPUPtBoat.Image = Properties.Resources.PT;
            pbCPUCruiser.Image = Properties.Resources.CR;
            pbCPUCarrier.Image = Properties.Resources.CA;
            pbCPUSub.Image = Properties.Resources.SB;
            pbCPUBattleship.Image = Properties.Resources.BA;
            lblTurnCounter.Text = "0";
            lblCPUCoordinate.Text = "";
            lblHumanCoordinates.Text = "";
            pbCPUBattleship.BackColor = Color.LightGray;
            pbCPUCarrier.BackColor = Color.LightGray;
            pbCPUCruiser.BackColor = Color.LightGray;
            pbCPUPtBoat.BackColor = Color.LightGray;
            pbCPUSub.BackColor = Color.LightGray;
            pbHumanBattleship.BackColor = Color.LightGray;
            pbHumanCarrier.BackColor = Color.LightGray;
            pbHumanCruiser.BackColor = Color.LightGray;
            pbHumanPTBoat.BackColor = Color.LightGray;
            pbHumanSub.BackColor = Color.LightGray;

            //setting up the buttons in the table
            for (int row = 0; row < tableLayoutPanel1.RowCount; row++)
            {
                for (int column = 0; column < tableLayoutPanel1.ColumnCount; column++)
                {
                    int M = row;
                    int N = column;
                    Control button = tableLayoutPanel1.GetControlFromPosition(row, column);
                    button.BackColor = Color.LightSeaGreen;
                    button.Click += delegate { SetUpPlacement(M, N); };
                }//end for
            }//end other for

            for (int row = 0; row < tableLayoutPanel2.RowCount; row++)
            {
                for (int column = 0; column < tableLayoutPanel2.ColumnCount; column++)
                {
                    Control button = tableLayoutPanel2.GetControlFromPosition(row, column);
                    button.BackColor = Color.LightSeaGreen;
                    int M = row;
                    int N = column;
                    button.Text = "";
                    button.Click += delegate { GenerateTurn(M, N); };


                }//end for
            }//end other for
            tableLayoutPanel1.Enabled = false;
            tableLayoutPanel2.Enabled = false;
            groupBoxPlayerAlignment.Enabled = false;
            groupBoxPlayerAlignment.Visible = false;
            groupBoxPlayerShips.Enabled = false;
            groupBoxPlayerShips.Visible = false;
            btnReady.Enabled = false;
            btnReady.Visible = false;
            btnSurrender.Enabled = false;
            btnSurrender.Visible = false;
        }//end setup Method

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            
            newGame = new Game(); //"Want to play a game?" - WOPR
            newGame.StartGame();

            //setting up the buttons in the table
            for (int row = 0; row < tableLayoutPanel1.RowCount; row++)
            {
                for (int column = 0; column < tableLayoutPanel1.ColumnCount; column++)
                {
                    Control button = tableLayoutPanel1.GetControlFromPosition(row, column);
                    button.Enabled = true;
                    button.BackColor = Color.LightSeaGreen;
                    button.Text = "";
                    

                }//end for
            }//end other for

            for (int row = 0; row < tableLayoutPanel2.RowCount; row++)
            {
                for (int column = 0; column < tableLayoutPanel2.ColumnCount; column++)
                {
                    Control button = tableLayoutPanel2.GetControlFromPosition(row, column);
                    button.BackColor = Color.LightSeaGreen;
                    button.Enabled = true;
                    
                }//end for
            }//end other for
            tableLayoutPanel1.Enabled = true;
            groupBoxPlayerAlignment.Enabled = true;
            groupBoxPlayerAlignment.Visible = true;
            groupBoxPlayerShips.Enabled = true;
            groupBoxPlayerShips.Visible = true;
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            btnReady.Enabled = true;
            rbBattleship.Checked = false;
            rbBattleship.Enabled = true;
            rbCarrier.Checked = false;
            rbCarrier.Enabled = true;
            rbSubmarine.Checked = false;
            rbSubmarine.Enabled = true;
            rbPTBoat.Checked = false;
            rbPTBoat.Enabled = true;
            rbCruiser.Checked = false;
            rbCruiser.Enabled = true;
            shipCounter = 0;
            counter = 0;
            lblTurnCounter.Text = "0";
            MessageBox.Show("Place your ships. Select a ship and a direction using the buttons below. Then click on the grid.  The ship will fill in below or to the right of where you clicked. Once all ships have been placed, the ready button will appear, and you can begin the game.");
        }

        private void RefreshGrids()//refreshes the grid arrays after every turn
        {

            for (int row = 0; row < newGame.Cpu.GridArray.GetLength(0); row++)
            {

                for (int column = 0; column < newGame.Cpu.GridArray.GetLength(1); column++)
                {
                    Control button = tableLayoutPanel2.GetControlFromPosition(row, column);
                    
                    switch (newGame.Cpu.GridArray[row, column])
                    {
                        case CPUPlayer.EMPTY: button.BackColor = Color.LightSeaGreen;

                            break;
                        case CPUPlayer.MISS: button.BackColor = Color.White;
                            button.Enabled = false;
                            break;
                        case CPUPlayer.HIT: button.BackColor = Color.Red;
                            button.Enabled = false;
                            break;
                        //so, we're leaving these in for the test...will change to LightSeaGreen later to hide
                        //from user
                        case CPUPlayer.PTBOAT:
                        case CPUPlayer.CRUISER:
                        case CPUPlayer.SUBMARINE:
                        case CPUPlayer.BATTLESHIP:
                        case CPUPlayer.CARRIER: button.BackColor = Color.LightSeaGreen;
                            break;
                        default: break;


                    }//end switch
                }//end for
            }//end for

            CPUShipsColorChange();
            


            if (newGame.Human.hitIndicator == true)
            {
                
                MessageBox.Show("You got a hit!");
            }


            if (CountIt == true)
            {
                counter++;
                lblTurnCounter.Text = Convert.ToString(counter);
            }//end if

            for (int row = 0; row < newGame.Human.GridArray.GetLength(0); row++)
            {



                for (int column = 0; column < newGame.Human.GridArray.GetLength(1); column++)
                {
                    Control button = tableLayoutPanel1.GetControlFromPosition(row, column);
                    switch (newGame.Human.GridArray[row, column])
                    {
                        case HumanPlayer.EMPTY: button.BackColor = Color.LightSeaGreen;
                            button.Text = "";
                            break;
                        case HumanPlayer.MISS: button.BackColor = Color.White;
                            button.Text = "";
                            break;
                        case HumanPlayer.HIT: button.BackColor = Color.Red;
                            button.Text = "";
                            break;
                        case HumanPlayer.PTBOAT: button.BackColor = Color.Gray;
                            button.Text = "PT";
                            break;
                        case HumanPlayer.CRUISER: button.BackColor = Color.Gray;
                            button.Text = "CR";
                            break;
                        case HumanPlayer.SUBMARINE: button.BackColor = Color.Gray;
                            button.Text = "SB";
                            break;
                        case HumanPlayer.BATTLESHIP: button.BackColor = Color.Gray;
                            button.Text = "BA";
                            break;
                        case HumanPlayer.CARRIER: button.BackColor = Color.Gray;
                            button.Text = "CA";
                            break;
                        default: break;


                    }//end switch

                }//end for
            }//end for
            if (newGame.Cpu.hitIndicator == true)
            {
                MessageBox.Show("The CPU got a hit!");
                
            }

            HumanShipsColorChange();
            CountIt = false;
            

        }//end method Refresh

        
        private void SetUpPlacement(int M, int N)
        {
            try
            {
                greyOut = newGame.Human.Place(vertical, placedShip, M, N);
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Please select a ship to be placed.");
            }//
            if (greyOut == true && placedShip != null)
            {
                RefreshGrids();
                placedShip = null;
                shipCounter++;
                if (shipCounter == 5)
                {
                    btnReady.Visible = true;
                    btnReady.Enabled = true;
                }//end if
            }//end if
            
                
            
        }//end method SetUpPlacement

        private void GenerateTurn(int M, int N)
        {
            bool gameOver = false;
            gameOver = newGame.Turn(M, N);
            CountIt = true;
            lblHumanCoordinates.Text = string.Format("{0} {1}",Convert.ToChar(N + 65) , M+1);
            RefreshGrids();
            lblCPUCoordinate.Text = string.Format("{0} {1}", Convert.ToChar(newGame.Cpu.ReturnCoordinateValues[0] + 65), newGame.Cpu.ReturnCoordinateValues[1] + 1);
            CountIt = true;
            if (gameOver == true)
            {
                btnNewGame.Enabled = true;
                btnNewGame.Visible = true;
                tableLayoutPanel1.Enabled = false;
                tableLayoutPanel2.Enabled = false;
                btnSurrender.Enabled = false;
                btnSurrender.Visible = false;
            }
            

        }//end method GenerateTurn

        private void Form1_Load(object sender, EventArgs e)
        {
            
                    
            
        }

        private void rbVertical_Checked(object sender, EventArgs e)
        {
            vertical = true;
        }

        private void rbHorizontal_Checked(object sender, EventArgs e)
        {
            vertical = false;
        }

        private void rbBattleship_Checked(object sender, EventArgs e)
        {
            placedShip = newGame.Human.battleship;
            if (greyOut == true)
            {
                
                rbBattleship.Enabled = false;
                
                greyOut = false;
                
            }

        }

        private void rbCarrier_Checked(object sender, EventArgs e)
        {
            placedShip = newGame.Human.carrier;
            if (greyOut == true)
            {
                rbCarrier.Enabled = false;
                
                greyOut = false;
            }
        }

        private void rbCruiser_Checked(object sender, EventArgs e)
        {
            placedShip = newGame.Human.cruiser;
            if (greyOut == true)
            {
                rbCruiser.Enabled = false;
               
                greyOut = false;
            }
        }

        private void rbPTBoat_Checked(object sender, EventArgs e)
        {
            placedShip = newGame.Human.ptboat;
            if (greyOut == true)
            {
                rbPTBoat.Enabled = false;
                greyOut = false;
                
            }
        }

        private void rbSubmarine_Checked(object sender, EventArgs e)
        {
            placedShip = newGame.Human.submarine;
            if (greyOut == true)
            {
                rbSubmarine.Enabled = false;
                
                greyOut = false;
            }
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            if (shipCounter == 5)
            {
                btnReady.Enabled = false;
                btnReady.Visible = false;
                groupBoxPlayerAlignment.Enabled = false;
                groupBoxPlayerAlignment.Visible = false;
                groupBoxPlayerShips.Enabled = false;
                groupBoxPlayerShips.Visible = false;
                btnSurrender.Enabled = true;
                btnSurrender.Visible = true;
                tableLayoutPanel1.Enabled = false;
                tableLayoutPanel2.Enabled = true;
             
            }//end if
            else
            {
                MessageBox.Show("Please Place All Ships Onto Grid Before Starting Game");
            }//end else
        }

        private void btnSurrender_Click(object sender, EventArgs e)
        {
            newGame.Surrender();
            btnSurrender.Enabled = false;
            btnSurrender.Visible = false;
            btnNewGame.Enabled = true;
            btnNewGame.Visible = true;
            tableLayoutPanel1.Enabled = false;
            tableLayoutPanel2.Enabled = false;
        }

        private void HumanShipsColorChange()
        {

            if (newGame.Human.ptboat.IsSunk == true)
            {
                pbHumanPTBoat.Image = Properties.Resources.HIT;
                //pbHumanPTBoat.BackColor = Color.Red;
            }//end if

            if (newGame.Human.cruiser.IsSunk == true)
            {
                pbHumanCruiser.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Human.submarine.IsSunk == true)
            {
                pbHumanSub.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Human.battleship.IsSunk == true)
            {
                pbHumanBattleship.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Human.carrier.IsSunk == true)
            {
                pbHumanCarrier.Image = Properties.Resources.HIT;
               
            }//end if

        }//end HumanShipsColorChange

        private void CPUShipsColorChange()
        {

            if (newGame.Cpu.ptboat.IsSunk == true)
            {
                pbCPUPtBoat.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Cpu.cruiser.IsSunk == true)
            {
                pbCPUCruiser.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Cpu.submarine.IsSunk == true)
            {
                pbCPUSub.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Cpu.battleship.IsSunk == true)
            {
                pbCPUBattleship.Image = Properties.Resources.HIT;
                
            }//end if

            if (newGame.Cpu.carrier.IsSunk == true)
            {
                pbCPUCarrier.Image = Properties.Resources.HIT;
                
            }//end if

        }//end CPUShipsColorChange
        
    }
}
