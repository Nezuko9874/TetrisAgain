using System;
using System.Windows.Forms;

namespace TetrisAgain
{
    public partial class Winner : Form
    {
        public Winner(int score, int lines)
        {
            InitializeComponent();

            label5.Text = "" + score;
            label6.Text = "" + lines;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Choice choice = new Choice();

            this.Hide();

            choice.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
