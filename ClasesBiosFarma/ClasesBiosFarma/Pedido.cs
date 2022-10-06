using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Pedido
    {
        private int numero;
        private string estado;
        private string direccionEntrega;
        private DateTime fechaRealizado;
        private Empleado empleado;
        private List<LineaPedido> detallePedido;

        #region Propiedades

        [DataMember]
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember]
        public string Estado
        {
            get { return estado; }
            set
            {
                if (value != null)
                {
                    if (!value.Equals("Generado") && !value.Equals("Enviado") && !value.Equals("Entregado"))
                    {
                        throw new Exception("El estado del pedido puede ser, Generado, Enviado o Entregado.");
                    }
                }
                estado = value;
            }
        }

        [DataMember]
        public string DireccionEntrega
        {
            get { return direccionEntrega; }
            set { direccionEntrega = value; }
        }

        [DataMember]
        public DateTime FechaRealizado
        {
            get { return fechaRealizado; }
            set { fechaRealizado = value; }
        }

        [DataMember]
        public Empleado Empleado
        {
            get { return empleado; }
            set
            {
                if (value == null)
                {
                    throw new Exception("El empleado no puede ser nulo.");
                }
                empleado = value;
            }
        }

        [DataMember]
        public List<LineaPedido> DetallePedido
        {
            get { return detallePedido; }
            set { detallePedido = value; }
        }

        #endregion

        #region Constructores

        public Pedido() { }

        public Pedido(int pNumero, string pEstado, string pDireccionEntrega, DateTime pFechaRealizado, Empleado pEmpleado, List<LineaPedido> pDetallePedido)
        {
            Numero = pNumero;
            Estado = pEstado;
            DireccionEntrega = pDireccionEntrega;
            FechaRealizado = pFechaRealizado;
            Empleado = pEmpleado;
            DetallePedido = pDetallePedido;
        }

        #endregion

        #region Operaciones
       
        public void AgregarLineaPedido(LineaPedido lineaPedido)
        {
            bool existe = false;
            foreach (LineaPedido l in detallePedido)
            {
                
                if (l.Medicamento.Codigo == lineaPedido.Medicamento.Codigo && l.Medicamento.Farma.Nombre == lineaPedido.Medicamento.Farma.Nombre)
                {
                    l.Cantidad = l.Cantidad + lineaPedido.Cantidad;
                    existe = true;
                }              
            }
            if (!existe)
                detallePedido.Add(lineaPedido);
        }

        #endregion
    }
}
