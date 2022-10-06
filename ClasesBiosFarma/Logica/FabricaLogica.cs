using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaUsuario getLogicaUsuario()
        {
            return LogicaUsuario.GetInstancia();
        }
        public static ILogicaMedicamento getLogicaMedicamento()
        {
            return LogicaMedicamento.GetInstancia();
        }
        public static ILogicaFarma getLogicaFarma()
        {
            return LogicaFarma.GetInstancia();
        }
        public static ILogicaPedido getLogicaPedido()
        {
            return LogicaPedido.GetInstancia();
        }
        public static ILogicaHorasExtras getLogicaExtras()
        {
            return LogicaHorasExtras.GetInstancia();
        }
    }
}
