using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class LineaPedido
    {
        private Medicamento medicamento;
        private int cantidad;

        #region Propiedades

        [DataMember]
        public Medicamento Medicamento
        {
            get { return medicamento; }
            set {
                if (value == null)
                    throw new Exception("Medicamento no puede ser nulo.");
                medicamento = value; 
            }
        }

        [DataMember]
        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("La cantidad debe ser mayor a cero.");
                }
                cantidad = value;
            }
        }
        
        #endregion

        #region Constructores

        public LineaPedido() { }

        public LineaPedido(Medicamento pMedicamento, int pCantidad)
        {
            Medicamento = pMedicamento;
            Cantidad = pCantidad;
        }

        #endregion
    }
}
