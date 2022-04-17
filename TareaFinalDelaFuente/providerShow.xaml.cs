using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class providerShow : Window
    {
        DataTable dt;
        MySqlConnection conn;
        BDUtils bd = new BDUtils();


        int itemNumber = 0;
        int maxItemsBD;
        public providerShow()
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
            dt = bd.RellenarDt("SELECT * FROM proveedores;", conn);
            maxItemsBD = dt.Rows.Count;
        }

        private void CargarContenido(DataTable dt)
        {

            if (dt.Rows.Count != 0)
            {
                providerName.Text = dt.Rows[itemNumber].Field<String>("nombreusuario");
                phoneNumber.Text = dt.Rows[itemNumber].Field<Int32>("telefono").ToString();
                cif.Text = dt.Rows[itemNumber].Field<String>("cif");
                email.Text = dt.Rows[itemNumber].Field<String>("email");
                address.Text = dt.Rows[itemNumber].Field<String>("direccion");
                backaccount.Text = dt.Rows[itemNumber].Field<String>("cuentabanco");
            }
            else
            {
                providerName.Text = "No data";
                phoneNumber.Text = "No data";
                cif.Text = "No data";
                email.Text = "No data";
                address.Text = "No data";
                backaccount.Text = "No data";
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

            if (itemNumber + 1 < maxItemsBD)
            {
                itemNumber++;
                CargarContenido(dt);
            }
        }

        private void deleteItemOnShow(object sender, RoutedEventArgs e)
        {

            if (dt.Rows.Count != 0)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea realmente eliminar el proveedor mostrado?", "Eliminación del proveedor", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try
                        {

                            String proveedorDelete = (dt.Rows[itemNumber].Field<String>("cif"));

                            conn.Open();

                            var cmd = new MySqlCommand();
                            cmd.Connection = conn;

                            MySqlCommand comm = conn.CreateCommand();


                            cmd.CommandText = "DELETE FROM PROVEEDORES WHERE cif = ?cif";
                            cmd.Parameters.Add("?cif", MySqlDbType.VarChar).Value = proveedorDelete;


                            try
                            {
                                cmd.ExecuteNonQuery();

                                itemNumber = 0;

                                dt.Clear();
                                RellenarTabla();
                                CargarContenido(dt);

                                conn.Close();

                            }

                            catch (MySqlException ex)
                            {
                                MessageBox.Show("No se puede eliminar un proveedor que tenga productos relacionados. Borre primero los productos relacionados.");
                            }
                        }

                        catch (MySqlException)
                        {

                        }
                        break;
                    case MessageBoxResult.No:
                        break;
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
