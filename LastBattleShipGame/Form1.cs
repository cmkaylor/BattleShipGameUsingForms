using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace LastBattleShipGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[,] Matrix = new string[8,8];
        private string[,] EnemyMatrix = new string[8, 8];

        private void button1_Click(object sender, EventArgs e)
        {
            Action(); 
        }

        private void Action()
        {
            BattleShipMatrix battleShipMatrix = new BattleShipMatrix();
            Matrix = battleShipMatrix.BuildMatrix();

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(Matrix[i, j] == "S" || Matrix[i, j] == "D" || Matrix[i, j] == "A" || Matrix[i, j] == "W")
                    {
                      Point thePoint = new Point { X = i, Y = j };
                      UpdateBoard(thePoint);
                    }
                }
            }
        }

        private void UpdateBoard(Point x)
        {
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (string)item.Tag == x.X + "," + x.Y)
                {

                    Point itemLocation = item.Location;
                    Size itemSize = item.Size;

                    this.Controls.Remove(item);

                    PictureBox ship = new PictureBox();
                    ship.Location = itemLocation;
                    ship.Size = itemSize;
                    ship.Tag = x.X + "," + x.Y;
                    ship.BackColor = Color.Black;
                    this.Controls.Add(ship);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//Start Button
            StartGame();
        }

        private void StartGame()
        {
            BattleShipMatrix battleShipMatrix = new BattleShipMatrix();
            EnemyMatrix = battleShipMatrix.BuildMatrix();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
        }
        private void ClearBoard()
        {
          
            string x = "";
            int counter = 0;
            foreach (var item in EnemyMatrix)
            {
                if (counter < 8)
                {
                    x = x + item;
                    counter++;
                }

                if (counter == 8)
                {
                    x = x + "\r";
                    counter = 0;

                }
            }

            MessageBox.Show(x);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {//shoot button and get enemy strike
            Shoot(1);
            Shoot(2);
            //ClearBoard();
        }

        private void Shoot(int turn)
        {
            string coordinates = "";
            switch(turn)
            {
                case 1:
                    ShootWindow shootWindow = new ShootWindow();
                    shootWindow.ShowDialog();

                    coordinates = shootWindow.Coordinates;
                    break;

                case 2:
                    Random rand = new Random();

                    coordinates = BoardMatrix[rand.Next(0,7),rand.Next(0,7)];
                    break;
            }

            for(int i = 0; i < 8; i ++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(BoardMatrix[i,j] == coordinates)
                    {
                        Point thePoint = new Point { X = i, Y = j };
                        UpdateBoardWithShot(thePoint, HitOrMiss(thePoint,turn),turn);
                        break;
                    }
                }
            }

            SunkChecker();

        }

        private bool HitOrMiss(Point x, int turn)
        {
            bool returnable = false;

            if(turn == 1)
            {
                if (EnemyMatrix[x.X, x.Y] == "S" || EnemyMatrix[x.X, x.Y] == "D" || EnemyMatrix[x.X, x.Y] == "A" || EnemyMatrix[x.X, x.Y] == "W")
                {
                    EnemyMatrix[x.X, x.Y] = "X";
                    returnable = true;
                }
            }

            if(turn == 2)
            {
                if (Matrix[x.X, x.Y] == "S" || Matrix[x.X, x.Y] == "D" || Matrix[x.X, x.Y] == "A" || Matrix[x.X, x.Y] == "W")
                {
                    Matrix[x.X, x.Y] = "X";
                    returnable = true;
                }
            }

            return returnable;
        }

        private void UpdateBoardWithShot(Point x, bool hit, int turn)
        {
            Color hitColor = Color.Black;
            switch(hit)
            {
                case true:
                    hitColor = Color.Red;
                    break;

                case false:
                    hitColor = Color.Purple;
                    break;
            }

            switch(turn)
            {
                case 1:
                    foreach (Control item in this.Controls)
                    {
                        if (item is PictureBox && (string)item.Tag == "E" + x.X + "," + x.Y)
                        {

                            Point itemLocation = item.Location;
                            Size itemSize = item.Size;

                            this.Controls.Remove(item);

                            PictureBox ship = new PictureBox();
                            ship.Location = itemLocation;
                            ship.Size = itemSize;
                            ship.Tag = x.X + "," + x.Y;
                            ship.BackColor = hitColor;
                            this.Controls.Add(ship);
                        }
                    }
                    break;

                case 2:
                    foreach (Control item in this.Controls)
                    {
                        if (item is PictureBox && (string)item.Tag == x.X + "," + x.Y)
                        {

                            Point itemLocation = item.Location;
                            Size itemSize = item.Size;

                            this.Controls.Remove(item);

                            PictureBox ship = new PictureBox();
                            ship.Location = itemLocation;
                            ship.Size = itemSize;
                            ship.Tag = x.X + "," + x.Y;
                            ship.BackColor = hitColor;
                            this.Controls.Add(ship);
                        }
                    }
                    break;

            }
        }

        private string [,] BoardMatrix =
        {
            {"a1","a2","a3","a4","a5","a6","a7","a8"},
            {"b1","b2","b3","b4","b5","b6","b7","b8"},
            {"c1","c2","c3","c4","c5","c6","c7","c8"},
            {"d1","d2","d3","d4","d5","d6","d7","d8"},
            {"e1","e2","e3","e4","e5","e6","e7","e8"},
            {"f1","f2","f3","f4","f5","f6","f7","f8"},
            {"g1","g2","g3","g4","g5","g6","g7","g8"},
            {"h1","h2","h3","h4","h5","h6","h7","h8"},
        };


        int SunkBoatOne = 0;
        int SunkBoatTwo = 0;
        int SunkBoatThree = 0;
        int SunkBoatFour = 0;

        private void SunkChecker()
        {
            List<string> test = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    test.Add(EnemyMatrix[i, j]);
                }
            }

            if (test.Contains("A") != true)
            {
                SunkBoatOne = 1;
            }

            if (test.Contains("S") != true)
            {
                SunkBoatTwo = 1;
            }

            if (test.Contains("D") != true)
            {
                SunkBoatThree = 1;
            }

            if (test.Contains("W") != true)
            {
                SunkBoatFour = 1;
            }

            int counter = 0;
            counter = SunkBoatOne + SunkBoatTwo + SunkBoatThree + SunkBoatFour;

            label20.Text = counter.ToString();
        }
    }
}
