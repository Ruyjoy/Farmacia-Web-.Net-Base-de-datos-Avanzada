using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gestion.ServicioWeb;

namespace Gestion.Administracion
{
    public partial class FrmCambiodePassEncargado : Form
    {
        private Usuario _EmpLogueado;

        public FrmCambiodePassEncargado(Usuario pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
            this.ActivoPorDefecto();
            lblnombre.Text = _EmpLogueado.NombreCompleto.ToString();
        }

        private void ActivoPorDefecto()
        {
            //verifico botones activos
            btnAceptar.Enabled = false;

            txtActual.Text = "";
            txtActual.Enabled = true;
            txtPass.Text = "";
            txtRepetir.Text = "";
            txtActual.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string contraseña = "";

            if (txtPass.Text.Trim().Length != 0)
            {
                contraseña = txtPass.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese la contraseña.");
                return;
            }
            if (txtRepetir.Text.Trim().Length != 0)
            {
                if (txtRepetir.Text != contraseña)
                {
                    mostrarMensajeError("Se debe repetir correctamente la contraseña.");
                    return;
                }
            }
            else
            {
                mostrarMensajeError("Debe repetir la contraseña.");
                return;
            }

            try
            {
                if (_EmpLogueado is Encargado)
                {

                    ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                    _una.CambioPass((Encargado)_EmpLogueado, contraseña);
                    this.ActivoPorDefecto();
                    lblError.Text = "La contraseña Fue cambiada con éxito";

                }
                if (_EmpLogueado is Empleado)
                {

                    ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                    _una.CambioPass((Empleado)_EmpLogueado, contraseña);
                    this.ActivoPorDefecto();
                    lblError.Text = "La contraseña Fue cambiada con éxito";
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                lblError.Text = ex.Detail.InnerText;
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos, contacte al administrador del sistema");
                    this.ActivoPorDefecto();
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        protected void mostrarMensajeError(string mensajeError)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "¡ERROR! " + mensajeError;
        }

        private void txtActual_Validating(object sender, CancelEventArgs e)
        {
            string pass = "";

            if (txtActual.Text.Trim().Length != 0)
            {
                pass = txtActual.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese contraseña actual.");
                this.ActivoPorDefecto();
                return;
            }

            if (_EmpLogueado.Pass == txtActual.Text)
            {
                txtActual.Enabled = false;
                txtPass.Enabled = true;
                txtRepetir.Enabled = true;
                btnAceptar.Enabled = true;
                lblError.Text = "Se puede cambiar la Contraseña";
            }
            else 
            {
                lblError.Text = " La Contraseña Es Incorrecta";
            }
        }

    }
}
