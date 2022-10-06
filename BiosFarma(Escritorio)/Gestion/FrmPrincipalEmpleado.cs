using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gestion.ServicioWeb;

namespace Gestion
{
    public partial class FrmPrincipalEmpleado : Form
    {
        private Usuario _EmpLogueado;
        private string ruta="";

        public FrmPrincipalEmpleado(Usuario gemp, string rutaArchivoXml)
        {
            InitializeComponent();
            _EmpLogueado = gemp;
            ruta = rutaArchivoXml;
        }

        private void cambioDePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmCambiodePassEncargado(_EmpLogueado)).ShowDialog();
        }

        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Pedidos.FrmAltaPedidos(_EmpLogueado)).ShowDialog();
        }

        private void FrmPrincipalEmpleado_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
        }
    }
}
