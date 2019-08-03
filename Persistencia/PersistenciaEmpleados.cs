using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaEmpleados : IPersistenciaEmpleados
    {
         //Singleton *******************************************
        private static PersistenciaEmpleados _instancia = null;

        private PersistenciaEmpleados() 
        {
        }

        public static PersistenciaEmpleados GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEmpleados();
            return _instancia;
        }
        //******************************************************


        public Empleado Login(string pCi, string pPass)
        {
            Empleado E = null;

            SqlConnection conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand comando = new SqlCommand("Login", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ci", pCi);
            comando.Parameters.AddWithValue("@contrasena", pPass);

            SqlDataReader lector;

            try 
            {
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    E = new Empleado((string)lector["ci"], (string)lector["contrasena"], (string)lector["nomCompleto"]);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message); 
            }
            finally
            {
                conexion.Close();
            }

            return E;
        }

        public void AgregarEmpleado(Empleado E)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("AltaEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter ci = new SqlParameter("@ci", E.Ci);
            SqlParameter contrasena = new SqlParameter("@contrasena", E.Contrasena);
            SqlParameter nomCompleto = new SqlParameter("@nomCompleto", E.Nombre);
           
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(ci);
            oComando.Parameters.Add(contrasena);
            oComando.Parameters.Add(nomCompleto);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El empleado ya existe.");
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

        public void EliminarEmpleado(Empleado E)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BajaEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter ci = new SqlParameter("@ci", E.Ci);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(ci);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe el empleado.");
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

        public void ModificarEmpleado(Empleado E)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("ModificarEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter ci = new SqlParameter("@ci", E.Ci);
            SqlParameter contrasena = new SqlParameter("@contrasena", E.Contrasena);
            SqlParameter nomCompleto = new SqlParameter("@nomCompleto", E.Nombre);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            int oAfectados = -1;

            oComando.Parameters.Add(ci);
            oComando.Parameters.Add(contrasena);
            oComando.Parameters.Add(nomCompleto);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El Empleado no existe.");
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

        public Empleado BuscarEmpleadoActivo(string pCi)
        {
            string ci;
            string contrasena;
            string nomCompleto;
            Empleado E = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarEmpleadoActivo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@ci", pCi);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.Read())
                {
                    ci = (string)oReader["ci"];
                    contrasena = (string)oReader["contrasena"];
                    nomCompleto = (string)oReader["nomCompleto"];
                    E = new Empleado(ci, contrasena, nomCompleto);
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
            return E;
        }

        internal Empleado BuscarEmpleadoTodos(string pCi)
        {
            string ci;
            string contrasena;
            string nomCompleto;
            Empleado E = null;
            SqlConnection oConexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand oComando = new SqlCommand("BuscarEmpleadosTodos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            oComando.Parameters.AddWithValue("@ci", pCi);
            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();
                if (oReader.Read())
                {
                    ci = (string)oReader["ci"];
                    contrasena = (string)oReader["contrasena"];
                    nomCompleto = (string)oReader["nomCompleto"];
                    E = new Empleado(ci, contrasena, nomCompleto);
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
            return E;
        }
        /* public List<Empleado> ListarEmpleados()
        {
            string ci;
            string contrasena;
            string nomCompleto;
            List<Empleado> _Lista = new List<Empleado>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion);
            SqlCommand _Comando = new SqlCommand("ListadoEmpleados", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();
                while (_Reader.Read())
                {
                    ci = (string)_Reader["ci"];
                    contrasena = (string)_Reader["contrasena"];
                    nomCompleto = (string)_Reader["nomCompleto"];
                    Empleado E = new Empleado(ci, contrasena, nomCompleto);
                    _Lista.Add(E);
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
        }*/ 
    }
}
