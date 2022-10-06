using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaEmpleado:IPersistenciaEmpleado
    {

        private static PersistenciaEmpleado _instancia = null;

        private PersistenciaEmpleado() { }

        public static PersistenciaEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEmpleado();

            return _instancia;
        }

        public void Alta(Encargado login, Empleado empleado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("AltaEmpleado", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@NomLogueo", empleado.NombreUsuario);
            _comm.Parameters.AddWithValue("@pass",empleado.Pass);
            _comm.Parameters.AddWithValue("@documento",empleado.Cedula);
            _comm.Parameters.AddWithValue("@nombre", empleado.NombreCompleto);
            _comm.Parameters.AddWithValue("@horainicio", empleado.InicioTareas);
            _comm.Parameters.AddWithValue("@horafinal",empleado.FinTareas);

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
                    throw new Exception("El empleado ya existe en el sistema.");
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

        public void Modificar(Encargado login, Empleado empleado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("ModEmpleado", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@documento", empleado.Cedula);
            _comm.Parameters.AddWithValue("@nombre", empleado.NombreCompleto);
            _comm.Parameters.AddWithValue("@horainicio", empleado.InicioTareas);
            _comm.Parameters.AddWithValue("@horafinal", empleado.FinTareas);

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
                    throw new Exception("Ha ocurrido un error con la base de datos.");
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

        public void Baja(Encargado login, Empleado empleado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BajaEmpleado", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@documento", empleado.Cedula);
            _comm.Parameters.AddWithValue("@NomLogueo", empleado.NombreUsuario);

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
                    throw new Exception("Ha ocurrido un error con la base de datos.");
                else if (_afectados == -4)
                    throw new Exception("Ha ocurrido un error quitar los permisos en la base de datos.");
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

        public Empleado Consulta(Encargado login, int cedula)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BuscarEmpleadoAct", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@documento", cedula);

            Empleado empleado = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    empleado = new Empleado((DateTime)_lector["HoraInicio"], (DateTime)_lector["HoraFinal"], Convert.ToInt32(_lector["Documento"]), _lector["NomLogueo"].ToString(), _lector["NombreCompleto"].ToString(), _lector["Pass"].ToString());
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
            return empleado;
        }

        internal Empleado ConsultaTodos(int cedula)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("BuscarEmpleadoTodos", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@documento", cedula);

            Empleado empleado = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    empleado = new Empleado((DateTime)_lector["HoraInicio"], (DateTime)_lector["HoraFinal"], Convert.ToInt32(_lector["Documento"]), _lector["NomLogueo"].ToString(), _lector["NombreCompleto"].ToString(), _lector["Pass"].ToString());
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
            return empleado;
        }

        public void CambioPass(Empleado empleado, string passNuevo)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(empleado));
            SqlCommand _comm = new SqlCommand("CambioPassword", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@passActual", empleado.Pass);
            _comm.Parameters.AddWithValue("@nuevaPass", passNuevo);
            _comm.Parameters.AddWithValue("@logueo", empleado.NombreUsuario);

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

                empleado.Pass = passNuevo;
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

        public Empleado Login(string pNombreUsuario, string pass)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("Logueo", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.Parameters.AddWithValue("@NomLogueo", pNombreUsuario);
            _comm.Parameters.AddWithValue("@pass", pass);
            Empleado U = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    if (_lector["HoraInicio"].ToString() != null)
                    {
                        U = new Empleado(Convert.ToDateTime(_lector["HoraInicio"]), Convert.ToDateTime(_lector["HoraFinal"]), Convert.ToInt32(_lector["Documento"]),
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
