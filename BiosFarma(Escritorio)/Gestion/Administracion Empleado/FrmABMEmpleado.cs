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
    public partial class FrmABMEmpleado : Form
    {
        private Usuario _uNempleado;
        private Encargado _EmpLogueado;

        public FrmABMEmpleado(Encargado pEmp)
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

            txtCedula.Text = "";
            txtCedula.Enabled = true;
            txtNombre.Text = "";
            txtContraseña.Enabled = false;
            txtContraseña.Text = "";
            txtRepetir.Enabled = false;
            txtRepetir.Text = "";
            txtnombrelogueo.Text = "";
            txtnombrelogueo.Enabled = false;
            dtInicio.Value = DateTime.Now.Date;
            dtFin.Value = DateTime.Now.Date;

            txtCedula.Focus();
        }

        private void ActivoAgregar()
        {
            //verifico botones activos
            BtnAlta.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtContraseña.Enabled = true;
            txtRepetir.Enabled = true;
            txtNombre.Enabled = true;
            txtnombrelogueo.Enabled = true;
            txtCedula.Enabled = false;
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
                    Convert.ToInt32(Cedula);
                }
                else
                {
                    mostrarMensajeError("Ingrese Una Cedula");
                    this.ActivoPorDefecto();
                    return;
                }

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _uNempleado = _una.ConsultaU(_EmpLogueado, Cedula);

                if(_uNempleado is Encargado)
                {
                    lblError.Text = "El usuario que esta buscarndo es un encargado .";
                    txtNombre.Enabled = false;
                    txtnombrelogueo.Enabled = false;
                    txtCedula.Focus();
                    return;
                }
                //verifico accion en funcion de si se encuentra o no
                if (_uNempleado == null)
                {
                    this.ActivoAgregar();
                    lblError.Text = "No ha encontrado un usuario, desea agregarlo.";
                }
                else
                {
                    txtNombre.Text = _uNempleado.NombreCompleto;
                    txtCedula.Text = Convert.ToString(_uNempleado.Cedula);
                    txtnombrelogueo.Text = _uNempleado.NombreUsuario;
                    txtContraseña.Text = _uNempleado.Pass;
                    dtFin.Value = ((Empleado)_uNempleado).FinTareas;
                    dtInicio.Value = ((Empleado)_uNempleado).InicioTareas;
                    txtnombrelogueo.Enabled = true;
                    txtCedula.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    txtNombre.Enabled = true;
                    txtContraseña.Enabled = false;
                    txtRepetir.Enabled = false;
                    BtnAlta.Enabled = false;

                    lblError.Text = "Se ha encontrado su usuario.";
                }

            }
            catch (FormatException)
            {
                mostrarMensajeError("La cedula no es válida.");
                txtCedula.Focus();
                return;
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
                MessageBox.Show("Error En el Servicio");
                this.ActivoPorDefecto();
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
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

        private void BtnAlta_Click(object sender, EventArgs e)
        {
            int cedula = 0;
            string nombre = "";
            string nombrelogueo = "";
            string contraseña = "";

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
            if (txtContraseña.Text.Trim().Length != 0)
            {
                contraseña = txtContraseña.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un nombre de logueo.");
                return;
            }
            if (txtnombrelogueo.Text.Trim().Length != 0)
            {
                nombrelogueo = txtnombrelogueo.Text;
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
                DateTime Fechainicio= DateTime.Parse(dtInicio.Value.ToShortTimeString());
                DateTime Fechafin = DateTime.Parse(dtFin.Value.ToShortTimeString());

                Empleado emp = new Empleado();
                emp.Cedula = cedula;
                emp.NombreCompleto = nombre;
                emp.NombreUsuario = nombrelogueo;
                emp.Pass = contraseña;
                emp.InicioTareas= Fechainicio;
                emp.FinTareas= Fechafin;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _una.AltaU(_EmpLogueado, emp);
                lblError.Text = "El empleado fue agregado con éxito";
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
                if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int cedula = 0;
            string nombre = "";
            string nombrelogueo = "";

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
            if (txtnombrelogueo.Text.Trim().Length != 0)
            {
                nombrelogueo = txtnombrelogueo.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un nombre de logueo.");
                return;
            }

            try
            {
                DateTime Fechainicio = DateTime.Parse(dtInicio.Value.ToShortDateString());
                DateTime Fechafin = DateTime.Parse(dtFin.Value.ToShortDateString());

                Empleado emp = new Empleado();
                emp.Cedula = cedula;
                emp.NombreCompleto = nombre;
                emp.NombreUsuario = nombrelogueo;
                emp.InicioTareas = Fechainicio;
                emp.FinTareas = Fechafin;
                emp.Pass = _uNempleado.Pass;
                

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _una.ModU(_EmpLogueado,emp);
                lblError.Text = "El empleado fue modificado con éxito";

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
                MessageBox.Show("Error En el Servicio");
                this.ActivoPorDefecto();
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                if (_EmpLogueado.Cedula == _uNempleado.Cedula)
                {
                    mostrarMensajeError("No se puede elimiar el empleado logeado.");
                    return;
                }
                _una.BajaU(_EmpLogueado,_uNempleado);
                lblError.Text = "El empleado fue eliminado con éxito";
                this.ActivoPorDefecto();
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio");
                this.ActivoPorDefecto();
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
                    this.ActivoPorDefecto();
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
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
