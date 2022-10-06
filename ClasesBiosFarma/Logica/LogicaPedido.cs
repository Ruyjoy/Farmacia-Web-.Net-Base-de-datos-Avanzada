using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaPedido:ILogicaPedido
    {
        private static LogicaPedido _instancia = null;

        private LogicaPedido() { }

        public static LogicaPedido GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaPedido();

            return _instancia;
        }

        public void Alta(Pedido pedido)
        {
            FabricaPersistencia.getPersistenciaPedido().Alta(pedido);
        }

        public Pedido Consulta(int pNumPedido)
        {
            return FabricaPersistencia.getPersistenciaPedido().Consulta(pNumPedido);
        }

        public void CambioEstado(Encargado login, Pedido pedido)
        {
            FabricaPersistencia.getPersistenciaPedido().CambioEstado(login, pedido);
        }

        public List<Pedido> Listar(Encargado login)
        {
            return FabricaPersistencia.getPersistenciaPedido().Listar(login);
        }
    }
}
