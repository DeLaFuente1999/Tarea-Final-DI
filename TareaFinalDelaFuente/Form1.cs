using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TareaFinalDelaFuente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'alvaodooDataSet1.producto' Puede moverla o quitarla según sea necesario.
            this.productoTableAdapter.Fill(this.alvaodooDataSet1.producto);
            // TODO: esta línea de código carga datos en la tabla 'alvaodooDataSet1.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.alvaodooDataSet1.usuarios);
            // TODO: esta línea de código carga datos en la tabla 'alvaodooDataSet1.proveedores' Puede moverla o quitarla según sea necesario.
            this.proveedoresTableAdapter.Fill(this.alvaodooDataSet1.proveedores);

            this.reportViewer1.RefreshReport();
        }
    }
}
