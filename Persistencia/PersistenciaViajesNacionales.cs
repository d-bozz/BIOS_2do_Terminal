using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;
namespace Persistencia
{
    internal class PersistenciaViajesNacionales : IPersistenciaViajesNacionales
    {
        //Singleton *******************************************
        private static PersistenciaViajesNacionales _instancia = null;

        private PersistenciaViajesNacionales()
        {
        }

        public static PersistenciaViajesNacionales GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaViajesNacionales();
            return _instancia;
        }
        //******************************************************

        public void AgregarViajeNacional(ViajeNacional Vn)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("AgregarNacional", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", Vn.Numero);
            SqlParameter nomCompania = new SqlParameter("@nomCompania", Vn.Compania.Nombre);
            SqlParameter destino = new SqlParameter("@destino", Vn.Destino.Cod);
            SqlParameter fSalida = new SqlParameter("@fSalida", Vn.FechaSalida);
            SqlParameter fArribo = new SqlParameter("@fArribo", Vn.FechaArribo);
            SqlParameter CantAsientos = new SqlParameter("@CantAsientos", Vn.CantidadAsientos);
            SqlParameter ultEmpleado = new SqlParameter("@ultEmpleado", Vn.Usuario.Ci);
            SqlParameter paradas = new SqlParameter("@paradas", Vn.Paradas);

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
            oComando.Parameters.Add(paradas);

            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El viaje ya existe.");
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

        public void EliminarViajeNacional(ViajeNacional Vn)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("EliminarNacional", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", Vn.Numero);
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
                    throw new Exception("No existe el viaje nacional ingresado.");
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

        public void ModificarViajeNacional(ViajeNacional Vn)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("ModificarNacional", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter numViaje = new SqlParameter("@numViaje", Vn.Numero);
            SqlParameter nomCompania = new SqlParameter("@nomCompania", Vn.Compania.Nombre);
            SqlParameter destino = new SqlParameter("@destino", Vn.Destino.Cod);
            SqlParameter fSalida = new SqlParameter("@fSalida", Vn.FechaSalida);
            SqlParameter fArribo = new SqlParameter("@fArribo", Vn.FechaArribo);
            SqlParameter CantAsientos = new SqlParameter("@CantAsientos", Vn.CantidadAsientos);
            SqlParameter ultEmpleado = new SqlParameter("@ultEmpleado", Vn.Usuario.Ci);
            SqlParameter paradas = new SqlParameter("@paradas", Vn.Paradas);

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
            oComando.Parameters.Add(paradas);

            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El viaje nacional no existe.");
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

        public ViajeNacional BuscarViajeNacional(int pNumero) 
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            int paradas;

            ViajeNacional Vn = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarViajeNacional", oConexion);
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
                    paradas = (int)oReader["paradas"];

                    Vn = new ViajeNacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, paradas);
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
            return Vn;
        }

        public List<ViajeNacional> ListadoNacionales() 
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            int paradas;

            List<ViajeNacional> _Lista = new List<ViajeNacional>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoNacionales", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();
                while (_Reader.Read())
                {
                    numViaje = (int)_Reader["numViaje"];
                    nomCompania = PersistenciaCompanias.GetInstancia().BuscarCompaniaTodas( _Reader["nomCompania"].ToString());
                    destino = PersistenciaDestinos.GetInstancia().BuscarDestinoTodos( _Reader["destino"].ToString());
                    fSalida = (DateTime)_Reader["fSalida"];
                    fArribo = (DateTime)_Reader["fArribo"];
                    CantAsientos = (int)_Reader["CantAsientos"];
                    ultEmpleado = PersistenciaEmpleados.GetInstancia().BuscarEmpleadoTodos( _Reader["ultEmpleado"].ToString());
                    paradas = (int)_Reader["paradas"];

                    ViajeNacional Vn = new ViajeNacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, paradas);
                    _Lista.Add(Vn);
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

        public List<ViajeNacional> ListadoSinPartirNac()
        {
            int numViaje;
            Compania nomCompania;
            Destino destino;
            DateTime fSalida;
            DateTime fArribo;
            int CantAsientos;
            Empleado ultEmpleado;
            int paradas;

            List<ViajeNacional> _Lista = new List<ViajeNacional>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoSinPartirNac", _Conexion);
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
                    paradas = (int)_Reader["paradas"];

                    ViajeNacional Vn = new ViajeNacional(numViaje, fSalida, fArribo, CantAsientos, ultEmpleado, destino, nomCompania, paradas);
                    _Lista.Add(Vn);
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
