using System;
using System.Windows.Forms;

namespace TrabajoPractico
{
    public partial class Sala : Form
    {
        public Sala()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var chat = new Chat();
            chat.Show();
        }
    }
}
