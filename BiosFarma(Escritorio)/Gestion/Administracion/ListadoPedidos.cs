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
    public partial class ListadoPedidos : Form
    {
        private Encargado _EmpLogueado;
        private Pedido[] Lista;

        public ListadoPedidos(Encargado pEmp)
        {
            InitializeComponent();
            _EmpLogueado = pEmp; 
            this.Cargar();
            this.cargoDropDown();
            
        }

        private void Cargar()
        {
            try
            {
                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                Lista = _una.ListarP(_EmpLogueado);
                int fechaInicial = DateTime.Now.Year; 

                try
                {
                    var resultado = (from list in Lista
                                     where Convert.ToDateTime(list.FechaRealizado).Year == fechaInicial
                                     select new
                                     {
                                         Numero = list.Numero,
                                         Fecha = list.FechaRealizado,
                                         Empleado = list.Empleado.NombreCompleto,
                                         Direccion = list.DireccionEntrega,
                                         Estado = list.Estado,

                                     }).ToList();

                    Gvtodo.DataSource = resultado;
                }
                catch (System.ServiceModel.ProtocolException)
                {
                    MessageBox.Show("Error En el Servicio, contacte al administrador del sistema");
                    return;
                }
                catch (Exception ex)
                {
                    if (ex is System.ServiceModel.FaultException)
                    {
                        MessageBox.Show("No puede acceder la  base de datos, contacte al administrador del sistema");
                        return;
                    }
                    else if (ex.Message.Length > 51)
                        MessageBox.Show(ex.Message.Substring(0, 51));
                    else
                        MessageBox.Show(ex.Message);
                }

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

        protected void cargoDropDown()
        {

            {

                if (Lista == null)
                {
                    lblError.Text = "NO tiene elementos en la lista. ";
                    return;
                }
                try
                {
                    foreach (Pedido m in Lista)
                    {
                        foreach (LineaPedido l in m.DetallePedido)
                        {
                            string nomFarma = l.Medicamento.Farma.Nombre;
                            bool encuentro = false;
                            foreach (object i in ddlFarma.Items)
                            {
                                if (i.ToString() == nomFarma)
                                {
                                    encuentro = true;
                                }
                            }
                            if (!encuentro)
                            {
                                ddlFarma.Items.Add(nomFarma);
                            }
                        }
                    }

                    ddlFarma.DropDownStyle = ComboBoxStyle.DropDownList;
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
        }

        protected void mostrarMensajeError(string mensajeError)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "¡ERROR! " + mensajeError;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void Filtro1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista == null)
                {
                    lblError.Text = " ";
                    return;
                }
                var resultado = (from list in Lista
                                 orderby list.Empleado.NombreCompleto
                                 group list by list.Empleado.Cedula into Group
                                 select new
                               {
                                   Nombre = Group.First().Empleado.NombreCompleto,
                                   Cantidad = Group.Count()
                               }).ToList();

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

        private void Filtro2_Click(object sender, EventArgs e)
        {
            try
            { 
               if (Lista == null)
                {
                    lblError.Text = "";
                    return;
                }

               var resultado = (from list in Lista
                                from med in list.DetallePedido
                                group med.Cantidad  by  med.Medicamento.Nombre  into Group
                                select new
                                {
                                    Nombre = Group.Key,
                                    Cantidad = Group.Count()
    
                                    

                                }).ToList();

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

        private void Filtro3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista == null)
                {
                    lblError.Text = "";
                    return;
                }

                Gvtodo.DataSource = null;
                Gvtodo.Refresh();

                var resultado = (from list in Lista
                                 from listFarma in list.DetallePedido
                                 where listFarma.Medicamento.Farma.Nombre == ddlFarma.Text
                                 group listFarma by new { listFarma.Medicamento.Codigo , list.FechaRealizado} into Grupo
                                 orderby Grupo.Key.FechaRealizado
                                 select new
                                 {
                                     Fecha =Grupo.Key.FechaRealizado,
                                     Nombre = Grupo.First().Medicamento.Nombre,
                                     //Cantidad = Grupo.Key.Codigo.Count()
                                     Cantidad = Grupo.Sum(x=>x.Cantidad)

                                 }).ToList();

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
    }
}
