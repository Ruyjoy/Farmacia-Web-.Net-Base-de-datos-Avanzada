using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaMedicamento:IPersistenciaMedicamento
    {
        private static PersistenciaMedicamento _instancia = null;

        private PersistenciaMedicamento() { }

        public static PersistenciaMedicamento GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaMedicamento();

            return _instancia;
        }


        public void Alta(Encargado login, Medicamento med)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("AltaMedicamento", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@codigo", med.Codigo);
            _comm.Parameters.AddWithValue("@nomFarma", med.Farma.Nombre);
            _comm.Parameters.AddWithValue("@nomMedicamento", med.Nombre);
            _comm.Parameters.AddWithValue("@descripcion", med.Descripcion);
            _comm.Parameters.AddWithValue("@tipoMedicamento", med.Tipo);
            _comm.Parameters.AddWithValue("@precio", med.Precio);
            _comm.Parameters.AddWithValue("@stock", med.Stock);

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
                    throw new Exception("El medicamento ya existe en el sistema.");
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

        public void Baja(Encargado login, Medicamento med)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BajaMedicamento", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@codigo", med.Codigo);
            _comm.Parameters.AddWithValue("@nomFarma", med.Farma.Nombre);

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
                    throw new Exception("El medicamento NO existe en el sistema.");
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

        public void Modificar(Encargado login, Medicamento med)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("ModMedicamento", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@codigo", med.Codigo);
            _comm.Parameters.AddWithValue("@nomFarma", med.Farma.Nombre);
            _comm.Parameters.AddWithValue("@descripcion", med.Descripcion);
            _comm.Parameters.AddWithValue("@tipoMedicamento", med.Tipo);
            _comm.Parameters.AddWithValue("@precio", med.Precio);
            _comm.Parameters.AddWithValue("@stock", med.Stock);

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
                    throw new Exception("El medicamento NO existe en el sistema.");
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

        public Medicamento Consulta(Encargado login, string codigo, string nombreFarma)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("BuscarMedicamentoActivo", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@codigo", codigo);
            _comm.Parameters.AddWithValue("@nomFarma", nombreFarma);

            Medicamento medicamento = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    Farmaceutica farma = PersistenciaFarma.GetInstancia().ConsultaTodas(login, nombreFarma);
                    medicamento = new Medicamento(_lector["Codigo"].ToString(), Convert.ToInt32(_lector["Stock"]), _lector["TipoMedicamento"].ToString(),
                        _lector["NomMedicamento"].ToString(), farma, _lector["Descripcion"].ToString(), Convert.ToDouble(_lector["Precio"]));
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
            return medicamento;
        }

        internal Medicamento Consulta(string codigo, string nombreFarma)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("BuscarMedicamentoTodas", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            _comm.Parameters.AddWithValue("@codigo", codigo);
            _comm.Parameters.AddWithValue("@nomFarma", nombreFarma);

            Medicamento medicamento = null;
            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _lector = _comm.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    Farmaceutica farma = PersistenciaFarma.GetInstancia().Consulta(nombreFarma);
                    medicamento = new Medicamento(_lector["Codigo"].ToString(), Convert.ToInt32(_lector["Stock"]), _lector["TipoMedicamento"].ToString(),
                        _lector["NomMedicamento"].ToString(), farma, _lector["Descripcion"].ToString(), Convert.ToDouble(_lector["Precio"]));
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
            return medicamento;
        }     

        public List<Medicamento> ListarActivos()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("ListarMedicamentos", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            List<Medicamento> medicamentos = new List<Medicamento>(); ;
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
                        Farmaceutica farma = PersistenciaFarma.GetInstancia().Consulta(_lector["NomFarmaceutica"].ToString());
                        medicamentos.Add(new Medicamento(_lector["Codigo"].ToString(), Convert.ToInt32(_lector["Stock"]), _lector["TipoMedicamento"].ToString(),
                            _lector["NomMedicamento"].ToString(), farma, _lector["Descripcion"].ToString(), Convert.ToDouble(_lector["Precio"])));
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
            return medicamentos;
        }


        public List<Medicamento> ListarActivos(Empleado login)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConectionU(login));
            SqlCommand _comm = new SqlCommand("ListarMedicamentos", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;

            List<Medicamento> medicamentos = new List<Medicamento>(); ;
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
                        Farmaceutica farma = PersistenciaFarma.GetInstancia().Consulta(_lector["NomFarmaceutica"].ToString());
                        medicamentos.Add(new Medicamento(_lector["Codigo"].ToString(), Convert.ToInt32(_lector["Stock"]), _lector["TipoMedicamento"].ToString(),
                            _lector["NomMedicamento"].ToString(), farma, _lector["Descripcion"].ToString(), Convert.ToDouble(_lector["Precio"])));
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
            return medicamentos;
        }
    }
}
