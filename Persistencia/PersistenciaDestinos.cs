using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;
namespace Persistencia
{
    internal class PersistenciaDestinos : IPersistenciaDestinos
    {
        //Singleton *******************************************
        private static PersistenciaDestinos _instancia = null;

        private PersistenciaDestinos()
        {
        }

        public static PersistenciaDestinos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaDestinos();
            return _instancia;
        }
        //******************************************************

        public void AgregarDestino(Destino D)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlTransaction _miTransaccion = null;
            SqlCommand _Comando = new SqlCommand("AltaDestino", _Conexion);

            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@codigo", D.Cod);
            _Comando.Parameters.AddWithValue("@ciudad", D.Ciudad);
            _Comando.Parameters.AddWithValue("@pais", D.Pais);
            SqlParameter Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(Retorno);
            int _retorno;
            try
            {
                _Conexion.Open();
                _miTransaccion = _Conexion.BeginTransaction();
                _Comando.Transaction = _miTransaccion;
                _Comando.ExecuteNonQuery();
                _retorno = (int)_Comando.Parameters["@Retorno"].Value;
                if (_retorno == -1)
                    throw new Exception("El Destino que intenta agregar ya existe.");
                else if (_retorno == -6)
                    throw new Exception("Error SQL, el destino no fue agregado.");

                foreach (Facilidades f in D.LasFacilidades)
                    PersistenciaFacilidades.GetInstancia().Alta(f, D.Cod, _miTransaccion);
                _miTransaccion.Commit();


            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new ApplicationException("Error con la base de datos: " + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
        }

        public void EliminarDestino(Destino D)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("BajaDestino", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@codigo", D.Cod);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            int retorno;
            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                retorno = (int)_Comando.Parameters["@Retorno"].Value;
                if (retorno == -1)
                    throw new Exception("El destino que quiere eliminar no existe.");
                else if (retorno == -6)
                    throw new Exception("Error SQL, no se elimino el destino.");
            }
            catch (Exception ex)
            { throw new ApplicationException("Error con la Base de Datos :" + ex.Message); }
            finally
            { _Conexion.Close(); }
 
        }

        public void ModificarDestino(Destino D)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlTransaction _miTransaccion = null;
            SqlCommand _Comando = new SqlCommand("ModificarDestino", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            
            _Comando.Parameters.AddWithValue("@codigo", D.Cod);
            _Comando.Parameters.AddWithValue("@ciudad", D.Ciudad);
            _Comando.Parameters.AddWithValue("@pais", D.Pais);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            int _retorno;
            try
            {
                _Conexion.Open();
                _miTransaccion = _Conexion.BeginTransaction();
                _Comando.Transaction = _miTransaccion;
                _Comando.ExecuteNonQuery();
                _retorno = (int)_Comando.Parameters["@Retorno"].Value;
                if (_retorno == -1)
                    throw new Exception("El Destino que intenta modificar no existe.");
                else if (_retorno == -6)
                    throw new Exception("Error SQL, el destino no fue modificado.");

                PersistenciaFacilidades.GetInstancia().EliminarFacilidades(D, _miTransaccion);

                foreach (Facilidades f in D.LasFacilidades)
                    PersistenciaFacilidades.GetInstancia().Alta(f, D.Cod, _miTransaccion);
                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new ApplicationException("Error con la base de datos: " + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
        }

        public Destino BuscarDestinoActivo(string pCod)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("BuscarDestinoActivo", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@codigo", pCod);
            SqlDataReader lector;
            Destino unDestino = null;
            List<Facilidades> lasFacilidades = new List<Facilidades>();
            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string codigo = (string)lector["codigo"];
                    string cidudad = (string)lector["ciudad"];
                    string pais = (string)lector["pais"];
                    unDestino = new Destino(codigo,cidudad,pais,lasFacilidades);
                    lector.Close();
                    if (unDestino != null)
                    {
                        PersistenciaFacilidades.GetInstancia().FacilidadesDeUnDestino(unDestino);
                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
            return unDestino; 
        }


        internal Destino BuscarDestinoTodos(string pCod)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("BuscarDestinoTodos", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@codigo", pCod);
            SqlDataReader lector;
            Destino unDestino = null;
            List<Facilidades> lasFacilidades = new List<Facilidades>();
            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string codigo = (string)lector["codigo"];
                    string cidudad = (string)lector["ciudad"];
                    string pais = (string)lector["pais"];
                    unDestino = new Destino(codigo, cidudad, pais, lasFacilidades);
                    lector.Close();
                    if (unDestino != null)
                    {
                        PersistenciaFacilidades.GetInstancia().FacilidadesDeUnDestino(unDestino);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
            return unDestino;
        }

        public List<Destino> ListarDestinos()
        {
            List<Destino> destinos = new List<Destino>();
            Destino unDestino = null;

            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListarDestinosActivos", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    List<Facilidades> lasFacilidades = new List<Facilidades>();
                    unDestino = new Destino((string)lector["codigo"],(string)lector["ciudad"],(string)lector["pais"],lasFacilidades);                   
                    PersistenciaFacilidades.GetInstancia().FacilidadesDeUnDestino(unDestino);
                    destinos.Add(unDestino);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { _Conexion.Close(); }

            return destinos; 
        }


    }
}
