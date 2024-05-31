using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisAgain
{

    public partial class Functional : Form
    {

       private int size;
       private int level = 1;
       private int linesRemoved = 0;
       public int lines = 0;
       public  int score = 0;

        private int FirstValue = 300;
        private int SpeedDecrease = 0;
        private int updateSpeed = 0;

        public bool UseVariant2;


        private ShapeOfFigure CurrentShape;
        private ShapeOfFigure NextShape;

        private int[,] matrix = new int[20, 10];
        private Brush[,] ColorofFallen = new Brush[20, 10];

        private LabelMessages labelMessages;
        private TetraminoColors tetraminoColors = new TetraminoColors();


        public Functional(bool useVariant2)
        {

            InitializeComponent();
            Init(useVariant2);
            this.UseVariant2 = useVariant2;
            this.DoubleBuffered = true;

            labelMessages = new LabelMessages(label1, label2, label3, label4);
        }

        public void Init(bool useVariant2)
        {
            tetraminoColors.ChoiceColor(useVariant2);

            CurrentShape = new ShapeOfFigure(3, 0, useVariant2);
            NextShape = new ShapeOfFigure(3, 0, useVariant2);

            size = 30;
            timer1.Interval = FirstValue;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            Invalidate();
        }

        private void update(object sender, EventArgs e)
        {
            if (score >= 50000)
            {
                timer1.Stop();
                Winner winner = new Winner(score, lines);

                this.Hide();

                winner.Show();
                return;
            }

            if (score >= 10000 * (SpeedDecrease + 1))
            {
                updateSpeed = FirstValue - 30;
                FirstValue = updateSpeed;
                timer1.Interval = FirstValue;
                SpeedDecrease++;
                level++;
                labelMessages.SpeedLabel(timer1.Interval);
                labelMessages.LevelUp(level);
            }

            if (CanMoveDown())
            {
                CurrentShape.MoveFigureDown();
                Invalidate();
            }
            else
            {
                if (CheckifGameOver())
                {
                    GameOver();
                }
                else
                {
                    NextFigureDown();
                    ClearFilds();
                    Invalidate();
                }
            }
        }

        private bool CheckifGameOver()
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[0, j] == 2) // If there are blocks in the first row, the game is over
                {
                    return true;
                }
            }
            return false;
        }

        private void GameOver()
        {
            timer1.Stop();
            GameOver gameOver = new GameOver(score, lines);

            this.Hide();

            gameOver.Show();

        }


        private void DrawNextShape()
        {
            // Створення Bitmap для малювання фігури
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {

                int x = (pictureBox2.Width - NextShape.Width * size) / 2;
                int y = (pictureBox2.Height - NextShape.Height * size) / 2;

                // Малюємо наступну фігуру на Bitmap
                NextShape.Draw(g, x, y, size);
            }


            pictureBox2.Image = bmp;
        }

        private void ClearFilds()
        {
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                bool ThisLineFull = true;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        ThisLineFull = false;
                        break;
                    }
                }

                if (ThisLineFull)
                {
                    linesRemoved++;
                    lines++;

                    for (int p = i; p > 0; p--)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[p, j] = matrix[p - 1, j];
                            ColorofFallen[p, j] = ColorofFallen[p - 1, j];
                        }
                    }

                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[0, j] = 0;
                        ColorofFallen[0, j] = null;
                    }

                    i++;
                }
            }

            labelMessages.NumberLines(lines);
            SoreLines(linesRemoved);
            linesRemoved = 0;
        }

        private void SoreLines(int linesRemoved)
        {
            for (int i = 0; i < linesRemoved; i++)
            {
                score += 100 * (int)Math.Pow(2, i);

            }
            labelMessages.UpdateScore(score);
        }

        private void NextFigureDown()
        {
            if (!CanMoveDown())
            {
                for (int i = 0; i < CurrentShape.map.GetLength(0); i++)
                {
                    for (int j = 0; j < CurrentShape.map.GetLength(1); j++)
                    {
                        if (CurrentShape.map[i, j] == 1)
                        {
                            ColorofFallen[CurrentShape.y + i, CurrentShape.x + j] = CurrentShape.Color;
                            matrix[CurrentShape.y + i, CurrentShape.x + j] = 2;
                        }
                    }
                }

                ClearFilds();

                CurrentShape = NextShape;
                NextShape = new ShapeOfFigure(3, 0, UseVariant2);

                if (!CanPlaceShape(CurrentShape))
                {
                    GameOver();
                    return;
                }

                DrawNextShape(); // Додаємо виклик DrawNextShape, щоб відобразити нову наступну фігуру

                Invalidate();
            }
        }



        private bool CanPlaceShape(ShapeOfFigure shape)
        {
            for (int i = 0; i < shape.map.GetLength(0); i++)
            {
                for (int j = 0; j < shape.map.GetLength(1); j++)
                {
                    if (shape.map[i, j] == 1)
                    {
                        int nextX = shape.x + j;
                        int nextY = shape.y + i;

                        if (nextX < 0 || nextX >= matrix.GetLength(1) || nextY < 0 || nextY >= matrix.GetLength(0) || matrix[nextY, nextX] == 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (CanMoveLeft())
                    {
                        CurrentShape.MoveFigureLeft();

                    }
                    break;
                case Keys.Right:
                    if (CanMoveRight())
                    {
                        CurrentShape.MoveFigureRigth();

                    }
                    break;
                case Keys.Up:
                    if (CanTurn())
                    {
                        CurrentShape.Turntetramino();
                    }
                  
                    break;
                case Keys.Enter:
                    if (CanMoveDown())
                    {
                        int interval = 5;
                        timer1.Interval = interval;
                    }
                    break;
                case Keys.P:
                    if (timer1.Enabled)
                    {
                        timer1.Stop();
                    }
                    else
                    {
                        timer1.Start();
                    }
                    break;
                case Keys.Escape:
                    timer1.Stop();
                    GameOver gameOver = new GameOver(score, lines);

                    this.Hide();

                    gameOver.Show(); break;
                case Keys.M:

                    timer1.Stop();
                    Menu menu = new Menu();

                    this.Hide();

                    menu.Show();
                    break;


            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    timer1.Interval = FirstValue;
                    break;
            }
        }

        private bool CanTurn()
        {
            int[,] rotatedShape = RotateShape(CurrentShape.map);

            // Перевіряємо чи можна повернути фігуру в її поточній позиції
            if (CanPlaceShapeTurned(rotatedShape, CurrentShape.x, CurrentShape.y))
            {
                return true;
            }

            // Якщо фігура виходить за межі, скоригуємо її позицію
            return AdjustPositionAfterRotation(rotatedShape);
        }

        private int[,] RotateShape(int[,] shape)
        {
            int width = shape.GetLength(0);
            int height = shape.GetLength(1);
            int[,] rotatedShape = new int[height, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rotatedShape[y, width - 1 - x] = shape[x, y];
                }
            }

            return rotatedShape;
        }

        private bool CanPlaceShapeTurned(int[,] shape, int shapeX, int shapeY)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 1)
                    {
                        int nextX = shapeX + j;
                        int nextY = shapeY + i;

                        if (nextX < 0 || nextX >= matrix.GetLength(1) || nextY < 0 || nextY >= matrix.GetLength(0) ||  matrix[nextY, nextX] == 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool AdjustPositionAfterRotation(int[,] rotatedShape)
        {
            // Спробуємо скоригувати позицію фігури, щоб вона не виходила за межі поля
            int shapeX = CurrentShape.x;
            int shapeY = CurrentShape.y;

            for (int xOffset = -rotatedShape.GetLength(1) + 1; xOffset < rotatedShape.GetLength(1); xOffset++)
            {
                for (int yOffset = -rotatedShape.GetLength(0) + 1; yOffset < rotatedShape.GetLength(0); yOffset++)
                {
                    if (CanPlaceShapeTurned(rotatedShape, shapeX + xOffset, shapeY + yOffset))
                    {
                        CurrentShape.x = shapeX + xOffset;
                        CurrentShape.y = shapeY + yOffset;
                        return true;
                    }
                }
            }

            return false;
        }
        private bool CanMoveLeft()
        {
            for (int i = 0; i < CurrentShape.map.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.map.GetLength(1); j++)
                {
                    if (CurrentShape.map[i, j] == 1)
                    {
                        int nextX = CurrentShape.x + j - 1;
                        if (nextX < 0 || matrix[CurrentShape.y + i, nextX] == 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CanMoveRight()
        {
            for (int i = 0; i < CurrentShape.map.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.map.GetLength(1); j++)
                {
                    if (CurrentShape.map[i, j] == 1)
                    {
                        int nextX = CurrentShape.x + j + 1;
                        if (nextX >= matrix.GetLength(1) || matrix[CurrentShape.y + i, nextX] == 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool CanMoveDown()
        {
            for (int i = 0; i < CurrentShape.map.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.map.GetLength(1); j++)
                {
                    if (CurrentShape.map[i, j] == 1)
                    {
                        int nextY = CurrentShape.y + i + 1;
                        if (nextY >= matrix.GetLength(0) || (nextY >= 0 && matrix[nextY, CurrentShape.x + j] == 2))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void DrawMap(Graphics g)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 2 && ColorofFallen[i, j] != null)
                    {
                        g.FillRectangle(ColorofFallen[i, j], new Rectangle(280 + j * size + 1, 80 + i * size + 1, size - 1, size - 1));
                    }
                }
            }
        }

        private void DrawCurrentShape(Graphics g)
        {

            for (int i = 0; i < CurrentShape.map.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.map.GetLength(1); j++)
                {
                    if (CurrentShape.map[i, j] == 1)
                    {
                        g.FillRectangle(CurrentShape.Color, new Rectangle(280 + (CurrentShape.x + j) * size + 1, 80 + (CurrentShape.y + i) * size + 1, size - 1, size - 1));
                    }
                }
            }
        }

        private void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= 20; i++)
            {
                g.DrawLine(Pens.White, new Point(280, 80 + i * size), new Point(280 + 10 * size, 80 + i * size));
            }
            for (int i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.White, new Point(280 + i * size, 80), new Point(280 + i * size, 80 + 20 * size));
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            DrawMap(e.Graphics);
            DrawCurrentShape(e.Graphics);
            DrawNextShape();

        }

    }
}
