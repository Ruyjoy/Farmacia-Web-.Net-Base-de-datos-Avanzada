using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaEncargado:IPersistenciaEncargado
    {
        private static PersistenciaEncargado _instancia = null;

        private PersistenciaEncargado() { }

        public static PersistenciaEncargado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEncargado();

            return _instancia;
        }

        public void Alta(Encargado login, Encargado encargado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("AltaEncargado", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@NomLogueo", encargado.NombreUsuario);
            _comm.Parameters.AddWithValue("@pass", encargado.Pass);
            _comm.Parameters.AddWithValue("@documento", encargado.Cedula);
            _comm.Parameters.AddWithValue("@nombre", encargado.NombreCompleto);
            _comm.Parameters.AddWithValue("@telefono", encargado.Telefono);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            int _afectados;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                if (_afectados == -1)
                    throw new Exception("El encargado ya existe en el sistema.");
                else if (_afectados == -2)
                    throw new Exception("Ha ocurrido un error con la base de datos.");
                else if (_afectados == -3)
                    throw new Exception("El nombre de usuario ya existe.");
                else if (_afectados == -4)
                    throw new Exception("Ha ocurrido un error al asignar los permisos en la base de datos.");
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

        public Encargado Consulta(Encargado login, int cedula)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BuscarEncargado", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@documento", cedula);

            Encargado encargado = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    encargado = new Encargado(_lector["telefono"].ToString(), Convert.ToInt32(_lector["Documento"]), _lector["NomLogueo"].ToString(), _lector["NombreCompleto"].ToString(), _lector["Pass"].ToString());
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
            return encargado;
        }

        public void CambioPass(Encargado encargado, string passNuevo)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(encargado));
            SqlCommand _comm = new SqlCommand("CambioPassword", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@passActual", encargado.Pass);
            _comm.Parameters.AddWithValue("@nuevaPass", passNuevo);
            _comm.Parameters.AddWithValue("@logueo", encargado.NombreUsuario);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            int _afectados;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                if (_afectados == -1)
                    throw new Exception("El empleado NO existe en el sistema.");
                else if (_afectados == -2)
                    throw new Exception("Ha ocurrido un error al cambiar el password en la base de datos.");

                encargado.Pass = passNuevo;
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

        public Encargado Login(string pNombreUsuario, string pass)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("Logueo", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.Parameters.AddWithValue("@NomLogueo", pNombreUsuario);
            _comm.Parameters.AddWithValue("@pass", pass);
            Encargado U = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    if (_lector["Telefono"].ToString() != null)
                    {
                        U = new Encargado(_lector["Telefono"].ToString(), Convert.ToInt32(_lector["Documento"]),
                            _lector["NomLogueo"].ToString(), _lector["NombreCompleto"].ToString(), _lector["Pass"].ToString());
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

            return U;
        }
    }
}
