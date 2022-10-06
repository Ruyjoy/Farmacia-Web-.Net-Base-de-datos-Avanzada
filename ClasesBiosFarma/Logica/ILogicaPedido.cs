using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaPedido
    {
        void Alta(Pedido pedido);
        Pedido Consulta(int pNumPedido);
        void CambioEstado(Encargado login, Pedido pedido);
        List<Pedido> Listar(Encargado login);
    }
}
