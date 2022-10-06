using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class HorasExtras
    {
        private Empleado empleado;
        private DateTime fecha;
        private int cantMin;

        #region Propiedades

        [DataMember]
        public Empleado Empleado
        {
            get { return empleado; }
            set
            {
                if (value == null)
                    throw new Exception("El empleado no puede ser nulo.");

                empleado = value;
            }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
            }
        }

        [DataMember]
        public int CantMinutos
        {
            get { return cantMin; }
            set
            {
                if (value <= 0)
                    throw new Exception("La cantidad de minutos tiene que ser mayor a cero.");

                cantMin = value;
                
            }
        }
        #endregion

        public HorasExtras() { }

        public HorasExtras(Empleado pEmpleado, DateTime pFecha, int pCantMin)
        {
            Empleado = pEmpleado;
            Fecha = pFecha;
            CantMinutos = pCantMin;
        }
    }
}
