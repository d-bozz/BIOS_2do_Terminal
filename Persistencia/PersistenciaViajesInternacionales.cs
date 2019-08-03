using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;
namespace Persistencia
{
    internal class PersistenciaViajesInternacionales : IPersistenciaViajesInternacionales
    {
        //Singleton *******************************************
        private static PersistenciaViajesInternacionales _instancia = null;

        private PersistenciaViajesInternacionales()
        {
        }

        public static PersistenciaViajesInternacionales GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaViajesInternacionales();
            return _instancia;
        }
        //******************************************************

        public void AgregarViajeInternacional(ViajeInternacional VI)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("AgregarInter", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", VI.Numero);
            SqlParameter nomCompania = new SqlParameter("@nomCompania", VI.Compania.Nombre);
            SqlParameter destino = new SqlParameter("@destino", VI.Destino.Cod);
            SqlParameter fSalida = new SqlParameter("@fSalida", VI.FechaSalida);
            SqlParameter fArribo = new SqlParameter("@fArribo", VI.FechaArribo);
            SqlParameter CantAsientos = new SqlParameter("@CantAsientos", VI.CantidadAsientos);
            SqlParameter ultEmpleado = new SqlParameter("@ultEmpleado", VI.Usuario.Ci);
            SqlParameter servAbordo = new SqlParameter("@servAbordo", VI.ServicioABordo);
            SqlParameter documentos = new SqlParameter("@documentos", VI.Documentos);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(numViaje);
            oComando.Parameters.Add(nomCompania);
            oComando.Parameters.Add(destino);
            oComando.Parameters.Add(fSalida);
            oComando.Parameters.Add(fArribo);
            oComando.Parameters.Add(CantAsientos);
            oComando.Parameters.Add(ultEmpleado);
            oComando.Parameters.Add(servAbordo);
            oComando.Parameters.Add(documentos);

            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El viaje internacional ya existe.");
                if (oAfectados == -2)
                    throw new Exception("Debe haber una diferencia de 2 horas entre llegada y salida a un mismo destino.");
                if (oAfectados == -6)
                    throw new Exception("Error en Sql.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }

        public void EliminarViajeInternacional(ViajeInternacional VI)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("EliminarInter", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", VI.Numero);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(numViaje);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe el viaje internacional ingresado.");
                if (oAfectados == -6)
                    throw new Exception("Error en Sql.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }

        public void ModificarViajeInternacional(ViajeInternacional VI)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("ModificarInter", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", VI.Numero);
            SqlParameter nomCompania = new SqlParameter("@nomCompania", VI.Compania.Nombre);
            SqlParameter destino = new SqlParameter("@destino", VI.Destino.Cod);
            SqlParameter fSalida = new SqlParameter("@fSalida", VI.FechaSalida);
            SqlParameter fArribo = new SqlParameter("@fArribo", VI.FechaArribo);
            SqlParameter CantAsientos = new SqlParameter("@CantAsientos", VI.CantidadAsientos);
            SqlParameter ultEmpleado = new SqlParameter("@ultEmpleado", VI.Usuario.Ci);
            SqlParameter servAbordo = new SqlParameter("@servAbordo", VI.ServicioABordo);
            SqlParameter documentos = new SqlParameter("@documentos", VI.Documentos);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;
            oComando.Parameters.Add(numViaje);
            oComando.Parameters.Add(nomCompania);
            oComando.Parameters.Add(destino);
            oComando.Parameters.Add(fSalida);
            oComando.Parameters.Add(fArribo);
            oComando.Parameters.Add(CantAsientos);
            oComando.Parameters.Add(ultEmpleado);
            oComando.Parameters.Add(servAbordo);
            oComando.Parameters.Add(documentos);

            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El viaje internacional no existe.");
                if (oAfectados == -2)
                    throw new Exception("Debe haber una diferencia de 2 horas entre llegada y salida a un mismo destino.");
                if (oAfectados == -6)
                    throw new Exception("Error en Sql.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }

        public ViajeInternacional BuscarViajeInter(int pNumero) 
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            bool servAbordo;
            string documentos;

            ViajeInternacional VI = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarViajeInter", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@numViaje", pNumero);
            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.Read())
                {
                    numViaje = (int)oReader["numViaje"];
                    nomCompania = PersistenciaCompanias.GetInstancia().BuscarCompaniaTodas(oReader["nomCompania"].ToString());
                    destino = PersistenciaDestinos.GetInstancia().BuscarDestinoTodos(oReader["destino"].ToString());
                    fSalida = (DateTime)oReader["fSalida"];
                    fArribo = (DateTime)oReader["fArribo"];
                    CantAsientos = (int)oReader["CantAsientos"];
                    ultEmpleado = PersistenciaEmpleados.GetInstancia().BuscarEmpleadoTodos(oReader["ultEmpleado"].ToString());
                    servAbordo = (bool)oReader["servAbordo"];
                    documentos = (string)oReader["documentos"];

                    VI = new ViajeInternacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, servAbordo, documentos);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
            return VI;
        }

        public List<ViajeInternacional> ListadoInter()  
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            bool servAbordo;
            string documentos;

            List<ViajeInternacional> _Lista = new List<ViajeInternacional>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoInter", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();
                while (_Reader.Read())
                {
                    numViaje = (int)_Reader["numViaje"];
                    nomCompania = PersistenciaCompanias.GetInstancia().BuscarCompaniaTodas(_Reader["nomCompania"].ToString());
                    destino = PersistenciaDestinos.GetInstancia().BuscarDestinoTodos(_Reader["destino"].ToString());
                    fSalida = (DateTime)_Reader["fSalida"];
                    fArribo = (DateTime)_Reader["fArribo"];
                    CantAsientos = (int)_Reader["CantAsientos"];
                    ultEmpleado = PersistenciaEmpleados.GetInstancia().BuscarEmpleadoTodos(_Reader["ultEmpleado"].ToString());
                    servAbordo = (bool)_Reader["servAbordo"];
                    documentos = (string)_Reader["documentos"];

                    ViajeInternacional VI = new ViajeInternacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, servAbordo, documentos);
                    _Lista.Add(VI);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
            return _Lista;
        }

        public List<ViajeInternacional> ListadoSinPartirInter()
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            bool servAbordo;
            string documentos;

            List<ViajeInternacional> _Lista = new List<ViajeInternacional>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoSinPartirInter", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();
                while (_Reader.Read())
                {
                    numViaje = (int)_Reader["numViaje"];
                    nomCompania = PersistenciaCompanias.GetInstancia().BuscarCompaniaTodas(_Reader["nomCompania"].ToString());
                    destino = PersistenciaDestinos.GetInstancia().BuscarDestinoTodos(_Reader["destino"].ToString());
                    fSalida = (DateTime)_Reader["fSalida"];
                    fArribo = (DateTime)_Reader["fArribo"];
                    CantAsientos = (int)_Reader["CantAsientos"];
                    ultEmpleado = PersistenciaEmpleados.GetInstancia().BuscarEmpleadoTodos(_Reader["ultEmpleado"].ToString());
                    servAbordo = (bool)_Reader["servAbordo"];
                    documentos = (string)_Reader["documentos"];

                    ViajeInternacional VI = new ViajeInternacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, servAbordo, documentos);
                    _Lista.Add(VI);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
            return _Lista;
        }
    }
}
