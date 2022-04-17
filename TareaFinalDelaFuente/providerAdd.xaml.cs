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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class providerAdd : Window
    {

        MySqlConnection conn;
        BDUtils bd;
        public providerAdd()
        {
            InitializeComponent();

            bd = new BDUtils();
            conn = bd.ConexionDB();
            conn.Open();
        }

        private void CreateProvider(object sender, RoutedEventArgs e)
        {

            if (nombreproveedor.Text == "" || telefonoproveedor.Text == "" || cifproveedor.Text == "")
            {
                MessageBox.Show("El campo Nombre, Telefono y CIF son OBLIGATORIOS", "ERROR");
            } else
            {
                try
                {

                    conn = bd.ConexionDB();

                    conn.Open();

                    var cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    MySqlCommand comm = conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO PROVEEDORES VALUES (?nombre, ?telefono, ?cif, ?email, ?direccion, ?cuenta)";

                    cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombreproveedor.Text.ToString();
                    cmd.Parameters.Add("?telefono", MySqlDbType.Int32).Value = telefonoproveedor.Text.ToString();
                    cmd.Parameters.Add("?cif", MySqlDbType.VarChar).Value = cifproveedor.Text.ToString();
                    cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = emailproveedor.Text.ToString();
                    cmd.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = direccionproveedor.Text.ToString();
                    cmd.Parameters.Add("?cuenta", MySqlDbType.VarChar).Value = bancoproveedor.Text.ToString();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("El usuario " + nombreproveedor.Text.ToString() + " se agregó correctamente", "Accion Completada");

                    nombreproveedor.Text = "";
                    telefonoproveedor.Text = "";
                    cifproveedor.Text = "";
                    emailproveedor.Text = "";
                    direccionproveedor.Text = "";
                    bancoproveedor.Text = "";
                }
                catch (MySqlException ex) 
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }

        private void openProviderAdd(object sender, RoutedEventArgs e)
        {
            providerAdd providerAdd = new providerAdd();
            providerAdd.Show();
            this.Close();
        }


        private void openProviderShow(object sender, RoutedEventArgs e)
        {
            providerShow providerShow = new providerShow();
            providerShow.Show();
            this.Close();
        }


        private void openProductAdd(object sender, RoutedEventArgs e)
        {
            productAdd productAdd = new productAdd();
            productAdd.Show();
            this.Close();
        }


        private void openProductShow(object sender, RoutedEventArgs e)
        {
            productShow productShow = new productShow();
            productShow.Show();
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                Form1 form = new Form1();
                form.Show();
            }
            if (e.Key == Key.F1)
            {
                String documentation = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ayuda"), "documentacion.pdf");
                System.Diagnostics.Process.Start(documentation);
            }
        }
    }
}
