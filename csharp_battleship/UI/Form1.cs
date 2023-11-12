using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TrabajoPractico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Name = "Login";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox3.Hide();
            this.label4.Hide();
            this.button3.Hide();
        }

        private bool VerificarButton_Click()
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            string passwordPattern = @"^.{8,}$";

            bool emailIsValid = Regex.IsMatch(textBox1.Text, emailPattern);
            bool passwordIsValid = Regex.IsMatch(textBox2.Text, passwordPattern);

            if (emailIsValid && passwordIsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!VerificarButton_Click())
            {
                MessageBox.Show("Hay un error en el ingreso de datos, verifique si escribio bien sus datos.");
                return;
            }
            var u = new UsuarioBLL(textBox1.Text, textBox2.Text);
            u.BuscarUsuario();
            this.Hide();
            var newF = new Sala();
            newF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label4.Show();
            this.textBox3.Show();
            this.button3.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!VerificarButton_Click())
            {
                MessageBox.Show("Hay un error en el ingreso de datos, verifique si escribio bien sus datos.");
                return;
            }
            var u = new UsuarioBLL(textBox1.Text, textBox2.Text);
            u.RegistrarUsuario();
        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                await SocketCliente.StartClientAsync("0000#C[255]");
                Console.WriteLine(SocketCliente.response);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                await SocketCliente.StartClientAsync("0000#E[255]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fj = new FormJuego();
            fj.Show();
        }
    }
}
