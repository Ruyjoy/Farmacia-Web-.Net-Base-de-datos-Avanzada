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
    public partial class FrmABMMedicamento : Form
    {
        private Encargado _EmpLogueado;
        private Medicamento _uNmedicamento;
        private Farmaceutica _uNaFarmaceutica;

        public FrmABMMedicamento(Encargado pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
            this.ActivoPorDefecto();
            ddlDesc.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ActivoPorDefecto()
        {
            //verifico botones activos
            BtnAlta.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            txtFarma.Enabled = true;
            txtCodigo.Text = "";
            txtCodigo.Enabled = true;
            txtNombre.Text = "";
            txtNombre.Enabled = true;
            txtDesc.Text = "";
            txtDesc.Enabled = false;
            txtStock.Text = "";
            txtPrecio.Enabled = false;
            txtPrecio.Text = "";
            txtStock.Enabled = false;
            ddlDesc.Enabled = false;
            txtFarma.Text = "";
            ddlDesc.SelectedIndex = -1;
            txtFarma.Focus();
        }

        private void ActivoAgregar()
        {
            //verifico botones activos
            BtnAlta.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtNombre.Enabled = true;
            txtStock.Enabled = true;
            txtPrecio.Enabled = true;
            txtDesc.Enabled = true;
            ddlDesc.Enabled = true;
            txtNombre.Focus();
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                string codigo = "";
                string nombreFarm = "";
               
                if (txtCodigo.Text.Trim().Length != 0)
                {
                    codigo = txtCodigo.Text;
                }
                else
                {
                    mostrarMensajeError("Ingrese Una codigo");
                    this.ActivoPorDefecto();
                    txtFarma.Enabled = false;
                    return;

                }
                nombreFarm = _uNaFarmaceutica.Nombre;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _uNmedicamento = _una.ConsultaM(_EmpLogueado, codigo, nombreFarm);

                if (_uNmedicamento == null)
                {
                    this.ActivoAgregar();
                    lblError.Text = "No ha encontrado el medicamento, desea agregarlo.";
                    txtCodigo.Enabled = false;
                }
                else
                {
                    txtNombre.Text = _uNmedicamento.Nombre;
                    txtStock.Text = Convert.ToString(_uNmedicamento.Stock);
                    txtDesc.Text = _uNmedicamento.Descripcion;
                    txtPrecio.Text = Convert.ToString(_uNmedicamento.Precio);
                    ddlDesc.Text = _uNmedicamento.Tipo.ToString();
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    BtnAlta.Enabled = false;
                    txtCodigo.Enabled = false;
                    txtFarma.Enabled = false;
                    txtNombre.Enabled = false;
                    txtDesc.Enabled = true;
                    ddlDesc.Enabled = true;
                    txtPrecio.Enabled = true;
                    txtStock.Enabled = true;



                    lblError.Text = "Se ha encontrado su usuario.";
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
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

        private void BtnAlta_Click(object sender, EventArgs e)
        {

            try
            {

            string codigo = "";
            string nombre = "";
            string descripcion = "";
            int stock = 0;
            int precio = 0;

            if (txtCodigo.Text.Trim().Length != 0)
            {
                codigo = txtCodigo.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese un codigo.");
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
            if (txtDesc.Text.Trim().Length != 0)
            {
                descripcion = txtDesc.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese una descripción.");
                return;
            }
            if (txtStock.Text.Trim().Length != 0)
            {
                stock = Convert.ToInt32(txtStock.Text);
            }
            else
            {
                mostrarMensajeError("Ingrese un stock.");
                return;
            }
            if (txtPrecio.Text.Trim().Length != 0)
            {
                precio = Convert.ToInt32(txtPrecio.Text);

            }
            else
            {
                mostrarMensajeError("ingrese un precio.");
                return;
            }
            if (ddlDesc.SelectedIndex == -1)
            {
                mostrarMensajeError("Debe de seleccionar un tipo.");
                return;
            }
            

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                Medicamento med = new Medicamento();
                med.Codigo = codigo;
                med.Farma= _uNaFarmaceutica;
                med.Nombre = nombre;
                med.Tipo = ddlDesc.SelectedItem.ToString(); ;
                med.Stock = stock;
                med.Descripcion = descripcion;
                med.Precio = precio;
                
                _una.AltaM(_EmpLogueado, med);
                lblError.Text = "El medicamento fue agregado con éxito";
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
                //this.ActivoPorDefecto();
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos, contacte al administrador del sistema");
                    //this.ActivoPorDefecto();
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {


            string nombre = "";
            string descripcion = "";
            int stock = 0;
            int precio = 0;

            if (txtNombre.Text.Trim().Length != 0)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese el nombre.");
                return;
            }
            if (txtDesc.Text.Trim().Length != 0)
            {
                descripcion = txtDesc.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese una descripción.");
                return;
            }
            if (txtStock.Text.Trim().Length != 0)
            {
                stock = Convert.ToInt32(txtStock.Text);
            }
            else
            {
                mostrarMensajeError("Ingrese un stock.");
                return;
            }
            if (txtPrecio.Text.Trim().Length != 0)
            {
                precio = Convert.ToInt32(txtPrecio.Text);

            }
            else
            {
                mostrarMensajeError("ingrese un precio.");
                return;
            }
            if (ddlDesc.SelectedIndex == -1)
            {
                throw new Exception("Debe de seleccionar un tipo.");
            }
            try
            {

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                Medicamento med = new Medicamento();
                med.Codigo = _uNmedicamento.Codigo;
                med.Farma = _uNaFarmaceutica;
                med.Nombre = nombre;
                med.Tipo = ddlDesc.SelectedItem.ToString(); ;
                med.Stock = stock;
                med.Descripcion = descripcion;
                med.Precio = precio;

                _una.ModificarM(_EmpLogueado, med);
                lblError.Text = "El medicamento fue modificado con éxito";
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
                //this.ActivoPorDefecto();
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos, contacte al administrador del sistema");
                    //this.ActivoPorDefecto();
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

                _una.BajaM(_EmpLogueado, _uNmedicamento);
                lblError.Text = "El medicamento fue eliminado con éxito";
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

        private void txtFarma_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string nombre = "";

                //busco si existe
                if (txtFarma.Text.Trim().Length != 0)
                {
                    nombre = txtFarma.Text;

                }
                else
                {
                    mostrarMensajeError("Ingrese un Farmaceutica");
                    this.ActivoPorDefecto();
                    return;
                }

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                _uNaFarmaceutica = _una.ConsultaTodasF(_EmpLogueado, nombre);

                if (_uNaFarmaceutica == null)
                {
                    lblError.Text = "No ha encontrado la Farmaceutica.";
                    this.ActivoPorDefecto();

                }
                else
                {
                    txtFarma.Text = _uNaFarmaceutica.Nombre;
                    txtFarma.Enabled = false;
                    txtCodigo.Enabled = true;
                    lblError.Text = "Se ha encontrado la Farmaceutica.";
                    txtCodigo.Focus();

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

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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
