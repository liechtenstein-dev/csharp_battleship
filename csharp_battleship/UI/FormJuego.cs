using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPractico.Extras;

namespace TrabajoPractico
{
    public partial class FormJuego : Form
    {
        char[,] islands = new char[15,15]; 
        int[] pos;
        int selected_ship;
        
        Timer timer;
        Point coord;

        int selected_ship_arrays;
        int[] availableMoves = new int[5] { 5, 4, 3, 2 , 1};
        int[] posibleMoves = new int[5] { 5, 4, 3, 2, 1 };
        Dictionary<int, Color> type_ships_color = new Dictionary<int, Color>()
        {
            {1, Color.Crimson },
            {2, Color.Blue },
            {3, Color.Green },
            {4, Color.Red },
            {5, Color.Yellow },
        };
        // TODO: Could be enums 
        Dictionary<int, int> type_ships_moves = new Dictionary<int, int>()
        {
        };
        public static event EventHandler<EventCellOutter> BarcoSeleccionado;
        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            pos = new int[2];
            pos[0] = col;
            pos[1] = row;

            return new Point(col, row);
        }
        
        void PaintCellBasedOnShip(Control control)
        {
            if (selected_ship_arrays < 0 || selected_ship_arrays > 5)
                throw new Exception("Something went wrong");
            availableMoves[selected_ship_arrays]--;
            Console.WriteLine(pos[0]+(pos[1] * 16));
            type_ships_moves.Add(pos[0]+(pos[1]*16), selected_ship);
            control.BackColor = type_ships_color[selected_ship];
        }
        public FormJuego()
        {
            InitializeComponent();
            this.tableLayoutPanel1.BackColor = Color.Gray;
            timer = new Timer { Interval = 1000 };
            timer.Tick += OnTick;
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.comboBox1.DataSource = new int[] { 1, 2, 3, 4, 5 };
           
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    islands[i,j] = '0';
                }
            }
        }

        private void OnTick(object sender, EventArgs arg)
        {
            timer.Stop();
            coord = Cursor.Position;
            Console.WriteLine("Cordenads + delta de tiempo: " + coord);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_ship = int.Parse(comboBox1.SelectedItem.ToString());
            selected_ship_arrays = selected_ship - 1;
            MessageBox.Show($"Barco elegido {selected_ship}");
        }
        bool CalculateNearPosition()
        {
            /*
             *  Filtro los movimientos registrados con el barcoID que esta seleccionado
             *  Para tener los movimientos/puntos del barco en cuestión y en base a eso
             *  Volver a filtrar con los posibles movimientos
             */
            var selectedShipKeys = type_ships_moves.Where(pair => pair.Value == selected_ship)
                                                  .Select(pair => pair.Key)
                                                  .ToList();
            var pos_ship = pos[0] + pos[1]*16;
            var posibleMovesLR = new List<int>()
            {
                {pos_ship + 1},
                {pos_ship - 1},
            };
            var posibleMovesUD = new List<int>()
            {
                {pos_ship - 16},
                {pos_ship + 16},
            };
            var allPosibleMoves = new List<int>();
            allPosibleMoves.AddRange(posibleMovesLR);
            allPosibleMoves.AddRange(posibleMovesUD);
            // Interseccióno los movimientos posibles con los que tengo registrados
            // siempre un barco debe de tener minimo 1 pieza conectada
            var moves = allPosibleMoves.Intersect(selectedShipKeys);

            // si tiene 2 piezas conectadas tengo q saber la orientación del barco
            if (selectedShipKeys.Count >= 2 && moves.Any())
            {
               var orientation = GetOrientation(selectedShipKeys[0], selectedShipKeys[1]);

                switch (orientation)
                {
                    case Orientation.Horizontal:
                        return (IsHorizontalMove(selectedShipKeys.Max(), pos_ship) || IsHorizontalMove(selectedShipKeys.Min(), pos_ship));
                    case Orientation.Vertical:
                        return (IsVerticalMove(selectedShipKeys.Max(), pos_ship) || IsVerticalMove(selectedShipKeys.Min(), pos_ship));
                    default:
                        break;
                }
            } 
            return moves.Any();
        }

        private Orientation GetOrientation(int m1, int m2)
        {
            // Determinar la orientación en función de las coordenadas de los dos movimientos.
            if (Math.Abs(m1 - m2) == 1)
            {
                return Orientation.Horizontal;
            }
            else
            {
                return Orientation.Vertical;
            }
        }

        private bool IsHorizontalMove(int move1, int move2)
        {
            return Math.Abs(move1 - move2) == 1;
        }

        private bool IsVerticalMove(int move1, int move2)
        {
            return Math.Abs(move1 - move2) == 16;
        }

        private bool LegalMove()
        {
            // no hay más movimientos posibles
            if (availableMoves[selected_ship_arrays] == 0) return false;
            // hay un barco en la pos que se quiere acceder
            if (type_ships_moves.ContainsKey(pos[0] * 16 + pos[1])) return false;
            // ya se hizo una jugada con ese barco
            if (posibleMoves[selected_ship_arrays] > availableMoves[selected_ship_arrays])
            {
                // busco para donde encaran los barcos que se movieron
                return CalculateNearPosition();
            }
            return true;

        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            this.Activate();
            timer.Start();
            // Obtener la posición de la celda correspondiente al clic
            // Console.WriteLine(Cursor.Position);
            // Console.WriteLine(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y).ToString());
            Console.WriteLine(coord);


            Point? cellPos = GetRowColIndex(this.tableLayoutPanel1, tableLayoutPanel1.PointToClient(Cursor.Current.HotSpot));
            
            if (cellPos.HasValue && LegalMove())
            {
                var control = new Panel();
                tableLayoutPanel1.Controls.Add(control, pos[0], pos[1]);
                islands[pos[0], pos[1]] = '1';
                PaintCellBasedOnShip(control);
               //Realiza acciones basadas en la celda clicada, por ejemplo, mostrar en un MessageBox
                MessageBox.Show($"Celda: Columna {pos[0]} Fila {pos[1]}");
            }

        }

    }
}
