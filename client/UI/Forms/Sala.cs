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

        private void Sala_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Forms.BattleGames.BSG();
            form.Show();
        }
    }
}
