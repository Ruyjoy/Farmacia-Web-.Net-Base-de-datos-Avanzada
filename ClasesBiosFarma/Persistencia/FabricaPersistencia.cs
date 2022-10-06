using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaEmpleado getPersistenciaEmpleado()
        {
            return PersistenciaEmpleado.GetInstancia();
        }
        public static IPersistenciaEncargado getPersistenciaEncargado()
        {
            return PersistenciaEncargado.GetInstancia();
        }
        public static IPersistenciaFarma getPersistenciaFarma()
        {
            return PersistenciaFarma.GetInstancia();
        }
        public static IPersistenciaMedicamento getPersistenciaMedicamento()
        {
            return PersistenciaMedicamento.GetInstancia();
        }
        public static IPersistenciaPedido getPersistenciaPedido()
        {
            return PersistenciaPedido.GetInstancia();
        }
        public static IPersistenciaHorasExtras getPersistenciaHorasExtras()
        {
            return PersistenciaHorasExtras.GetInstancia();
        }
    }
}
