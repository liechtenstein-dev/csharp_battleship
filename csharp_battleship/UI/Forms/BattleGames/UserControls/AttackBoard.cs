using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    public partial class AttackBoard : UserControl
    {
        public Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        // triggerear esto con un evento cuando se haga clic en un botón
        private bool attackingShip = false;
        public AttackBoard()
        {
            InitializeComponent();
            #region Basic Config and calling board configure
            // Configurar TableLayoutPanel para organizar los botones
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.AutoSize = false;

            // Crear botones y agregarlos al TableLayoutPanel
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    Button button = new Button();
                    button.FlatStyle = FlatStyle.Flat;
                    button.Tag = $"{row},{col}"; // la ubicación del botón
                    button.MouseEnter += Button_MouseEnter;
                    button.MouseLeave += Button_MouseLeave;
                    button.MouseClick += Button_Click;
                    // button.Click += Button_Click; // Manejador de evento Click para realizar acciones cuando se haga clic en un botón
                    this.tableLayoutPanel1.Controls.Add(button, col, row);
                    boardButtons[row, col] = button; // Almacenar el botón en el arreglo para acceder a él más tarde
                }
            }
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            #endregion
        }
    }
}
