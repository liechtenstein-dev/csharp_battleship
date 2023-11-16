using System;
using System.Windows.Forms;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    public partial class ButtonAttackAction : UserControl
    {
        public static event EventHandler readyToAttack;
        public ButtonAttackAction()
        {
            this.Hide();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readyToAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}
