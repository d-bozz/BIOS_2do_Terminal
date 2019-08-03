using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaFacilidades
    {
        //Singleton********************************************
        internal static PersistenciaFacilidades _instancia = null;

        internal PersistenciaFacilidades()
        {
        }

        internal static PersistenciaFacilidades GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaFacilidades();
            return _instancia;
        }
        //*****************************************************

        internal void Alta(Facilidades unaFacilidad, string pCod, SqlTransaction _pTransaccion)
        {


            SqlCommand _comando = new SqlCommand("AgregarFacilidades", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@codigo", pCod);
            _comando.Parameters.AddWithValue("@facilidad", unaFacilidad.Facilidad);
            SqlParameter _parRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _parRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_parRetorno);

            try
            {
                //determino que debo trabajar con la misma transaccion
                _comando.Transaction = _pTransaccion;
                //ejecuto comando
                _comando.ExecuteNonQuery();
                //Verifico si hay errores

                int retorno = Convert.ToInt32(_parRetorno.Value);
                if (retorno == -1)
                    throw new Exception("Destino no valido");
                else if (retorno == 0)
                    throw new Exception("Error no especificado");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        internal void FacilidadesDeUnDestino(Destino unDestino)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.MiConexion);

            SqlCommand _comando = new SqlCommand("FacilidadesDeUnDestino", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Cod", unDestino.Cod);

            try
            {
                //me conecto
                _cnn.Open();

                //ejecuto consulta
                SqlDataReader _lector = _comando.ExecuteReader();

                //verifico si hay facilidades 
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        Facilidades f = new Facilidades((string)_lector["facilidad"]);
                        unDestino.AgregarFacilidad(f);
                    }
                }

                _lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }
        }


        internal void EliminarFacilidades(Destino unDestino, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("EliminarFacilidades", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@codigo", unDestino.Cod);
            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);
            int retorno;
            try
            {
                //determino que debo trabajar con la misma transaccion
                _comando.Transaction = _pTransaccion;

                //ejecuto comando
                _comando.ExecuteNonQuery();
                retorno = (int)_comando.Parameters["@Retorno"].Value;
                if (retorno == -1)
                    throw new Exception("Error Sql, no se pudo eliminar las facilidades.");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

