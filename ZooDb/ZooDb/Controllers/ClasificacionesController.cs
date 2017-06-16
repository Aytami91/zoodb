using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooDb.Controllers
{
    public class ClasificacionesController : ApiController
    {
        // GET: api/Clasificaciones
        public RespuestaApi Get()
        {
            RespuestaApi resultado = new RespuestaApi();
            List<Clasificaciones> listaClasificaciones = new List<Clasificaciones>();
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    listaClasificaciones = Db.GET_CLASIFICACION();
                }
                resultado.error = "";
                Db.Desconectar();
            }
            catch
            {
                resultado.error = "Se produjo un error";
            }

            resultado.totalElementos = listaClasificaciones.Count;
            resultado.dataClasificaciones = listaClasificaciones;
            return resultado;
        }

        // GET: api/Clasificaciones/5
        public RespuestaApi Get(long id)
        {
            RespuestaApi resultado = new RespuestaApi();
            List<Clasificaciones> data = new List<Clasificaciones>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {               
                    resultado.error = "";
                }
                Db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }
            resultado.totalElementos = data.Count;
            resultado.dataClasificaciones = data;
            return resultado;

        }

        // POST: api/Clasificaciones
        [HttpPost]
        public IHttpActionResult Post([FromBody]Clasificaciones clasificacion)
        {
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.AgregarClasificacion(clasificacion);
                }


                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = filasAfectadas;
                respuesta.error = "Error al agregar la marca";
            }

            return Ok(clasificacion);
        }
        [HttpPut]
        // PUT: api/Clasificaciones/5
        public void Put([FromBody]Clasificaciones clasificacion)
        {
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    filasAfectadas = Db.AgregarClasificacion(clasificacion);
                }


                Db.Desconectar();
            }
            catch (Exception ex)
            {
                respuesta.totalElementos = filasAfectadas;
                respuesta.error = "Error al agregar la marca";
            }
        }

        [HttpDelete]
        // DELETE: api/Clasificaciones/5
        public void Delete(string borrando)
        {
         
        }
    }
}
