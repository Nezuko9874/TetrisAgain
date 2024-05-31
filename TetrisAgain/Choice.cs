using System;
using System.Windows.Forms;

namespace TetrisAgain
{
    public partial class Choice : Form
    {
        public Choice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Functional form1 = new Functional(false);

            this.Hide();

            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Functional form1 = new Functional(true);

            this.Hide();

            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();

            this.Hide();

            menu.Show();
        }
    }
}
