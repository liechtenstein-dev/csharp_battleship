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
        // Game state Moves
        int[] pos;

        Timer timer;
        Point coord;

        int selected_ship_arrays;
        Dictionary<int, Color> type_ships_color = new Dictionary<int, Color>()
        {
            { 1, Color.Crimson },
            { 2, Color.Blue },
            { 3, Color.Green },
            { 4, Color.Red },
            { 5, Color.Yellow },
        };

        // TODO: Could be enums
        Dictionary<int, int> type_ships_moves = new Dictionary<int, int>() { };
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
            Console.WriteLine(pos[0] + (pos[1] * 16));
            type_ships_moves.Add(pos[0] + (pos[1] * 16), selected_ship);
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

        // una fumada increible esta por ser acontecida aqui
        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Activate();
            timer.Start();
            Console.WriteLine(coord);

            Point? cellPos = GetRowColIndex(
                this.tableLayoutPanel1,
                tableLayoutPanel1.PointToClient(Cursor.Position)
            );

            if (cellPos.HasValue && LegalMove())
            {
                var control = new Panel();
                tableLayoutPanel1.Controls.Add(control, pos[0], pos[1]);
                PaintCellBasedOnShip(control);
                //Realiza acciones basadas en la celda clicada, por ejemplo, mostrar en un MessageBox
                MessageBox.Show($"Celda: Columna {pos[0]} Fila {pos[1]}");
            }
        }
    }
}
