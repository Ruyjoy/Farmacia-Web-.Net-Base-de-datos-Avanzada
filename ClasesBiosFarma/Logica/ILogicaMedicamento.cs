using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaMedicamento
    {
        void Alta(Encargado login, Medicamento med);
        void Modificar(Encargado login, Medicamento med);
        void Baja(Encargado login, Medicamento med);
        Medicamento Consulta(Encargado login, string codigo, string nombreFarma);
        List<Medicamento> ListaActivos();
        List<Medicamento> ListaActivos(Empleado login);
    }
}
