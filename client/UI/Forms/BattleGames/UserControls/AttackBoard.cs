using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    public class EventArgsPosition : EventArgs
    {
        public int x;
        public int y;
        public EventArgsPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public partial class AttackBoard : UserControl
    {
        public Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        // triggerear esto con un evento cuando se haga clic en un botón
        private bool attackingShip = false;
        private Stack<Button> shipsAttacked = new Stack<Button>();
        public static event EventHandler<EventArgsPosition> shipAttackedPosition;


        public AttackBoard()
        {
            this.Hide();
            InitializeComponent();
            ButtonAttackAction.readyToAttack += readyToAttackEventHandler;
            label1.Text = string.Empty;
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
            Game.serverResponseEvent += Game_serverResponseEvent;
            #endregion
        }

        private void Game_serverResponseEvent(object sender, ServerResponseEvent e)
        {
            var button = this.shipsAttacked.Peek();
            if (e.response == "tocado")
            {
                button.BackColor = Color.CadetBlue;
            }
            if (e.response == "agua")
            {
                button.BackColor = Color.Red;
            }
        }

        private void readyToAttackEventHandler(object sender, EventArgs e)
        {
            label1.Text = "Listo para lanzar misiles";
            this.attackingShip = true;
        }

        #region Button Events
        private void Button_Click(object sender, EventArgs e)
        {
            if (attackingShip)
            {
                // Manejar el clic del botón aquí
                Button clickedButton = (Button)sender;
                string[] location = clickedButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                if (boardButtons[row, col].Enabled)
                {
                    boardButtons[row, col].Enabled = false;
                    this.attackingShip = false;
                    this.shipsAttacked.Push(boardButtons[row, col]);
                    label1.Text = $"Misil lanzado en la posicion. Fila:{row}, Columna:{col}";
                    

                    shipAttackedPosition.Invoke(this, new EventArgsPosition(col, row));
                }
                else
                {
                    label1.Text = "No se puede atacar un barco ya atacado";
                }
            }
            #endregion
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (attackingShip)
            {
                Button leavedButton = (Button)sender;
                string[] location = leavedButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                if (boardButtons[row, col].Enabled == false)
                    return;
                boardButtons[row, col].BackColor = Color.Transparent;
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (attackingShip)
            {
                Button hoveredButton = (Button)sender;
                string[] location = hoveredButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                if (boardButtons[row, col].Enabled == false)
                    return;
                boardButtons[row, col].BackColor = Color.LightPink;
            }
        }
    }
}
