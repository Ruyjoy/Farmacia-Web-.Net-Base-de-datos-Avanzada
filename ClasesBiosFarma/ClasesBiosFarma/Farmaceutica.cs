using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Farmaceutica
    {
        private string nombre;
        private string telefono;
        private string direccionFiscal;
        private string email;

        #region Propiedades

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("El nombre no puede quedar vacío.");
                else if (value.Trim().Length > 20)
                    throw new Exception("El nombre no puede tener más de 20 carácteres.");

                nombre = value.Trim();
            }
        }

        [DataMember]
        public string Telefono
        {
            get { return telefono; }
            set
            {
                int tel;
                if (value.Trim().Length == 0)
                    throw new Exception("El teléfono no puede quedar vacío.");
                else if (value.Trim().Length > 10)
                    throw new Exception("El teléfono no puede tener más de 10 dígitos.");
                else if (!int.TryParse(value.Trim(), out tel))
                    throw new Exception("El teléfono debe ser numérico.");

                telefono = value.Trim();
            }
        }

        [DataMember]
        public string DireccionFiscal
        {
            get { return direccionFiscal; }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("La dirección no puede quedar vacía.");
                else if (value.Trim().Length > 100)
                    throw new Exception("La dirección no puede tener más de 100 carácteres.");

                direccionFiscal = value;
            }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("El e-mail no puede quedar vacío.");
                else if (value.Trim().Length > 40)
                    throw new Exception("El e-mail no puede tener más de 40 carácteres.");
                email = value;
            }
        }

        #endregion

        #region Constructores

        public Farmaceutica() { }

        public Farmaceutica(string pNombre, string pTelefono, string pDireccionFiscal, string pEmail)
        {
            Nombre = pNombre;
            Telefono = pTelefono;
            DireccionFiscal = pDireccionFiscal;
            Email = pEmail;
        }

        #endregion

    }
}
