using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaFarma:ILogicaFarma
    {
        private static LogicaFarma _instancia = null;

        private LogicaFarma() { }

        public static LogicaFarma GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaFarma();

            return _instancia;
        }

        public void Alta(Encargado login, Farmaceutica farma)
        {
            IPersistenciaFarma perFarma = FabricaPersistencia.getPersistenciaFarma();
            perFarma.Alta(login, farma);
        }

        public void Baja(Encargado login, Farmaceutica farma)
        {
            IPersistenciaFarma perFarma = FabricaPersistencia.getPersistenciaFarma();
            perFarma.Baja(login, farma);
        }

        public void Modificar(Encargado login, Farmaceutica farma)
        {
            IPersistenciaFarma perFarma = FabricaPersistencia.getPersistenciaFarma();
            perFarma.Modificar(login, farma);
        }

        public Farmaceutica Consulta(Encargado login, string nombre)
        {
            IPersistenciaFarma perFarma = FabricaPersistencia.getPersistenciaFarma();
            return perFarma.ConsultaTodas(login, nombre);
        }

        public Farmaceutica ConsultaTodas(Encargado login, string nombre)
        {
            IPersistenciaFarma perFarma = FabricaPersistencia.getPersistenciaFarma();
            return perFarma.ConsultaTodas(login, nombre);
        }
    }
}
