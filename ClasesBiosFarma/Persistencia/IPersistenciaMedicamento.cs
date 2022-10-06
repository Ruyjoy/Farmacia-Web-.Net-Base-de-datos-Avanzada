using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaMedicamento
    {
        void Alta(Encargado login, Medicamento med);
        void Baja(Encargado login, Medicamento med);
        void Modificar(Encargado login, Medicamento med);
        Medicamento Consulta(Encargado login, string codigo, string nombreFarma);
        List<Medicamento> ListarActivos();
        List<Medicamento> ListarActivos(Empleado login);
    }
}
