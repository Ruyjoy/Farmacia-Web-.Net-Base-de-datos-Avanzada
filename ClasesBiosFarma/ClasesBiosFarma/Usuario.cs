using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    [DataContract]
    [KnownType(typeof(Empleado))]
    [KnownType(typeof(Encargado))]
    public abstract class Usuario
    {
        private int cedula;
        private string nombreCompleto;
        private string nombreUsuario;
        private string pass;

        #region Propiedades

        [DataMember]
        public int Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        [DataMember]
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        [DataMember]
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }

        [DataMember]
        public string Pass
        {
            get { return pass; }
            set
            {
                Regex regexCarac = new Regex(@"([a-z][A-Z]*)");
                Regex regexNum = new Regex(@"([0-9])");
                if (value != null)
                {
                    MatchCollection matches = regexCarac.Matches(value.Substring(0, 5));
                    if (matches.Count == 5)
                    {
                        matches = regexNum.Matches(value.Substring(5, 2));
                        if (matches.Count != 2)
                            throw new Exception("El nuevo pass debe tener dos números al final.");
                    }
                    else
                        throw new Exception("El nuevo pass debe tener 5 letras al inicio.");

                    pass = value;
                }
            }
        }
        #endregion

        #region Constructores

        public Usuario() { }

        public Usuario(int pCedula, string pNombreUsuario, string pNombreCompleto, string pPass)
        {
            Cedula = pCedula;
            NombreUsuario = pNombreUsuario;
            NombreCompleto = pNombreCompleto;
            Pass = pPass;
        }

        #endregion

    }
}
