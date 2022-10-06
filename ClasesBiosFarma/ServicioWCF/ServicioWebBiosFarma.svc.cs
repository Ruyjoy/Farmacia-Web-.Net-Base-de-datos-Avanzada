using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntidadesCompartidas;
using Logica;
using System.Xml;
using System.Web.Services.Protocols;




namespace ServicioWCF
{
    public class ServicioWebBiosFarma : IServicioWebBiosFarma
    {

        #region Usuario
        public void AltaU(Encargado login, Usuario pUsuario)
        {
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                logicaUsuario.Alta(login, pUsuario);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void BajaU(Encargado login, Usuario pUsuario)
        {
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                logicaUsuario.Baja(login, pUsuario);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void ModU(Encargado login, Usuario pUsuario)
        {
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                logicaUsuario.Mod(login, pUsuario);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public Usuario Login(string nombreUsuario, string pass)
        {
            Usuario usu = null;
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                usu = logicaUsuario.Login(nombreUsuario, pass);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return usu;
        }

        public Usuario ConsultaU(Encargado login, int pCedula)
        {
            Usuario usu = null;
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                usu = logicaUsuario.Consulta(login, pCedula);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return usu;
        }

        public void CambioPass(Usuario pUsuario, string passNuevo)
        {
            try
            {
                ILogicaUsuario logicaUsuario = FabricaLogica.getLogicaUsuario();
                logicaUsuario.CambioPass(pUsuario, passNuevo);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }
        #endregion

        public void AltaF(Encargado login, Farmaceutica farma)
        {
            try
            {
                ILogicaFarma logFarma = FabricaLogica.getLogicaFarma();
                logFarma.Alta(login, farma);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void BajaF(Encargado login, Farmaceutica farma)
        {
            try
            {
                ILogicaFarma logFarma = FabricaLogica.getLogicaFarma();
                logFarma.Baja(login, farma);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void ModificarF(Encargado login, Farmaceutica farma)
        {
            try
            {
                ILogicaFarma logFarma = FabricaLogica.getLogicaFarma();
                logFarma.Modificar(login, farma);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public Farmaceutica ConsultaF(Encargado login, string nombre)
        {
            Farmaceutica farma = null;
            try
            {
                ILogicaFarma logFarma = FabricaLogica.getLogicaFarma();
                farma = logFarma.Consulta(login, nombre);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return farma;
        }

        public Farmaceutica ConsultaTodasF(Encargado login, string nombre)
        {
            Farmaceutica farma = null;
            try
            {
                ILogicaFarma logFarma = FabricaLogica.getLogicaFarma();
                farma = logFarma.ConsultaTodas(login, nombre);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return farma;
        }

        public void AltaM(Encargado login, Medicamento med)
        {
            try
            {
                ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
                logMed.Alta(login, med);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void BajaM(Encargado login, Medicamento med)
        {
            try
            {
                ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
                logMed.Baja(login, med);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public void ModificarM(Encargado login, Medicamento med)
        {
            try
            {
                ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
                logMed.Modificar(login, med);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public Medicamento ConsultaM(Encargado login, string codigo, string nombreFarma)
        {
            Medicamento med = null;
            try
            {
                ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
                med = logMed.Consulta(login, codigo, nombreFarma);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return med;
        }


        public XmlElement ListaMedXml()
        {
            ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
            
            XmlDocument retorno = new XmlDocument();
            XmlNode _Documento = retorno.CreateNode(XmlNodeType.Element, "Medicamentos", "");
            
            try
            {

                foreach (Medicamento unM in logMed.ListaActivos())
                {
                    XmlNode nodoFarma = retorno.CreateNode(XmlNodeType.Element, "Farmaceutica", "");
                    XmlNode nodoMedicamento = retorno.CreateNode(XmlNodeType.Element, "Medicamento", "");

                    XmlElement nombreFarma = retorno.CreateElement("Nombre");
                    nombreFarma.InnerText = unM.Farma.Nombre;
                    XmlElement direccionFarma = retorno.CreateElement("Direccion");
                    direccionFarma.InnerText = unM.Farma.DireccionFiscal;
                    XmlElement emailFarma = retorno.CreateElement("Email");
                    emailFarma.InnerText = unM.Farma.Email;
                    XmlElement telefonoFarma = retorno.CreateElement("Telefono");
                    telefonoFarma.InnerText = unM.Farma.Telefono;
                    nodoFarma.AppendChild(nombreFarma);
                    nodoFarma.AppendChild(direccionFarma);
                    nodoFarma.AppendChild(emailFarma);
                    nodoFarma.AppendChild(telefonoFarma);

                    XmlElement codigoMed = retorno.CreateElement("Codigo");
                    codigoMed.InnerText = unM.Codigo;
                    XmlElement nombreMed = retorno.CreateElement("Nombre");
                    nombreMed.InnerText = unM.Nombre;
                    XmlElement tipoMed = retorno.CreateElement("Tipo");
                    tipoMed.InnerText = unM.Tipo;
                    XmlElement descMed = retorno.CreateElement("Descripcion");
                    descMed.InnerText = unM.Descripcion;
                    XmlElement precioMed = retorno.CreateElement("Precio");
                    precioMed.InnerText = unM.Precio.ToString();
                    XmlElement stockMed = retorno.CreateElement("Stock");
                    stockMed.InnerText = unM.Stock.ToString();
                    nodoMedicamento.AppendChild(nodoFarma);
                    nodoMedicamento.AppendChild(codigoMed);
                    nodoMedicamento.AppendChild(nombreMed);
                    nodoMedicamento.AppendChild(tipoMed);
                    nodoMedicamento.AppendChild(descMed);
                    nodoMedicamento.AppendChild(precioMed);
                    nodoMedicamento.AppendChild(stockMed);
                    _Documento.AppendChild(nodoMedicamento);
                }               
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }

            retorno.AppendChild(_Documento);
            XmlElement doc = retorno.DocumentElement;
            return doc;
        }

        public void AltaP(Pedido pedido)
        {
            ILogicaPedido logPed = FabricaLogica.getLogicaPedido();
            try
            {
                logPed.Alta(pedido);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public Pedido ConsultaP(int pNumPedido)
        {
            ILogicaPedido logPed = FabricaLogica.getLogicaPedido();
            Pedido pedido = null;
            try
            {
                pedido = logPed.Consulta(pNumPedido);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return pedido;
        }

        public void CambioEstado(Encargado login, Pedido pedido)
        {
            ILogicaPedido logPed = FabricaLogica.getLogicaPedido();
            
            try
            {
                logPed.CambioEstado(login, pedido);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }

        public List<Pedido> ListarP(Encargado login)
        {
            ILogicaPedido logPed = FabricaLogica.getLogicaPedido();
            List<Pedido> lista = null;
            try
            {
                lista = logPed.Listar(login);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return lista;
        }

        #region Excepcion Soap
        private void GeneroExcepcion(Exception ex)
        {           
            FaultException _MiEx = new FaultException("Error WS: "+ex.Message);
            throw _MiEx;
        }
        #endregion


        public void AgregarHorasExtras(HorasExtras pHorasExtras)
        {
            ILogicaHorasExtras logExtras = FabricaLogica.getLogicaExtras();
            
            try
            {
                logExtras.AgregarHorasExtras(pHorasExtras);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
        }


        public List<Medicamento> ListaMedStock(Empleado login)
        {
            ILogicaMedicamento logMed = FabricaLogica.getLogicaMedicamento();
            List<Medicamento> lista = null;
            try
            {
                lista = logMed.ListaActivos(login);
            }
            catch (Exception ex)
            {
                this.GeneroExcepcion(ex);
            }
            return lista;
        }
    }
}
