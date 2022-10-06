using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Empleado:Usuario
    {
        private DateTime inicioTareas;
        private DateTime finTareas;

        #region Propiedades

        [DataMember]
        public DateTime InicioTareas
        {
            get { return inicioTareas; }
            set { inicioTareas = value; }
        }

        [DataMember]
        public DateTime FinTareas
        {
            get { return finTareas; }
            set { finTareas = value; }
        }

        #endregion

        #region Constructores

        public Empleado() : base() { }

        public Empleado(DateTime pInicioTareas, DateTime pFinTareas, int pCedula, string pNombreUsuario, string pNombreCompleto, string pPass) :
            base(pCedula, pNombreUsuario, pNombreCompleto, pPass)
        {
            InicioTareas = pInicioTareas;
            FinTareas = pFinTareas;
        }
    
        #endregion
    }
}
