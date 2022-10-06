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
    public partial class FrmFarmaceutica : Form
    {
        private Farmaceutica _uNaFarmaceutica;
        private Encargado _EmpLogueado;

        public FrmFarmaceutica(Encargado pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
            this.ActivoPorDefecto();
        }

        private void ActivoPorDefecto()
        {
            //verifico botones activos
            BtnAlta.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

           
            txtNombre.Text = "";
            txtNombre.Enabled = true;
            txtDireccion.Enabled = false;
            txtDireccion.Text = "";
            txtTelefono.Enabled = false;
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtNombre.Focus();
        }

        private void ActivoAgregar()
        {
            //verifico botones activos
            BtnAlta.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            txtDireccion.Enabled = false;
            txtDireccion.Enabled = true;
            txtTelefono.Focus();
        }

        private void BtnAlta_Click(object sender, EventArgs e)
        {

            
            string nombre = "";
            string direccion= "";
            string telefono = "";
            string correo = "";

            if (txtNombre.Text.Trim().Length != 0)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese el nombre.");
                return;
            }
            if (txtTelefono.Text.Trim().Length != 0)
            {
                telefono = txtTelefono.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un telefono.");
                return;
            }
            if (txtDireccion.Text.Trim().Length != 0)
            {
                direccion = txtDireccion.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese una direccion.");
                return;
            }
            if (txtCorreo.Text.Trim().Length != 0)
            {

                correo = txtCorreo.Text;
               
            }
            else
            {
                mostrarMensajeError("Debe ingresar un correo.");
                return;
            }

            try
            {

                Farmaceutica farm = new Farmaceutica();
                farm.Nombre = nombre;
                farm.Telefono = telefono;
                farm.DireccionFiscal = direccion;
                farm.Email = correo;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _una.AltaF(_EmpLogueado, farm);
                lblError.Text = "La farmaceutica fue agregado con éxito";
                this.ActivoPorDefecto();

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 70)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 70);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
                this.ActivoPorDefecto();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                _una.BajaF(_EmpLogueado, _uNaFarmaceutica);
                lblError.Text = "La Farmaceutica fue eliminado con éxito";
                this.ActivoPorDefecto();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 70)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 70);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
                this.ActivoPorDefecto();
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

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.ActivoPorDefecto();
            lblError.Text = "";

        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                string nombre = "";

                //busco si existe
                if (txtNombre.Text.Trim().Length != 0)
                {
                    nombre = txtNombre.Text;

                }
                else
                {
                    mostrarMensajeError("Ingrese un nombre");
                    this.ActivoPorDefecto();
                    return;
                }

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _uNaFarmaceutica = _una.ConsultaF(_EmpLogueado, nombre);
               

                //verifico accion en funcion de si se encuentra o no
                if (_uNaFarmaceutica == null)
                {
                    this.ActivoAgregar();
                    txtNombre.Enabled = false;
                    lblError.Text = "No ha encontrado la Farmaceutica, desea agregarlo.";
                }
                else
                {
                    txtNombre.Text = _uNaFarmaceutica.Nombre;
                    txtTelefono.Text = _uNaFarmaceutica.Telefono;
                    txtDireccion.Text = _uNaFarmaceutica.DireccionFiscal;
                    txtCorreo.Text = _uNaFarmaceutica.Email;
                    txtNombre.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    txtTelefono.Enabled = true;
                    BtnAlta.Enabled = false;
                    txtDireccion.Enabled = true;
                    txtCorreo.Enabled = true;

                    lblError.Text = "Se ha encontrado su usuario.";
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 70)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 70);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
                this.ActivoPorDefecto();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string nombre = "";
            string direccion = "";
            string telefono = "";
            string correo = "";

            if (txtNombre.Text.Trim().Length != 0)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese el nombre.");
                return;
            }
            if (txtTelefono.Text.Trim().Length != 0)
            {
                telefono = txtTelefono.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un telefono.");
                return;
            }
            if (txtDireccion.Text.Trim().Length != 0)
            {
                direccion = txtDireccion.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese una direccion.");
                return;
            }
            if (txtCorreo.Text.Trim().Length != 0)
            {

                correo = txtCorreo.Text;

            }
            else
            {
                mostrarMensajeError("Debe ingresar un correo.");
                return;
            }

            try
            {

                Farmaceutica farm = new Farmaceutica();
                farm.Nombre = nombre;
                farm.Telefono = telefono;
                farm.DireccionFiscal = direccion;
                farm.Email = correo;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _una.ModificarF(_EmpLogueado, farm);
                lblError.Text = "La farmaceutica fue Modificada con éxito";
                this.ActivoPorDefecto();

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 70)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 70);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
                this.ActivoPorDefecto();
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
    }
}
