using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaFarma:IPersistenciaFarma
    {
        private static PersistenciaFarma _instancia = null;

        private PersistenciaFarma() { }

        public static PersistenciaFarma GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaFarma();

            return _instancia;
        }



        public void Alta(Encargado login, Farmaceutica farma)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("AltaFarmaceutica", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@nombre", farma.Nombre);
            _comm.Parameters.AddWithValue("@direccion", farma.DireccionFiscal);
            _comm.Parameters.AddWithValue("@telefono", farma.Telefono);
            _comm.Parameters.AddWithValue("@email", farma.Email);

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
                    throw new Exception("La farmaceutica ya existe en el sistema.");
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

        public void Baja(Encargado login, Farmaceutica farma)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BajaFarmaceutica", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@nombre", farma.Nombre);

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
                    throw new Exception("La farmaceutica NO existe en el sistema.");
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

        public void Modificar(Encargado login, Farmaceutica farma)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("ModFarmaceutica", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@nombre", farma.Nombre);
            _comm.Parameters.AddWithValue("@direccion", farma.DireccionFiscal);
            _comm.Parameters.AddWithValue("@telefono", farma.Telefono);
            _comm.Parameters.AddWithValue("@email", farma.Email);

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
                    throw new Exception("La farmaceutica NO existe en el sistema.");
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

        public Farmaceutica Consulta(Encargado login, string nombre)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BuscoFarmaceutica", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@nombre", nombre);

            Farmaceutica farma = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    farma = new Farmaceutica(_lector["nombre"].ToString(), _lector["Telefono"].ToString(), _lector["Direccion"].ToString(),
                        _lector["Email"].ToString());
                    
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
            return farma;
        }

        public Farmaceutica ConsultaTodas(Encargado login, string nombre)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BuscoFarmaceuticaTodas", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@nombre", nombre);

            Farmaceutica farma = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    farma = new Farmaceutica(_lector["nombre"].ToString(), _lector["Telefono"].ToString(), _lector["Direccion"].ToString(),
                        _lector["Email"].ToString());

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
            return farma;
        }

        internal Farmaceutica Consulta(string nombre)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("BuscoFarmaceuticaTodas", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@nombre", nombre);

            Farmaceutica farma = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    farma = new Farmaceutica(_lector["nombre"].ToString(), _lector["Telefono"].ToString(), _lector["Direccion"].ToString(),
                        _lector["Email"].ToString());

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
            return farma;
        }

        
    }
}
