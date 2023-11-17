using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPractico.Forms.BattleGame;
using TrabajoPractico.Forms.BattleGames.CLSocket;
using TrabajoPractico.Forms.BattleGames.UserControls;
using System.Messaging;

namespace TrabajoPractico.Forms.BattleGames.UserControls
{

    // TODO: Cambiale el nombre SRStatus
    public class ServerResponseEvent : EventArgs
    {
        public string response;
        public ServerResponseEvent(string r) {
            this.response = r;
        }    
    }
    
    
    public class SRAttack : EventArgs
    {
        public int x;
        public int y;

        public SRAttack(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public partial class Game : UserControl
    {
        private Button[,] boardButtons = new Button[15, 15]; // Arreglo para almacenar los botones del tablero
        public event EventHandler winsGame;
        public static event EventHandler<ServerResponseEvent> serverResponseEvent;
        public static event EventHandler<SRAttack> srAttackEvent;


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
            GameBoard.shipGotHit += GameBoard_shipGotHit;
            SocketCliente.MessageReceived += Cliente_MessageReceived;
        }



        private void GameBoard_shipGotHit(string obj)
        {
            Console.WriteLine("se manda al server si pego o no: " + obj);
            cliente.SendMessageToServer(obj);
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
            }
        }

        private void Cliente_MessageReceived(object sender, string e)
        {
            bool disparo = e.StartsWith("[");

            if (disparo)
            {
                string r = Regex.Replace(e, @"[\[\]{}]", "");
                var hittingCoords = r.Split(',');
                int x = int.Parse(hittingCoords[0]);
                int y = int.Parse(hittingCoords[1]);
                disparo = false;
                srAttackEvent?.Invoke(this, new SRAttack(x, y));
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
