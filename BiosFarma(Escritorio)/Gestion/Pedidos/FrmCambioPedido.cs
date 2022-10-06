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
    public partial class FrmCambioPedido : Form
    {

        private Encargado _EmpLogueado;
        private Pedido[] lista;
        private List<Pedido> ListaPedidos = new List<Pedido>();

        public FrmCambioPedido(Encargado pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp;
            this.Cargar();
        }

        private void Cargar()
        {
            try
            {
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                //lista = _una.ListarP(_EmpLogueado);
                var resultado = (from lista in _una.ListarP(_EmpLogueado)
                                 where (lista.Estado == "Generado" || lista.Estado == "Enviado")
                                 select new
                                 {
                                     Direccion = lista.DireccionEntrega,
                                     Empleado = lista.Empleado.NombreUsuario,
                                     Estado = lista.Estado,
                                     Fecha = lista.FechaRealizado,
                                     Numero = lista.Numero

                                 }
                                     ).ToList();
                
                /*
                foreach (Pedido pe in lista)
                {
                    if (!pe.Estado.Equals("Entregado"))
                    {
                        ListaPedidos.Add(pe);
                        Gvtodo.DataSource = ListaPedidos;
                    }
                }
                if (ListaPedidos.Count() <= 0) 
                {
                    lblError.Text = "No tine ningun elemento Generado o Enviado";
                    return;
                }
                 * */
                Gvtodo.DataSource = resultado;
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
                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void Gvtodo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                     
            if (e.RowIndex >= 0) 
             {
                DataGridViewRow row = this.Gvtodo.Rows[e.RowIndex];

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();

                DialogResult r = MessageBox.Show("Vas a cambiar el estado del pedido", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);


                if (r == DialogResult.Yes)
                {

                    string pNumero = row.Cells["Numero"].Value.ToString();
                    _una.ConsultaP(Convert.ToInt32(pNumero));
                    _una.CambioEstado(_EmpLogueado,_una.ConsultaP(Convert.ToInt32(pNumero)));

                    var resultado = (from lista in _una.ListarP(_EmpLogueado)
                                     where (lista.Estado == "Generado" || lista.Estado == "Enviado")
                                     select new 
                                     {
                                         Direccion = lista.DireccionEntrega,
                                         Empleado = lista.Empleado.NombreUsuario,
                                         Estado = lista.Estado,
                                         Fecha = lista.FechaRealizado,
                                         Numero = lista.Numero

                                     }
                                     ).ToList();

                    Gvtodo.DataSource = resultado;
                }            
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
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.FaultException)
                {
                    MessageBox.Show("No puede acceder la  base de datos, contacte al administrador del sistema");

                }
                else if (ex.Message.Length > 51)
                    MessageBox.Show(ex.Message.Substring(0, 51));
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gvtodo.DataSource = null;
            Gvtodo.Refresh();
        }
    }
}
