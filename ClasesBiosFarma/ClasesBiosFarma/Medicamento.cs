using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Medicamento
    {
        private string codigo;
        private int stock;
        private string nombre;
        private Farmaceutica farmaceutica;
        private string tipo;
        private string descripcion;
        private double precio;

        #region Propiedades

        [DataMember]
        public Farmaceutica Farma
        {
            get { return farmaceutica; }
            set { farmaceutica = value; }
        }

        [DataMember]
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [DataMember]
        public int Stock
        {
            get { return stock; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Stock no puede ser negativo.");
                }
                if (value > 10000)
                {
                    throw new Exception("Stock no puede superar las 10000 unidades.");
                }
                stock = value;
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (!value.Equals("Cardiologico") && !value.Equals("Diabeticos") && !value.Equals("Otros"))
                    throw new Exception("El tipo de medicamento debe ser 'Cardiologico', 'Diabeticos' u 'Otros'.");
                tipo = value;
            }
        }

        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        [DataMember]
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        #endregion

        #region Constructores

        public Medicamento() { }

        public Medicamento(string pCodigo, int pStock, string pTipo, string pNombre, Farmaceutica pFarmaceutica, string pDescripcion, double pPrecio)
        {
            Codigo = pCodigo;
            Stock = pStock;
            Nombre = pNombre;
            Farma = pFarmaceutica;
            Descripcion = pDescripcion;
            Tipo = pTipo;
            Precio = pPrecio;
        }

        #endregion
    }
}
