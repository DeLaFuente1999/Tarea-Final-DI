using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    public partial class productAdd : Window
    {
        String filePath;
        OpenFileDialog dialog;
        String rutaCarpetaItems = "imageProduct";
        String nombre;
        MySqlConnection conn;
        BDUtils bd;
        public productAdd()
        { 
            InitializeComponent();
            bd = new BDUtils();
            conn = bd.ConexionDB();
            conn.Open();
            generarItems();
        }

        private void createItem_Click(object sender, RoutedEventArgs e)
        {

            if (nombreproducto.Text == "" || referenciaproducto.Text == "" || proveedorCB.Text == "")
            {
                MessageBox.Show("El campo Nombre, Referencia y Proveedor son OBLIGATORIOS", "ERROR");
            }
            else
            {

                try
                {
                    conn = bd.ConexionDB();

                    conn.Open();

                    var cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    MySqlCommand comm = conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO PRODUCTO (nombre, referencia, precio, proveedor, imagen) VALUES (?nombre, ?referencia, ?precio, ?proveedor, ?imagen);";

                    cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombreproducto.Text.ToString();
                    cmd.Parameters.Add("?referencia", MySqlDbType.VarChar).Value = referenciaproducto.Text.ToString();
                    cmd.Parameters.Add("?precio", MySqlDbType.VarChar).Value = precioproducto.Text.ToString();
                    cmd.Parameters.Add("?proveedor", MySqlDbType.VarChar).Value = proveedorCB.Text.ToString();
                    cmd.Parameters.Add("?imagen", MySqlDbType.VarChar).Value = nombre;

                    MessageBox.Show("El producto " + nombreproducto.Text.ToString() + " se agregó correctamente.", "Accion Completada");

                    nombreproducto.Text = "";
                    referenciaproducto.Text = "";
                    precioproducto.Text = "";
                    proveedorCB.Text = "";

                    String baseImage = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "resources"), "base.png");

                    BitmapImage myBitmapImage = new BitmapImage();
                    myBitmapImage.BeginInit();
                    myBitmapImage.UriSource = new Uri(baseImage);
                    myBitmapImage.DecodePixelWidth = 200;
                    myBitmapImage.EndInit();

                    imagenProducto.Source = myBitmapImage;

                    if (filePath != null)
                    {
                        try
                        {
                            File.Copy(filePath, System.IO.Path.Combine(rutaCarpetaItems, nombre));
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception excp)
                        {
                            MessageBox.Show(excp.ToString());
                        }
                    } else
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception excp)
                        {
                            MessageBox.Show(excp.ToString());
                        }
                    }
                }

                catch (SqlException) { }
            }
        }


        private void generarItems()
        {

            var cmd = new MySqlCommand();
            cmd.Connection = conn;

            MySqlCommand comm = conn.CreateCommand();
            cmd.CommandText = "SELECT nombreusuario FROM proveedores";

            cmd.ExecuteNonQuery();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                proveedorCB.Items.Add(reader["nombreusuario"].ToString());
            }

        }

        private void addImage(object sender, RoutedEventArgs e)
        {

            dialog = new OpenFileDialog();
            dialog.Filter = "JPG images (*.jpg)|*.jpg|PNG images (*.png)|*.png|JPEG images (*.jpeg)|*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
                nombre = System.IO.Path.GetFileName(filePath);

                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(filePath);
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                imagenProducto.Source = myBitmapImage;

                var fileStream = dialog.OpenFile();

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
            this.Visibility = Visibility.Collapsed;
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
