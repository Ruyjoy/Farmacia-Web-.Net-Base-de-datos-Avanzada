using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaHorasExtras:IPersistenciaHorasExtras
    {
        private static PersistenciaHorasExtras _instancia = null;

        private PersistenciaHorasExtras() { }

        public static PersistenciaHorasExtras GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaHorasExtras();

            return _instancia;
        }

        public void AgregarHorasExtras(HorasExtras pHorasExtras)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.getConection());
            SqlCommand _comm = new SqlCommand("AgregarExtras", _cnn);
            _comm.CommandType = CommandType.StoredProcedure;


            _comm.Parameters.AddWithValue("@documento", pHorasExtras.Empleado.Cedula);
            _comm.Parameters.AddWithValue("@fecha", pHorasExtras.Fecha);
            _comm.Parameters.AddWithValue("@minutos", pHorasExtras.CantMinutos);


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comm.Parameters.Add(_retorno);

            int _afectados;

            try
            {
                _cnn.Open();
                _comm.ExecuteNonQuery();

                _afectados = (int)_comm.Parameters["@Retorno"].Value;
                
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
    }
}
