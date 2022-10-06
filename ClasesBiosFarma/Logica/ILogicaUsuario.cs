using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaUsuario
    {
        void Alta(Encargado login, Usuario pUsuario);
        void Baja(Encargado login, Usuario pUsuario);
        void Mod(Encargado login, Usuario pUsuario);
        Usuario Login(string nombreUsuario, string pass);
        void CambioPass(Usuario pUsuario, string passNuevo);
        Usuario Consulta(Encargado login, int pCedula);
    }
}
