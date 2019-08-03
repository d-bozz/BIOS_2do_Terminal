using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaCompanias : IPersistenciaCompanias
    {
        //Singleton********************************************
        private static PersistenciaCompanias _instancia = null;
        
        private PersistenciaCompanias() 
        {
        }

        public static PersistenciaCompanias GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCompanias();
            return _instancia;
        }
        //*****************************************************

        public void AgregarCompania(Compania C)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("AltaCompania", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter nombre = new SqlParameter("@nombre", C.Nombre);
            SqlParameter direccion = new SqlParameter("@direccion", C.Direccion);
            SqlParameter telefono = new SqlParameter("@telefono", C.Telefono);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(nombre);
            oComando.Parameters.Add(direccion);
            oComando.Parameters.Add(telefono);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("La compania ya existe.");
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

        public void EliminarCompania(Compania C)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("EliminarCompania", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter nombre = new SqlParameter("@nombre", C.Nombre);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(nombre);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe la compania.");
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

        public void ModificarCompania(Compania C)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("ModificarCompania", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter nombre = new SqlParameter("@nombre", C.Nombre);
            SqlParameter direccion = new SqlParameter("@direccion", C.Direccion);
            SqlParameter telefono = new SqlParameter("@telefono", C.Telefono);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(nombre);
            oComando.Parameters.Add(direccion);
            oComando.Parameters.Add(telefono);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("La compania no existe.");
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

        public Compania BuscarCompaniaActiva(string pNombre)
        {
            string nombre;
            string direccion;
            int telefono;
            Compania C = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarCompaniaActiva",oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@nombre", pNombre);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.Read())
                {
                    nombre = (string)oReader["nombre"];
                    direccion = (string)oReader["direccion"];
                    telefono = (int)oReader["telefono"];
                    C = new Compania(nombre, direccion, telefono);
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
            return C;    
        }

        internal Compania BuscarCompaniaTodas(string pNombre)
        {
            string nombre;
            string direccion;
            int telefono;
            Compania C = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarCompaniaTodas", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@nombre", pNombre);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.Read())
                {
                    nombre = (string)oReader["nombre"];
                    direccion = (string)oReader["direccion"];
                    telefono = (int)oReader["telefono"];
                    C = new Compania(nombre, direccion, telefono);
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
            return C;    
        }

        public List<Compania> ListarCompanias()
        {
            string nombre;
            string direccion;
            int telefono;
            List<Compania> _Lista = new List<Compania>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoCompanias", _Conexion);

            _Comando.CommandType = CommandType.StoredProcedure;


            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();
                while (_Reader.Read())
                {
                    nombre = (string)_Reader["nombre"];
                    direccion = (string)_Reader["direccion"];
                    telefono = (int)_Reader["telefono"];
                    Compania C = new Compania(nombre, direccion, telefono);
                    _Lista.Add(C);
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
