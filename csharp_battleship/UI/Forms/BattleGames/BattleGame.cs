using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPractico.Forms.BattleGames.UserControls;

namespace TrabajoPractico.Forms
{
    public partial class BattleGameForm : Form
    {
        private Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        public BattleGameForm()
        {
            InitializeComponent();
        }

        private void BattleGame_Load(object sender, EventArgs e)
        {
            ShipSelector.ShipSelected += ShipSelector_ShipSelected;
            // Hacer que el formulario no sea redimensionable
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 
            // JASDJASJDAS NO PODES MAXIMIZAR AMIGUITO
            // Deshabilito el botón de maximizar 
            this.MaximizeBox = false; 
        }

        private void ShipSelector_ShipSelected(object sender, EventSelector e)
        {
            Ships selectedShip = e.state;
            MessageBox.Show($"Selected ship: {selectedShip}");
        }

        #region Initial Configuration 
        #endregion
        #region Button Logic

        #endregion
    }
}
