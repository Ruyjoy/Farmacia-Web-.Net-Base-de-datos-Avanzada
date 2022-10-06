using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaDetallePedido
    {
        internal static void Alta(LineaPedido l, int pNumP, SqlTransaction pTran)
        {
            SqlCommand _comm = new SqlCommand("LineaPedidos", pTran.Connection);
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.Parameters.AddWithValue("@numPedido", pNumP);
            _comm.Parameters.AddWithValue("@codigomed", l.Medicamento.Codigo);
            _comm.Parameters.AddWithValue("@nomfarma", l.Medicamento.Farma.Nombre);
            _comm.Parameters.AddWithValue("@cantidad", l.Cantidad);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            int _afectados;

            try
            {
                _comm.Transaction = pTran;

                _comm.ExecuteNonQuery();
                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                if (_afectados == -1)
                    throw new Exception("No hay stock suficiente para el medicamento: " + l.Medicamento.Nombre);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
