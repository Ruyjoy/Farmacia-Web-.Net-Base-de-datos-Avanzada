using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaEncargado
    {
        void Alta(Encargado login, Encargado encargado);
        Encargado Consulta(Encargado login, int cedula);
        void CambioPass(Encargado encargado, string passNuevo);
        Encargado Login(string pNombreUsuario, string pass);
    }
}
