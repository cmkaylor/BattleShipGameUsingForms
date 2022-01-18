using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastBattleShipGame
{
    public class BattleShipMatrix
    {
        private string[,] BattleShipsMatrix =
        {
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
            {"0","0","0","0","0","0","0","0"},
        };

        private List<Point> MakeBoatLocation(int health)
        {
            Random rand = new Random();
            Point randomPoint = new Point(rand.Next(0, 7), rand.Next(0, 7));
            Point keepPoint = randomPoint;
            List<Point> returnable = new List<Point>();
            int randomDirection = rand.Next(1, 4);

            switch (randomDirection)
            {
                case 1:
                    //up
                    for (int i = 0; i < health; i++)
                    {
                        if (randomPoint.X != -1)
                        {
                            returnable.Add(new Point { X = randomPoint.X--, Y = randomPoint.Y });
                        }
                        else
                        {
                            keepPoint.X = keepPoint.X + 1;
                            returnable.Add(new Point { X = keepPoint.X++, Y = keepPoint.Y });
                        }
                    }
                    break;
                case 2:
                    //down
                    for (int i = 0; i < health; i++)
                    {
                        if (randomPoint.X != 8)
                        {
                            returnable.Add(new Point { X = randomPoint.X++, Y = randomPoint.Y });
                        }
                        else
                        {
                            keepPoint.X = keepPoint.X - 1;
                            returnable.Add(new Point { X = keepPoint.X--, Y = keepPoint.Y });
                        }
                    }
                    break;
                case 3:
                    //right
                    for (int i = 0; i < health; i++)
                    {
                        if (randomPoint.Y != 8)
                        {
                            returnable.Add(new Point { X = randomPoint.X, Y = randomPoint.Y++ });
                        }
                        else
                        {
                            keepPoint.Y = keepPoint.Y - 1;
                            returnable.Add(new Point { X = keepPoint.X, Y = keepPoint.Y-- });
                        }
                    }
                    break;
                case 4:
                    //left
                    for (int i = 0; i < health; i++)
                    {
                        if (randomPoint.Y != -1)
                        {
                            returnable.Add(new Point { X = randomPoint.X, Y = randomPoint.Y-- });
                        }
                        else
                        {
                            keepPoint.Y = keepPoint.Y + 1;
                            returnable.Add(new Point { X = keepPoint.X, Y = keepPoint.Y++ });
                        }
                    }
                    break;
            }

            return returnable;

        }

        private bool OverlappingBoats(List<Point> x)
        {
            bool returnable = false;
            List<Point> currentBoatLocations = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BattleShipsMatrix[i, j] == "S" || BattleShipsMatrix[i, j] == "D" || BattleShipsMatrix[i, j] == "A" || BattleShipsMatrix[i, j] == "W")
                    {
                        currentBoatLocations.Add(new Point { X = i, Y = j });
                    }
                }
            }

            foreach (var item in currentBoatLocations)
            {
                if (x.Contains(item))
                {
                    returnable = true;
                }
            }

            return returnable;
        }

        public string [,] BuildMatrix()
        {
            List<Point> thelist = MakeBoatLocation(4);
            while (true)
            {
                if (OverlappingBoats(thelist))
                {
                    thelist = MakeBoatLocation(4);
                }
                else
                {
                    foreach (var item in thelist)
                    {
                        BattleShipsMatrix[item.X, item.Y] = "A";
                    }

                    break;
                }
            }

            thelist = MakeBoatLocation(3);
            while (true)
            {
                if (OverlappingBoats(thelist))
                {
                    thelist = MakeBoatLocation(3);
                }
                else
                {
                    foreach (var item in thelist)
                    {
                        BattleShipsMatrix[item.X, item.Y] = "W";
                    }

                    break;
                }
            }

            thelist = MakeBoatLocation(2);
            while (true)
            {
                if (OverlappingBoats(thelist))
                {
                    thelist = MakeBoatLocation(2);
                }
                else
                {
                    foreach (var item in thelist)
                    {
                        BattleShipsMatrix[item.X, item.Y] = "D";
                    }

                    break;
                }
            }

            thelist = MakeBoatLocation(2);
            while (true)
            {
                if (OverlappingBoats(thelist))
                {
                    thelist = MakeBoatLocation(2);
                }
                else
                {
                    foreach (var item in thelist)
                    {
                        BattleShipsMatrix[item.X, item.Y] = "S";
                    }

                    break;
                }
            }

            return BattleShipsMatrix;
        }
    }
}
