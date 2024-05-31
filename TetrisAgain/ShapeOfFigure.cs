using System;
using System.Drawing;


namespace Tetris
{
    class ShapeOfFigure
    {
        public int x;
        public int y;
        public int[,] map;


        public int Width { get; private set; } // Ширина фігури
        public int Height { get; private set; }
        public Brush Color { get; private set; }
        private Random random = new Random();

        public Tetramino[] Shapes;


        public ShapeOfFigure(int X, int Y, bool useVariant2)
        {
            x = X;
            y = Y;

            RandomSelection(useVariant2);

            Width = map.GetLength(1);
            Height = map.GetLength(0);
        }

        public void MoveFigureDown()
        {
            y++;
        }

        public void MoveFigureRigth()
        {
            x++;
        }

        public void MoveFigureLeft()
        {
            x--;
        }

        public void Turntetramino()
        {
            int rows = map.GetLength(0);
            int columns = map.GetLength(1);

            int[,] turnedFigure = new int[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (map[i, j] == 1)
                    {
                        turnedFigure[j, rows - 1 - i] = map[i,j];
                    }
                }
            }

            map = turnedFigure;

        }

        private void RandomSelection(bool useVariant2)
        {

            if (!useVariant2)
            {
        Shapes = new Tetramino[]
      {
          new Tetramino(
              new int [4,1]
              {
                  {1},
                  {1},
                  {1},
                  {1}
              }),

           new Tetramino(
              new int [3,2]
              {
                  {0,1},
                  {0,1},
                  {1,1}
              }),

            new Tetramino(
              new int [3,2]
              {
                  {1,0},
                  {1,0},
                  {1,1}
              }),

                  new Tetramino(
              new int [2,2]
              {
                  {1,1},
                  {1,1}
              }),

               new Tetramino(
              new int [2,3]
              {
                  {0,1,1},
                  {1,1,0}
              }),

                   new Tetramino(
              new int [2,3]
              {
                  {1,1,1},
                  {0,1,0}
              }),

                new Tetramino(
              new int [2,3]
              {
                  {1,1,0},
                  {0,1,1}
              }),

      };
    } else {

         Shapes = new Tetramino[]
{
          new Tetramino(
              new int [5,1]
              {
                  {1},
                  {1},
                  {1},
                  {1},
                  {1}
              }),

           new Tetramino(
              new int [4,2]
              {
                  {0,1},
                  {0,1},
                  {0,1},
                  {1,1}
              }),

            new Tetramino(
              new int [3,3]
              {
                  {0,1,1},
                  {1,1,0},
                  {0,1,0}
              }),

                  new Tetramino(
              new int [3,3]
              {
                  {1,1,0},
                  {0,1,1},
                  {0,1,0}
              }),

                   new Tetramino(
              new int [4,2]
              {
                  {1,0},
                  {1,0},
                  {1,0},
                  {1,1}
              }),

                new Tetramino(
              new int [3,2]
              {
                  {1,1},
                  {1,1},
                  {0,1}
              }),

                  new Tetramino(
              new int [3,2]
              {
                  {1,1},
                  {1,1},
                  {1,0}
              }),

                  new Tetramino(
              new int [4,2]
              {
                  {0,1},
                  {0,1},
                  {1,1},
                  {1,0}
              }),

                  new Tetramino(
              new int [4,2]
              {
                  {1,0},
                  {1,0},
                  {1,1},
                  {0,1}
              }),

                     new Tetramino(
              new int [3,3]
              {
                  {1,1,1},
                  {0,1,0},
                  {0,1,0}
              }),

               new Tetramino(
              new int [2,3]
              {
                  {1,0,1},
                  {1,1,1}
              }),

                 new Tetramino(
              new int [3,3]
              {
                  {0,0,1},
                  {0,0,1},
                  {1,1,1}
              }),

                    new Tetramino(
              new int [3,3]
              {
                  {0,0,1},
                  {0,1,1},
                  {1,1,0}
              }),

                     new Tetramino(
              new int [3,3]
              {
                  {0,1,0},
                  {1,1,1},
                  {0,1,0}
              }),

                         new Tetramino(
              new int [4,2]
              {
                  {0,1},
                  {1,1},
                  {0,1},
                  {0,1}
              }),

                          new Tetramino(
              new int [4,2]
              {
                  {1,0},
                  {1,1},
                  {1,0},
                  {1,0}
              }),

                           new Tetramino(
              new int [3,3]
              {
                  {0,1,1},
                  {0,1,0},
                  {1,1,0}
              }),

                             new Tetramino(
              new int [3,3]
              {
                  {1,1,0},
                  {0,1,0},
                  {0,1,1}
              }),

};
}
            

            int randomIndex = random.Next(0, Shapes.Length);
            map = Shapes[randomIndex].Shape;
            Color = TetraminoColors.Colors[randomIndex];

        }
        public void Draw(Graphics g, int offsetX, int offsetY, int cellSize)//мал на пікчерс
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                    {
                        g.FillRectangle(Color, new Rectangle(offsetX + j * cellSize, offsetY + i * cellSize, cellSize - 1, cellSize - 1));
                    }
                }
            }
        }

    }
}
