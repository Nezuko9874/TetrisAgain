using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisAgain
{
    public class Tetramino
    {
        public int[,] Shape { get; private set; }

        public Tetramino(int[,] shape)
        {
            Shape = shape;
           
        }
    }
}
