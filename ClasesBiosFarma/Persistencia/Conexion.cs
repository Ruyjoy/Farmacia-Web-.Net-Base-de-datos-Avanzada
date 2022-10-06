using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class Conexion
    {
        public static string getConectionU(Usuario u)
        {
            return "Data Source=.; Initial Catalog = BiosFarma; user id = " + u.NombreUsuario + "; password = " + u.Pass + ";";
        }    
  
        public static string getConection()
        {
            return "Data Source=.; Initial Catalog = BiosFarma; user id = BiosFarma; password =  BiosFarma";
        }
    }
}
