using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaHorasExtras:ILogicaHorasExtras
    {
        private static LogicaHorasExtras _instancia = null;

        private LogicaHorasExtras() { }

        public static LogicaHorasExtras GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaHorasExtras();

            return _instancia;
        }



        public void AgregarHorasExtras(HorasExtras pHorasExtras)
        {
            IPersistenciaHorasExtras perExtras = FabricaPersistencia.getPersistenciaHorasExtras();
            perExtras.AgregarHorasExtras(pHorasExtras);
        }
    }
}
