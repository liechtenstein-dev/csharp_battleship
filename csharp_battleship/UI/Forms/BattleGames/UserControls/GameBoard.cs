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

namespace TrabajoPractico.Forms.BattleGame
{
    public partial class GameBoard : UserControl
    {
        private bool placingShip = false;
        private bool shipCorrectlyCreated = false;
        private Rotation rotation;
        private Ships typeShip;

        public Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        public static event Action<Ships> shipCreated;

        public GameBoard()
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
            ShipSelector.ShipSelected += ShipSelector_ShipSelected;
        }

        #region Ship selected logic
        private void ShipSelector_ShipSelected(object sender, EventSelector e)
        {
            placingShip = true;
            typeShip = e.state;
            rotation = e.rotation;
        }
        private void CalculateNearPreviews(int row, int col, Color color, bool create = false)
        {
            switch (typeShip)
            {
                case Ships.BattallaNaval:
                    CalculateRotationPreview(row, col, Ships.BattallaNaval, color, create);
                    break;
                case Ships.Fragatas:
                    CalculateRotationPreview(row, col, Ships.Fragatas, color, create);
                    break;
                case Ships.PortaAviones:
                    CalculateRotationPreview(row, col, Ships.PortaAviones, color, create);
                    break;
                case Ships.Acorazados:
                    CalculateRotationPreview(row, col, Ships.Acorazados, color, create);
                    break;
                case Ships.Buque:
                    CalculateRotationPreview(row, col, Ships.Buque, color, create);
                    break;
            }
        }
        private void CalculateRotationPreview(int row, int col, Ships ship, Color color, bool create = false)
        {
            int cellCounter = 0;
            switch (rotation)
            {
                case Rotation.up:
                    for (int i = 0; i < (int)ship; i++)
                    {
                        // que no se vaya de rango
                        // y que no exista otro barco ahi PORQUE SINO TE METO UNA HOSTIAAAA
                        if (row - i < 0 || !boardButtons[row - i, col].Enabled) break;
                        else
                        {
                            cellCounter++;
                            boardButtons[row - i, col].BackColor = color;
                        }

                    }
                    break;
                case Rotation.down:
                    for (int i = 0; i < (int)ship; i++)
                    {
                        if (row + i >= 15 || !boardButtons[row + i, col].Enabled)
                            break;
                        else
                        {
                            cellCounter++;
                            boardButtons[row + i, col].BackColor = color;
                        }
                    }
                    break;
                case Rotation.left:
                    for (int i = 0; i < (int)ship; i++)
                    {
                        if (col - i < 0 || !boardButtons[row, col - i].Enabled)
                            break;
                        else
                        {
                            cellCounter++;
                            boardButtons[row, col - i].BackColor = color;
                        }
                    }
                    break;
                case Rotation.right:
                    for (int i = 0; i < (int)ship; i++)
                    {
                        if (col + i >= 15 || !boardButtons[row, col + i].Enabled)
                            break;
                        else
                        {
                            cellCounter++;
                            boardButtons[row, col + i].BackColor = color;
                        }
                    }
                    break;
            }
            if (create == true && cellCounter == (int)ship)
            {
                Console.WriteLine("valid");
                switch (rotation)
                {
                    case Rotation.up:
                        for (int i = 0; i < (int)ship; i++)
                        {
                            boardButtons[row - i, col].Enabled = false;
                            boardButtons[row - i, col].BackColor = color;
                        }
                        break;
                    case Rotation.down:
                        for (int i = 0; i < (int)ship; i++)
                        {
                            boardButtons[row + i, col].Enabled = false;
                            boardButtons[row + i, col].BackColor = color;
                        }
                        break;
                    case Rotation.left:
                        for (int i = 0; i < (int)ship; i++)
                        {
                            boardButtons[row, col - i].Enabled = false;
                            boardButtons[row, col - i].BackColor = color;
                        }
                        break;
                    case Rotation.right:
                        for (int i = 0; i < (int)ship; i++)
                        {
                            boardButtons[row, col + i].Enabled = false;
                            boardButtons[row, col + i].BackColor = color;
                        }
                        break;
                }
                shipCorrectlyCreated = true;
            }
        }
        #endregion
        #region Button Events
        private void Button_Click(object sender, EventArgs e)
        {
            if (placingShip)
            {
                // Manejar el clic del botón aquí
                Button clickedButton = (Button)sender;
                string[] location = clickedButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                // Console.WriteLine($"Mouse click: row:{row} col:{col}");
                CalculateNearPreviews(row, col, Color.AliceBlue, true);
                if (shipCorrectlyCreated)
                {
                    Console.WriteLine("lo mando a delete xd");
                    shipCreated.Invoke(typeShip);
                    placingShip = false;
                }
                shipCorrectlyCreated = false;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (placingShip)
            {
                Button leavedButton = (Button)sender;
                string[] location = leavedButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                if (boardButtons[row, col].Enabled == false)
                    return;
                //  Console.WriteLine($"Mouse leave: row:{row} col:{col}");
                CalculateNearPreviews(row, col, Color.Transparent);
            }
        }
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (placingShip)
            {
                Button hoveredButton = (Button)sender;
                string[] location = hoveredButton.Tag.ToString().Split(',');
                int row = int.Parse(location[0]);
                int col = int.Parse(location[1]);
                if (boardButtons[row, col].Enabled == false)
                    return;
                //    Console.WriteLine($"Mouse enter: row:{row} col:{col}");
                CalculateNearPreviews(row, col, Color.LightGray);
            }
        }
        #endregion
    }
}
