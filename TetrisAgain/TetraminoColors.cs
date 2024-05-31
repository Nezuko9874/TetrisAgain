using System.Drawing;


namespace Tetris
{
    public class TetraminoColors
    {
        public static Brush[] Colors;
    
        public void ChoiceColor(bool useVariant2)
        {
            if (!useVariant2)
            {
                Colors = new Brush[]
           {
        Brushes.Blue,   // І
        Brushes.Purple, // T
        Brushes.Red,    // Z
        Brushes.Green,  // S
        Brushes.Yellow, // O
        Brushes.Orange,// L
        Brushes.Cyan
           };
            }
            else
            {
                Colors = new Brush[]
        {
       Brushes.Blue,   
       Brushes.Purple, 
       Brushes.Red,    
       Brushes.Green,  
       Brushes.Yellow, 
       Brushes.Orange,
       Brushes.Cyan,
       Brushes.DarkSeaGreen,
       Brushes.Fuchsia,
       Brushes.DarkBlue,
       Brushes.GreenYellow,
       Brushes.Indigo,
       Brushes.LightSalmon,
       Brushes.LemonChiffon,
       Brushes.Lime,
       Brushes.BlueViolet,
       Brushes.SeaShell,
       Brushes.Tan
        };
            }
        }
       
    }
}
