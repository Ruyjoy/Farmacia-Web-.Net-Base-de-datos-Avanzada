using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Gestion.ServicioWeb;
using System.Xml;

namespace Gestion.Login
{
    public partial class FrmLogin : Form
    {
        private Usuario empleado = null;
        private string rutaArchivoXml ="";
       
        public FrmLogin()
        {
            InitializeComponent();
            txtuser.Focus();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtuser.Text))
            {
                MessageBox.Show("Debe ingresar un Usuario.");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtpass.Text))
            {
                MessageBox.Show("Debe ingresar la contraseña.");
                return;
            }
            string contraseña = "";
            string usuario = "";

            try
            {
                usuario = txtuser.Text;
                contraseña = txtpass.Text;

                ServicioWeb.IServicioWebBiosFarma _una = new ServicioWeb.ServicioWebBiosFarmaClient();
                empleado = _una.Login(usuario, contraseña);
            }
            catch (System.Web.Services.Protocols.SoapHeaderException ex)
            {
                if (ex.Detail.InnerText.Length > 70)
                    MessageBox.Show(ex.Detail.InnerText.Substring(0, 70));
                else
                    MessageBox.Show(ex.Detail.InnerText);

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
            if (empleado is Encargado)
            {
                if (empleado != null && empleado.Pass == contraseña)
                {
                    this.Hide();

                    FrmPrincipalEncargado _unForm = new FrmPrincipalEncargado((Encargado)empleado);
                    _unForm.ShowDialog();
                    this.Close();
                }
            }
            if (empleado is Empleado) 
            {
                if (empleado != null && empleado.Pass == contraseña)
                {
                    this.xml((Empleado)empleado);


                    this.Hide();
                    FrmPrincipalEmpleado _unForm = new FrmPrincipalEmpleado((Empleado)empleado, rutaArchivoXml);
                    _unForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nombre de usuario y/o contraseña incorrecto/a(s).");
                }

            }
            else
            {
                MessageBox.Show("Nombre de usuario y/o contraseña incorrecto/a(s).");
            }
        }

        private void xml(Empleado emp) 
        {
            try  
            {
                rutaArchivoXml =Application.StartupPath + @"\ControlHoras\Xml\ControlHoras.xml";

                if (System.IO.File.Exists(rutaArchivoXml))
                {
                    System.IO.File.Delete(rutaArchivoXml);
                }

                DateTime horaInicio = DateTime.Today + emp.InicioTareas.TimeOfDay;
                DateTime horaFinal = new DateTime();
                if (emp.FinTareas.TimeOfDay < emp.InicioTareas.TimeOfDay)
                    horaFinal = DateTime.Today.AddDays(1) + emp.FinTareas.TimeOfDay;
                else
                    horaFinal = DateTime.Today + emp.FinTareas.TimeOfDay;

                XmlDocument _Documento = new XmlDocument();

                XmlNode _raiz = _Documento.CreateNode(XmlNodeType.Element, "HorasExtras", "");

                XmlElement _Cedula = _Documento.CreateElement("Cedula");
                _Cedula.InnerText = emp.Cedula.ToString();

                XmlElement _HoraInicio = _Documento.CreateElement("HoraInicio");
                _HoraInicio.InnerText = horaInicio.ToString("");

                XmlElement _HoraFin = _Documento.CreateElement("HoraFin");
                _HoraFin.InnerText = horaFinal.ToString("");

                XmlElement _Nodo = _Documento.CreateElement("HoraExtra");


                _Nodo.AppendChild(_Cedula);
                _Nodo.AppendChild(_HoraInicio);
                _Nodo.AppendChild(_HoraFin);

                _raiz.AppendChild(_Nodo);
                _Documento.AppendChild(_raiz);

                _Documento.Save(rutaArchivoXml);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
