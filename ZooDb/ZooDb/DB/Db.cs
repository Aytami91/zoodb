using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZooDb
{
    public class Db
    {
        private static SqlConnection conexion = null;

        public static void Conectar()
        {
            try
            {
                // PREPARO LA CADENA DE CONEXIÓN A LA BD
                string cadenaConexion = @"Server=.\sqlexpress;
                                          Database=zooDb;
                                          User Id=userzoo;
                                          Password=!Curso@2017;";

                // CREO LA CONEXIÓN
                conexion = new SqlConnection();
                conexion.ConnectionString = cadenaConexion;

                // TRATO DE ABRIR LA CONEXION
                conexion.Open();

                //// PREGUNTO POR EL ESTADO DE LA CONEXIÓN
                //if (conexion.State == ConnectionState.Open)
                //{
                Console.WriteLine("Conexión abierta con éxito");
                //    // CIERRO LA CONEXIÓN
                //    conexion.Close();
                //}
            }
            catch (Exception)
            {
                if (conexion != null)
                {
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    conexion.Dispose();
                    conexion = null;
                }
            }
            finally
            {
                // DESTRUYO LA CONEXIÓN
                //if (conexion != null)
                //{
                //    if (conexion.State != ConnectionState.Closed)
                //    {
                //        conexion.Close();
                Console.WriteLine("Conexión cerrada con éxito");
                //    }
                //    conexion.Dispose();
                //    conexion = null;
                //}
            }
        }

        public static bool EstaLaConexionAbierta()
        {
            return conexion.State == ConnectionState.Open;
        }

        public static void Desconectar()
        {
            if (conexion != null)
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }
        
        //TiposAnimal
        public static List<TiposAnimal> GET_TIPO_ANIMAL()
        {
            List<TiposAnimal> respuesta = new List<TiposAnimal>();
            string procedimiento = "dbo.GET_TIPO_ANIMAL";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                TiposAnimal tiposAnimal = new TiposAnimal();
                tiposAnimal.idTipoAnimal = (long)reader["idTipoAnimal"];
                tiposAnimal.denominacion = reader["denominacion"].ToString();
                respuesta.Add(tiposAnimal);
            }
            return respuesta;

        }

        public static void AgregarTipoAnimal(TiposAnimal animal)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"INSERT INTO TiposAnimal (
                   idTipoAnimal, denominacion)
                                         VALUES (";
            consultaSQL += "'" + animal.idTipoAnimal + "'";
            consultaSQL += ",'" + animal.denominacion + "'";
            consultaSQL += ");";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            comando.ExecuteNonQuery();
        }

        public static void ActualizarTiposAnimal(TiposAnimal animal)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"INSERT INTO Clasificaciones (
                    idClasificaciones, denominacion)
                                         VALUES (";
            consultaSQL += "'" + animal.idTipoAnimal + "'";
            consultaSQL += ",'" + animal.denominacion + "'";
            consultaSQL += ");";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            comando.ExecuteNonQuery();
        }

        public static void EliminarTiposAnimal(string animal)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"DELETE FROM Clasificacion 
                                   WHERE idClasificacion = '" + animal + "';";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // EJECUTO EL COMANDO
            comando.ExecuteNonQuery();
        }

        //CLASIFICACION
        public static List<Clasificaciones> GET_CLASIFICACION()
        {
            List<Clasificaciones> respuesta = new List<Clasificaciones>();
            string procedimiento = "dbo.GET_CLASIFICACION";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Clasificaciones clasificacion = new Clasificaciones();
                clasificacion.idClasificacion = (long)reader["idClasificacion"];
                clasificacion.denominacion = reader["denominacion"].ToString();
                respuesta.Add(clasificacion);


            }
            return respuesta;
        }

        public static List<Clasificaciones> GET_CLASIFICACION_TIPO_ANIMAL()
        {
            List<Clasificaciones> resultados = new List<Clasificaciones>();
            string procedimientoAEjecutar = "dbo.GET_CLASIFICACION_TIPO_ANIMAL";

            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Clasificaciones clasificacion = new Clasificaciones();
                clasificacion.idClasificacion = (long)reader["idClasificacion"];
                clasificacion.denominacion = reader["denominacion"].ToString();
                TiposAnimal tipoAnimal = new TiposAnimal();
                tipoAnimal.idTipoAnimal = (long)reader["idTipoAnimal"];
                tipoAnimal.denominacion = reader["denominacion"].ToString();
            }
            return resultados;
        }

        public static void ActualizarClasificacion(Clasificaciones clasificacion)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"INSERT INTO Clasificaciones (
                    idClasificaciones, denominacion)
                                         VALUES (";
            consultaSQL += "'" + clasificacion.idClasificacion + "'";
            consultaSQL += ",'" + clasificacion.denominacion + "'";
            consultaSQL += ");";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            comando.ExecuteNonQuery();
        }

        public static int AgregarClasificacion(Clasificaciones clasificacion)
        {
            string procedimiento = "dbo.AgregarClasificacion";

            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "denominacion";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = clasificacion.denominacion;

            comando.Parameters.Add(parametro);
            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;
        }

        public static void EliminarClasificacion(string clasificacion)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"DELETE FROM Clasificacion 
                                   WHERE idClasificacion = '" + clasificacion + "';";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // EJECUTO EL COMANDO
            comando.ExecuteNonQuery();
        }

        //ESPECIES
        public static List<Especies> GET_ESPECIES()
        {
            List<Especies> respuesta = new List<Especies>();
            string procedimiento = "dbo.GET_ESPECIES";
            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Especies especie = new Especies();
                especie.idEspecies = (long)reader["idEspecies"];
                especie.idClasificacion = (int)reader["idClasificacion"];
                especie.idTipoAnimal = (long)reader["idTipoAnimal"];
                especie.nombre = reader["nombre"].ToString();
                especie.nPatas = (int)reader["nPatas"];
                especie.esMascota = (bool)reader["esMascota"];
                respuesta.Add(especie);

            }
            return respuesta;
        }

        public static List<Especies> GET_ESPECIE_CLASIFICACION()
        {

            List<Especies> resultados = new List<Especies>();

            string procedimientoAEjecutar = "dbo.GET_ESPECIE_CLASIFICACION";

            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader reader = comando.ExecuteReader();
            
            while (reader.Read())
            {
              
                Especies especie = new Especies();
                especie.idEspecies = (long)reader["idEspecies"];
                especie.idClasificacion = (int)reader["idClasificacion"];
                especie.idTipoAnimal = (long)reader["idTipoAnimal"];
                especie.nombre = reader["nombre"].ToString();
                especie.nPatas = (int)reader["nPatas"];
                especie.esMascota = (bool)reader["esMascota"];
                Clasificaciones clasificacion = new Clasificaciones();
                clasificacion.idClasificacion = (int)reader["idClasificacion"];
                clasificacion.denominacion = reader["Clasificacion"].ToString();
               
                resultados.Add(especie);
            }

            return resultados;
        }

        public List<Especies> GET_ESPECIE_TIPO_ANIMAL()
        {
            
            List<Especies> resultados = new List<Especies>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_ESPECIE_TIPO_ANIMAL";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                // CREO EL COCHE
                Especies especie = new Especies();
                especie.idEspecies = (long)reader["idEspecies"];
                especie.idTipoAnimal = (long)reader["idTipoAnimal"];
                TiposAnimal idTipoAnimal = new TiposAnimal();
                idTipoAnimal.denominacion = reader["Marca"].ToString();
                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(especie);
            }

            return resultados;
        }

        public static void ActualizarEspecie(Especies especie)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"INSERT INTO Especies (
                    idEspecies,idTipoAnimal,idClasificacion,nombre,nPatas
                    ,esMascota)
                                         VALUES (";
            consultaSQL += "'" + especie.idEspecies + "'";
            consultaSQL += ",'" + especie.idTipoAnimal + "'";
            consultaSQL += ",'" + especie.idClasificacion + "'";
            consultaSQL += ",'" + especie.nombre + "'";
            consultaSQL += ",'" + especie.nPatas + "'";
            consultaSQL += ",'" + especie.esMascota + "'";            
            consultaSQL += ");";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            comando.ExecuteNonQuery();
        }

        public static void EliminarEspecie(string especie)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"DELETE FROM Especies 
                                   WHERE idEspecies = '" + especie + "';";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // EJECUTO EL COMANDO
            comando.ExecuteNonQuery();
        }

        public static int AgregarEspecie(Especies especie)
        {
            string procedimiento = "dbo.AgregarEspecie";

            SqlCommand comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "idEspecies";
            parametro.SqlDbType = SqlDbType.NVarChar;
            parametro.SqlValue = especie.idEspecies;

            comando.Parameters.Add(parametro);
            int filasAfectadas = comando.ExecuteNonQuery();


            return filasAfectadas;
        }
    }       
}   
