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
    public partial class UserControl1 : UserControl
    {

        int pos;
        string hxpos;
        static event EventHandler UserControlChanged;

        public UserControl1()
        {
            InitializeComponent();
            FormJuego.BarcoSeleccionado += newShipSelected;
            create();
        }

        private void newShipSelected(object sender, EventCellOutter e)
        {

        }
        
       public void create()
       {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.Tan;
            var numerothis = new Label();
            hxpos = IntToHexString(pos);
            numerothis.Location = new Point((this.Width / 2), (this.Height / 2));
            this.Controls.Add(numerothis);
        }
        /*
        public UserControl1(float widthCol=150, float heightCol=150, int i, int j) : this()
        {
           // this.Width = int.Parse(widthCol.ToString());
          //  this.Height = int.Parse(heightCol.ToString());
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.Tan;
            var numerothis = new Label();
            numerothis.Text = $"{i.ToString()}{j.ToString()}";
            pos = i * 16 + j;
            hxpos = IntToHexString(pos);
            numerothis.Location = new Point((this.Width / 2), (this.Height / 2));
            this.Controls.Add(numerothis);
            this.Location = new Point(j * this.Width, i * this.Height);
        }
        */

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private string IntToHexString(int i)
        {
            if (i < 0 || i > 255)
            {
                throw new ArgumentException("El valor debe estar en el rango de 0 a 255.");
            }

            Dictionary<int, string> hxmap = new Dictionary<int, string>() {
                {10, "A"},
                {11, "B"},
                {12, "C"},
                {13, "D"},
                {14, "E"},
                {15, "F"},
            };

            if (i < 10)
            {
                return i.ToString(); // Si es menor que 10, la representación hexadecimal es simplemente el valor en sí.
            }
            else
            {
                int oneR = i / 16;
                int nR = i % 16;
                return (hxmap.ContainsKey(oneR) ? hxmap[oneR] : oneR.ToString()) + (hxmap.ContainsKey(nR) ? hxmap[nR] : nR.ToString());
            }
        }
    }
}