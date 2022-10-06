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
    public partial class FrmPrincipalEncargado : Form
    {
        private Usuario _EmpLogueado;


        public FrmPrincipalEncargado(Usuario pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmABMEmpleado((Encargado)_EmpLogueado)).ShowDialog();
        }

        private void aBMCompaniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmCambiodePassEncargado(_EmpLogueado)).ShowDialog();
        }

        private void aBMFarmaceuticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmFarmaceutica((Encargado)_EmpLogueado)).ShowDialog();
        }

        private void aBMMedicamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmABMMedicamento((Encargado)_EmpLogueado)).ShowDialog();
        }

        private void cambioDePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmCambiodePassEncargado(_EmpLogueado)).ShowDialog();
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Pedidos.FrmCambioPedido((Encargado)_EmpLogueado)).ShowDialog();
        }

        private void altaEncargadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.FrmEncargado((Encargado)_EmpLogueado)).ShowDialog();
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Administracion.ListadoPedidos((Encargado)_EmpLogueado)).ShowDialog();
        }
    }
}
