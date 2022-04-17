using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TareaFinalDelaFuente
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class register : Window
    {

        MySqlConnection conn;
        BDUtils bd = new BDUtils();
        public register()
        {
            InitializeComponent();
            conn = bd.ConexionDB();
            conn.Open();
        }

        private void RegistrarUsuario(object sender, RoutedEventArgs e)
        {
            if (passw1.Password != passw2.Password) 
            {
                MessageBox.Show("Las contraseñas no coinciden");
            } 
            
            else
            {
                conn = bd.ConexionDB();

                conn.Open();

                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                MySqlCommand comm = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO USUARIOS (usuario, passwd) VALUES (?usuario, ?passwd);";

                cmd.Parameters.Add("?usuario", MySqlDbType.VarChar).Value = username.Text.ToString();
                cmd.Parameters.Add("?passwd", MySqlDbType.VarChar).Value = passw1.Password;


                try
                {
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("El usuario " + username.Text.ToString() + " se agregó correctamente.", "Accion Completada");


                    
                } catch (Exception)
                {
                    MessageBox.Show("El usuario ya existe, intentelo con otro nombre");
                }
                
            }
        }

        private void cerrando(object sender, System.ComponentModel.CancelEventArgs e)
        {
            login login = new login();
            login.Show();
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
 