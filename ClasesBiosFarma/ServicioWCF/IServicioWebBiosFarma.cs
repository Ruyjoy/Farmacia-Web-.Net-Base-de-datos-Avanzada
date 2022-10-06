using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntidadesCompartidas;
using System.Xml;

namespace ServicioWCF
{
    [ServiceContract]
    public interface IServicioWebBiosFarma
    {

        [OperationContract]
        void AltaU(Encargado login, Usuario pUsuario);
        [OperationContract]
        void BajaU(Encargado login, Usuario pUsuario);
        [OperationContract]
        void ModU(Encargado login, Usuario pUsuario);
        [OperationContract]
        Usuario Login(string nombreUsuario, string pass);
        [OperationContract]
        void CambioPass(Usuario pUsuario, string passNuevo);
        [OperationContract]
        Usuario ConsultaU(Encargado login, int pCedula);

        [OperationContract]
        void AltaF(Encargado login, Farmaceutica farma);
        [OperationContract]
        void BajaF(Encargado login, Farmaceutica farma);
        [OperationContract]
        void ModificarF(Encargado login, Farmaceutica farma);
        [OperationContract]
        Farmaceutica ConsultaF(Encargado login, string nombre);
        [OperationContract]
        Farmaceutica ConsultaTodasF(Encargado login, string nombre);

        [OperationContract]
        void AltaM(Encargado login, Medicamento med);
        [OperationContract]
        void BajaM(Encargado login, Medicamento med);
        [OperationContract]
        void ModificarM(Encargado login, Medicamento med);
        [OperationContract]
        Medicamento ConsultaM(Encargado login, string codigo, string nombreFarma);
        [OperationContract]
        XmlElement ListaMedXml();
        [OperationContract]
        List<Medicamento> ListaMedStock(Empleado login);

        [OperationContract]
        void AltaP(Pedido pedido);
        [OperationContract]
        Pedido ConsultaP(int numPedido);
        [OperationContract]
        void CambioEstado(Encargado login, Pedido pedido);
        [OperationContract]
        List<Pedido> ListarP(Encargado login);

        [OperationContract]
        void AgregarHorasExtras(HorasExtras pHorasExtras);
    }
}
