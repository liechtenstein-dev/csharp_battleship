using BLL;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TrabajoPractico
{
    public partial class Form1 : Form
    {
        BLLUsuario bllusuario = new BLLUsuario();
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
            string emailPattern = @"^[^\s@]+@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
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
            bool existe = bllusuario.BuscarUsuario(textBox1.Text, textBox2.Text);
            if (existe)
            {
                this.Hide();
                var newF = new Sala();
                newF.Show();
            } else
            {
                MessageBox.Show("No existe ese usuario");
            }
            
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
            bllusuario.RegistrarUsuario(textBox1.Text, textBox2.Text, textBox3.Text);
            this.textBox3.Hide();
            this.label4.Hide();
            MessageBox.Show("Registrado exitosamente");
        }
    }
}
