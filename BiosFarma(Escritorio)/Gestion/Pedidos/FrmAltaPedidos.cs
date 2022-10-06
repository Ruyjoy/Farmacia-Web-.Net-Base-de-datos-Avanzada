using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gestion.ServicioWeb;

namespace Gestion.Pedidos
{
    public partial class FrmAltaPedidos : Form
    {
        private Usuario _EmpLogueado;
        private List<Medicamento> med= new List<Medicamento>();
        LineaPedido[] lista ;

        public FrmAltaPedidos(Usuario gemp)
        {
            InitializeComponent();
            _EmpLogueado = gemp;
            this.cargoDropDown();
            this.ActivoPorDefecto();
        }

        private void ActivoPorDefecto()
        {
            //verifico botones activos
            BtnAlta.Enabled = false;
          

            txtCantidad.Text = "";
            txtCantidad.Enabled = true;
            txtDireccion.Text = "";

            ddlMedicamento.SelectedIndex = -1;
            ddlFarma.SelectedIndex = -1;
            this.Gvlista.Rows.Clear();
            lblError.Text = "";
            txtDireccion.Focus();

        }

        protected void cargoDropDown()
        {
            try
            {

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                foreach (Medicamento m in _una.ListaMedStock((Empleado)_EmpLogueado))
                {

                    med.Add(m);
                    ddlFarma.Items.Add(m.Farma.Nombre.ToString());
                }

                for (int i = 0; i < ddlFarma.Items.Count; i++)
                {
                    for (int y = 0; y < ddlFarma.Items.Count; y++)
                    {
                        if (y != i && ddlFarma.Items[i].ToString() == ddlFarma.Items[y].ToString())
                        {
                            ddlFarma.Items.RemoveAt(i);
                            break;
                        }
                    }
                    ddlMedicamento.DropDownStyle = ComboBoxStyle.DropDownList;
                    ddlFarma.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio");
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    lblError.Text = "Error cargando Medicamentos. " + ex.Message;
            }

        }

        protected void mostrarMensajeError(string mensajeError)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "¡ERROR! " + mensajeError;
        }

        private void BtnAlta_Click(object sender, EventArgs e)
        {
            string direccion = "";


            if (txtDireccion.Text.Trim().Length != 0)
            {
                direccion = txtDireccion.Text;
            }
            else
            {
                mostrarMensajeError("Ingrese una direccion.");
                return;
            }
            if( Gvlista.Rows.Count == 0 )
            {
                MessageBox.Show("Debe seleccionar un medicamento y una cantidad para ingresar en el pedido");
                return;

            }

            try
            {
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                lista = new LineaPedido[Gvlista.Rows.Count];

                for (int j = 0; j < Gvlista.RowCount; j++)
                {
                    int indice = 0;
                    while (indice < med.Count)
                    {
                        if (med[indice].Farma.Nombre == Gvlista.Rows[j].Cells[0].Value.ToString() && med[indice].Codigo == Gvlista.Rows[j].Cells[1].Value.ToString()) 
                        {
                            LineaPedido f = new LineaPedido();
                            f.Medicamento = med[indice];

                            if (med[indice].Stock < Convert.ToInt32(Gvlista.Rows[j].Cells[2].Value.ToString()))
                            {
                                MessageBox.Show("NO tiene stock suficinete para este medicamento" + " Farmaceutica: " + med[indice].Farma.Nombre + " Medicamento: " + med[indice].Codigo);
                                return;
                            }
                            else 
                            {
                                f.Cantidad = Convert.ToInt32(Gvlista.Rows[j].Cells[2].Value.ToString());
                            }
                            lista[j] = f;
                        } 
                        indice++;
                    }
   
                }

                DateTime Fecha = DateTime.Parse(dtFecha.Value.ToShortDateString());
                Pedido pe = new Pedido();
                pe.FechaRealizado = Fecha;
                pe.DireccionEntrega = direccion;
                pe.Estado = "Generado";
                pe.Empleado = (Empleado)_EmpLogueado;
                pe.DetallePedido = lista;


                _una.AltaP(pe);
                this.ActivoPorDefecto();
                lblError.Text = "El Pedido Fue agregado con éxito";

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
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                string linea = "";
   
                if (txtCantidad.Text.Trim().Length != 0)
                {
                    linea = txtCantidad.Text.Trim();
                }
                else
                {
                    mostrarMensajeError("Ingrese una Cantidad.");
                    return;
                }

                if (ddlFarma.SelectedIndex == -1)
                {
                    throw new Exception("Debe de seleccionar una Farmaceutica.");
                }
                if (ddlMedicamento.SelectedIndex == -1)
                {
                    throw new Exception("Debe de seleccionar un Medicamento.");
                }

                string farma = ddlFarma.Text;
                string medicamento = ddlMedicamento.Text;
                bool encuentro = false;

                if (linea.Length > 0)
                {
                    if (Gvlista.Rows.Count == 0)
                    {
                        Gvlista.Rows.Add(farma, medicamento, linea);
                        txtCantidad.Text = "";
                        BtnAlta.Enabled = true;
                    }
                    else
                    {
                        for (int j = 0; j < Gvlista.RowCount; j++)
                        {
                            for (int col = 0; col < Gvlista.Rows[j].Cells.Count; col++)
                            {
                                if (farma == (Gvlista.Rows[j].Cells[0].Value.ToString()) && medicamento == Gvlista.Rows[j].Cells[1].Value.ToString())
                                {
                                    MessageBox.Show("Ya tiene un medicamento con ese codigo");
                                    txtCantidad.Text = "";
                                    encuentro = true;
                                    return;
                                }
                            }
                        }
                        if (encuentro == false)
                        {
                            Gvlista.Rows.Add(farma, medicamento, linea);
                            txtCantidad.Text = "";
                            BtnAlta.Enabled = true;
                        }

                       
                    }       
                }
                else
                {
                    throw new Exception("Debe especificar cantidada.");
                }
            }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio");
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
                    ActivoPorDefecto();
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void ddlFarma_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlMedicamento.Items.Clear();
                foreach (Medicamento m in med)
                {
                    if (m.Farma.Nombre.Equals(ddlFarma.Text))
                    {
                        ddlMedicamento.Items.Add(m.Codigo.ToString());
                    }
                }
            
             }
            catch (System.ServiceModel.ProtocolException)
            {
                MessageBox.Show("Error En el Servicio");
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos");
                    ActivoPorDefecto();
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
        }

        private void Gvlista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogResult r = MessageBox.Show("Desea eliminar esta linea ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (r == DialogResult.Yes)
                {
                    Gvlista.Rows.RemoveAt(e.RowIndex);
                    if (Gvlista.Rows.Count == 0)
                    {
                        this.ActivoPorDefecto();
                        lblError.Text = "Debe introducir algun medicamento para poder pedir";
                    }

                }

            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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
