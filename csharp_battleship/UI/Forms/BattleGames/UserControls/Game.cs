using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPractico.Forms.BattleGames.CLSocket;
using TrabajoPractico.Forms.BattleGames.UserControls;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    public class ServerResponseEvent : EventArgs
    {
        public string response;
        public ServerResponseEvent(string r) {
            this.response = r;
        }    
    }
    public partial class Game : UserControl
    {
        private Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        public event EventHandler winsGame;
        public static event EventHandler<ServerResponseEvent> serverResponseEvent; 
        
        private SocketCliente cliente;
        public Game()
        {
            InitializeComponent();
        }
        private void Game_Load(object sender, EventArgs e)
        {
            ShipSelector.ShipSelected += ShipSelector_ShipSelected;
            ShipSelector.AllShipsInPosition += ShipSelector_AllShipsInPosition;
            AttackBoard.shipAttackedPosition += AttackBoard_shipAttackedPosition;
        }

        private void AttackBoard_shipAttackedPosition(object sender, EventArgsPosition e)
        {
            if (sender != null)
            {
                Console.WriteLine($"ATACO A: {e.x}, {e.y}");
                string x = e.x.ToString();
                string y = e.y.ToString();
                if (e.x < 10)
                    x = $"0{e.x}";
                if (e.y < 10)
                    y = $"0{e.y}";
                cliente.SendMessageToServer($"[{y},{x}]");
                cliente.MessageReceived += Cliente_MessageReceived;

            }
        }

        private void Cliente_MessageReceived(object sender, string e)
        {
            bool disparo = e.StartsWith("[");

            if (disparo)
            {
                Console.WriteLine($"Disparo del sv: {e}");
                return;
            }

            switch (e)
            {
                case "tocado":
                    serverResponseEvent?.Invoke(this, new ServerResponseEvent(e));
                    break;
                case "agua":
                    serverResponseEvent?.Invoke(this, new ServerResponseEvent(e));
                    break;
                case "gg":
                    MessageBox.Show("Ganaste");
                    winsGame?.Invoke(this, EventArgs.Empty);
                    break;
                default: Console.WriteLine($"Acciones de respuesta de disparo del sv: {e}");
                    break;
            }

                Console.WriteLine(e);
        }

        private void ShipSelector_AllShipsInPosition(object sender, EventArgs e)
        {
            Console.WriteLine("All ships in position event");
            cliente = new SocketCliente();
            cliente.StartSocket();
            Console.WriteLine("Socket cliente disparado");
            this.attackBoard1.Show();
            this.buttonAttackAction1.Show();
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
