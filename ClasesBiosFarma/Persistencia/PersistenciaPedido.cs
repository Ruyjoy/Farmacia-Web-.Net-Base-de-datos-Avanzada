using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaPedido:IPersistenciaPedido
    {
        private static PersistenciaPedido _instancia = null;

        private PersistenciaPedido() { }

        public static PersistenciaPedido GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaPedido();

            return _instancia;
        }


        public void Alta(Pedido pedido)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(pedido.Empleado));
            SqlCommand _comm = new SqlCommand("AltaPedido", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.Parameters.AddWithValue("@fechapedido", pedido.FechaRealizado);
            _comm.Parameters.AddWithValue("@direccion", pedido.DireccionEntrega);
            _comm.Parameters.AddWithValue("@usuariopedido", pedido.Empleado.Cedula);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            SqlTransaction _transaccion = null;
            int _afectados;

            try
            {
                _cnn.Open();
                _transaccion = _cnn.BeginTransaction();
                _comm.Transaction = _transaccion;
                _comm.ExecuteNonQuery();

                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                pedido.Numero = _afectados;
            
                foreach (LineaPedido l in pedido.DetallePedido)
                {
                    PersistenciaDetallePedido.Alta(l, pedido.Numero, _transaccion);
                }
                
                _transaccion.Commit();
            }
            catch (Exception ex)
            {
                _transaccion.Rollback();
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public Pedido Consulta(int pNumPedido)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("BuscarPedido", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@numPedido", pNumPedido);

            Pedido pedido = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        if (pedido == null)
                            pedido = new Pedido(pNumPedido, _lector["Estado"].ToString(), _lector["Direccion"].ToString(), (DateTime)_lector["FechaPedido"],
                                PersistenciaEmpleado.GetInstancia().ConsultaTodos((int)_lector["UsuarioPedido"]), new List<LineaPedido>());

                        LineaPedido linea = new LineaPedido(PersistenciaMedicamento.GetInstancia().Consulta(_lector["CodigoMed"].ToString(), _lector["NomFarma"].ToString()), Convert.ToInt32(_lector["Cantidad"].ToString()));
                        pedido.AgregarLineaPedido(linea);
                    }
                }

                _lector.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return pedido;
        }

        public void CambioEstado(Encargado login, Pedido pedido)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("CambioEstadoPedido", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@numPedido", pedido.Numero);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            int _afectados;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                if (_afectados != 0)
                    throw new Exception("Ocurrió un error en la BD.");

                pedido = this.Consulta(pedido.Numero);

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }

        public List<Pedido> Listar(Encargado login)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("ListarPedidos", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            List<Pedido> lista = new List<Pedido>();
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    int numPedido = 0;
                    int numAnterior = -1;
                    while (_lector.Read())
                    {
                        numPedido =(int)_lector["NumPedido"];
                        if (numPedido != numAnterior)
                        {
                            numAnterior = numPedido;
                            Pedido p = this.Consulta(numPedido);
                            lista.Add(p);
                        }
                    }
                }
                _lector.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return lista;
        }
    }
}
