using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TrabajoPractico.Forms.BattleGame;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    public partial class ShipSelector : UserControl
    {
        public static event EventHandler<EventSelector> ShipSelected;
        public static event EventHandler<EventArgs> AllShipsInPosition;
        public Rotation rotation = 0;
        List<Ships> shipsAvailable;
        public ShipSelector()
        {
            InitializeComponent();
            shipsAvailable = new List<Ships>()
            {
                Ships.BattallaNaval,
                Ships.PortaAviones,
                Ships.Fragatas,
                Ships.Acorazados,
                Ships.Buque
            };
            this.comboBox1.DataSource = shipsAvailable;
            GameBoard.shipCreated += deleteShipUsed;
        }

        private void deleteShipUsed(Ships obj)
        {
            comboBox1.DataSource = null;
            shipsAvailable.Remove(obj);
            comboBox1.DataSource = shipsAvailable;

            if (comboBox1.Items.Count == 0)
            {
                this.Hide();
                AllShipsInPosition?.Invoke(this, new EventArgs());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) { return; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!comboBox1.Enabled) { return; }
            if (comboBox1 == null) { return; }
            if (comboBox1.Items.Count == 0) { return; }
            if (comboBox1.SelectedIndex < 0) { return; }
            rotation++;
            if ((int)rotation > 3)
                rotation = 0;
            Ships selectedShip = (Ships)comboBox1.SelectedItem;
            ShipSelected?.Invoke(this, new EventSelector(selectedShip, rotation));
        }
    }
    public enum Ships
    {
        BattallaNaval = 5,
        PortaAviones = 4,
        Fragatas = 3,
        Buque = 2,
        Acorazados = 1
    }

    public enum Rotation
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3,
    }

    public class EventSelector : EventArgs
    {
        public Ships state;
        public Rotation rotation;
        public EventSelector(Ships ship, Rotation rotate)
        {
            this.state = ship;
            this.rotation = rotate;
        }
    }

}
