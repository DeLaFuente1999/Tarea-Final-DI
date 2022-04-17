using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TareaFinalDelaFuente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class login : Window
    {
        MySqlConnection conn;
        BDUtils bd = new BDUtils();
        public login()
        {
            InitializeComponent();
            
            try
            {
                conn = bd.ConexionDB();
                conn.Open();
            } 
            catch (Exception)
            {
                MessageBox.Show("No se ha podido conectar con la base de datos");
                this.Close();
            }

        }

        private void Login(object sender, RoutedEventArgs e)
        {

            var cmd = new MySqlCommand();
            cmd.Connection = conn;

            String query = "SELECT passwd FROM USUARIOS WHERE usuario = \"" + usernameTxt.Text.ToString() + "\"";

            MySqlCommand mycomand = new MySqlCommand(query, conn);

            MySqlDataReader myreader = mycomand.ExecuteReader();

            myreader.Read();


            try
            {
                String strnombre = myreader["passwd"].ToString();
                if (passwdTxt.Password == strnombre)
                {
                    productShow win2 = new productShow();
                    win2.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error en el Login");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("USUARIO NO ENCONTRADO");
            }


            

            myreader.Close();
            
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            register reg = new register();
            reg.Show();
            this.Close();
        }



        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                String documentation = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ayuda"), "documentacion.pdf");
                System.Diagnostics.Process.Start(documentation);
            }
        }
    }
}
