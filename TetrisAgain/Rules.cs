using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();

            this.Hide();

            menu.Show();
        }
    }
}
