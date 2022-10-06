using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Encargado:Usuario
    {
        private string telefono;

        #region Propiedades

        [DataMember]
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        #endregion

        #region Constructores

        public Encargado() : base() { }

        public Encargado(string pTelefono, int pCedula, string pNombreUsuario, string pNombreCompleto, string pPass) :
            base(pCedula, pNombreUsuario, pNombreCompleto, pPass)
        {
            Telefono = pTelefono;
        }

        #endregion
    }
}
