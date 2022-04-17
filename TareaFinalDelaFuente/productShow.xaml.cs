using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para Window3.xaml
    /// </summary>
    public partial class productShow : Window
    {

        DataTable dt;
        MySqlConnection conn;
        BDUtils bd = new BDUtils();


        int itemNumber = 0;
        int maxItemsBD;
        public productShow()
        {
            InitializeComponent();
           

            conn = bd.ConexionDB();
            
            conn.Open();
            RellenarTabla();
            CargarContenido(dt);
            conn.Close();
        }


        private void RellenarTabla()
        {
            dt = bd.RellenarDt("SELECT * FROM PRoducto;", conn);
            maxItemsBD = dt.Rows.Count;
        }

        private void CargarContenido(DataTable dt)
        {

            if (dt.Rows.Count != 0) {

                itemName.Text = dt.Rows[itemNumber].Field<String>("nombre");
                refProduct.Text = dt.Rows[itemNumber].Field<String>("referencia");
                precioProduct.Text = dt.Rows[itemNumber].Field<String>("precio");
                proveName.Text = dt.Rows[itemNumber].Field<String>("proveedor");


                String imageName = dt.Rows[itemNumber].Field<String>("imagen");
                String baseImage;

                if (imageName != null)
                {
                    baseImage = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "imageProduct"), imageName);
                }
                else
                {
                    baseImage = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "resources"), "base.png");

                }

                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                myBitmapImage.UriSource = new Uri(baseImage);
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                imageProduct.Source = myBitmapImage;


            } else
            {
                itemName.Text = "No data";
                refProduct.Text = "No data";
                precioProduct.Text = "No data";
                proveName.Text = "No data";

                String baseImage = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "resources"), "base.png");

                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(baseImage);
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                imageProduct.Source = myBitmapImage;
                myBitmapImage = null;
            }

        }
        private void Button_Click_Atras(object sender, RoutedEventArgs e)
        {
            if (itemNumber > 0)
            {
                itemNumber--;
                CargarContenido(dt);
            }


        } 
        private void Button_Click_Siguiente(object sender, RoutedEventArgs e)
        {
            
            if (itemNumber+1 < maxItemsBD) 
            {
                itemNumber++;
                CargarContenido(dt);
            }
        }

        private void deleteItemOnShow(object sender, RoutedEventArgs e)
        {

            if (dt.Rows.Count != 0)
            {


                MessageBoxResult result = MessageBox.Show("¿Desea realmente eliminar el producto mostrado?", "Eliminación de producto", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            String imagenProducto = "";
                            String imageName = dt.Rows[itemNumber].Field<String>("imagen");
                            if (imageName != null)
                            {
                                imagenProducto = System.IO.Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "imageProduct"), imageName);
                            }

                            String itemDelete = (dt.Rows[itemNumber].Field<String>("referencia"));

                            conn.Open();

                            var cmd = new MySqlCommand();
                            cmd.Connection = conn;

                            MySqlCommand comm = conn.CreateCommand();


                            cmd.CommandText = "DELETE FROM PRODUCTO WHERE referencia = ?nombre";
                            cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = itemDelete;


                            try
                            {
                                cmd.ExecuteNonQuery();

                                itemNumber = 0;

                                dt.Clear();
                                RellenarTabla();
                                CargarContenido(dt);

                                conn.Close();

                                if (imageName != null)
                                {
                                    File.Delete(imagenProducto);
                                }
                            }

                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        catch (MySqlException)
                        {

                        }
                        break;

                    case MessageBoxResult.No:
                        break;
                }
            } else
            {
                MessageBox.Show("No hay elementos disponibles para eliminar");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //login log = new login();
            //log.Owner = this;
            //log.Show();
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
