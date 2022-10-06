using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaMedicamento:ILogicaMedicamento
    {
        private static LogicaMedicamento _instancia = null;

        private LogicaMedicamento() { }

        public static LogicaMedicamento GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaMedicamento();

            return _instancia;
        }

        public void Alta(Encargado login, Medicamento med)
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            per.Alta(login, med);
        }

        public void Modificar(Encargado login, Medicamento med)
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            per.Modificar(login, med);
        }

        public void Baja(Encargado login, Medicamento med)
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            per.Baja(login, med);
        }

        public Medicamento Consulta(Encargado login, string codigo, string nombreFarma)
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            return per.Consulta(login, codigo, nombreFarma);
        }

        public List<Medicamento> ListaActivos()
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            return per.ListarActivos();
        }


        public List<Medicamento> ListaActivos(Empleado login)
        {
            IPersistenciaMedicamento per = FabricaPersistencia.getPersistenciaMedicamento();
            return per.ListarActivos(login);
        }
    }
}
