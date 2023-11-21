using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPractico.Forms.BattleGame;

namespace TrabajoPractico.Forms.BattleGames
{
    public partial class BSG : Form
    {
        public BSG()
        {
            InitializeComponent();
        }

        private void BSG_Load(object sender, EventArgs e)
        {
            // Hacer que el formulario no sea redimensionable
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // JASDJASJDAS NO PODES MAXIMIZAR AMIGUITO
            // Deshabilito el botón de maximizar 
            this.MaximizeBox = false;
            this.game1.winsGame += winsGameEvent;
            GameBoard.gameLoss += GameBoard_gameLoss;
        }

        private void GameBoard_gameLoss(object sender, EventArgs e)
        {
            // frase robada de un video de un pelado que toma cereales jasdjas
            MessageBox.Show("La vida da muchas vueltas, pero aveces se aprende a no girar");
            this.Close();
        }

        private void winsGameEvent(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
