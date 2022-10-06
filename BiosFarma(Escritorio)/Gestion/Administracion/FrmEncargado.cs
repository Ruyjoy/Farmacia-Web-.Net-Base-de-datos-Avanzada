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
    public partial class FrmEncargado : Form
    {
        private Usuario _uNempleado;
        private Encargado _EmpLogueado;

        public FrmEncargado(Encargado pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
            this.ActivoPorDefecto();
        }

        private void ActivoPorDefecto()
        {
            //verifico botones activos
            BtnAlta.Enabled = false;


            txtCedula.Text = "";
            txtCedula.Enabled = true;
            txtNombre.Text = "";
            txtNombre.Enabled = false;
            txtTelefono.Enabled = false;
            txtTelefono.Text = "";
            txtLogueo.Enabled = false;
            txtLogueo.Text = "";
            txtRepetir.Enabled = false;
            txtRepetir.Text = "";
            txtPass.Text = "";
            txtCedula.Focus();
        }

        private void ActivoAgregar()
        {
            //verifico botones activos
            BtnAlta.Enabled = true;
            txtLogueo.Enabled = true;
            txtRepetir.Enabled = true;
            txtCedula.Enabled = false;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtPass.Enabled = true;
            txtNombre.Focus();
        }

        private void txtCedula_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int Cedula = 0;

                //busco si existe
                if (txtCedula.Text.Trim().Length != 0)
                {
                    Cedula = Convert.ToInt32(txtCedula.Text);
                }
                else
                {
                    mostrarMensajeError("Ingrese Una Cedula");
                    this.ActivoPorDefecto();
                    return;
                }
                
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _uNempleado = _una.ConsultaU(_EmpLogueado, Cedula);

                //verifico accion en funcion de si se encuentra o no
                if (_uNempleado == null)
                {
                    this.ActivoAgregar();
                    lblError.Text = "No ha encontrado un Encargado, desea agregarlo.";
                }
                else if (_uNempleado is Encargado)
                {
                   
                    txtCedula.Enabled = true;
                    txtRepetir.Enabled = false;
                    txtNombre.Enabled = false;
                    txtLogueo.Enabled = false;
                    BtnAlta.Enabled = false;
                    txtLogueo.Enabled = false;
                    txtRepetir.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCedula.Focus();

                    lblError.Text = "la cédula ya está ingresada en el sistema.";
                }
                else 
                {
                    lblError.Text = "El usuario encontrado no es un encargado Es un empleado.";
                    this.ActivoPorDefecto();

                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                lblError.Text = ex.Detail.InnerText;
            }
            catch (FormatException)
            {
                mostrarMensajeError("La cedula no es válida.");
                txtCedula.Focus();
                return;
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

        private void BtnAlta_Click(object sender, EventArgs e)
        {
            int cedula = 0;
            string nombre = "";
            string nombrelogueo = "";
            string contraseña = "";
            string telefono = "";

            if (txtCedula.Text.Trim().Length != 0)
            {
                cedula = Convert.ToInt32(txtCedula.Text);
            }
            else
            {
                mostrarMensajeError("Ingrese una Cedula.");
                return;
            }
            if (txtNombre.Text.Trim().Length != 0)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese el nombre.");
                return;
            }
            if (txtLogueo.Text.Trim().Length != 0)
            {
                nombrelogueo = txtLogueo.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un nombre de logueo.");
                return;
            }
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
                    mostrarMensajeError("Debe repetir correctamente la contraseña.");
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
                Encargado emp = new Encargado();
                emp.Cedula = cedula;
                emp.NombreCompleto = nombre;
                emp.Telefono = telefono;
                emp.NombreUsuario = nombrelogueo;
                emp.Pass = contraseña;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _una.AltaU(_EmpLogueado, emp);
                lblError.Text = "El Encargado fue agregado con éxito";
                this.ActivoPorDefecto();

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
                 if (ex.Message.Length > 51)
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

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.ActivoPorDefecto();
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
