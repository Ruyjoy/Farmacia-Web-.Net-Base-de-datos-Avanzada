using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaEmpleado
    {
        void Alta(Encargado login, Empleado empleado);
        void Modificar(Encargado login, Empleado empleado);
        void Baja(Encargado login, Empleado empleado);
        Empleado Consulta(Encargado login, int cedula);
        void CambioPass(Empleado empleado, string passNuevo);
        Empleado Login(string nombreUsuario, string pass);
    }
}
