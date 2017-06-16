using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooDb.Controllers
{
    public class TiposAnimalController : ApiController
    {
        // GET: api/TiposAnimal
        public RespuestaApi Get()
        {
            RespuestaApi respuesta = new RespuestaApi();
            List<TiposAnimal> data = new List<TiposAnimal>();
            try
            {
                Db.Conectar();
                if (Db.EstaLaConexionAbierta())
                {
                    data = Db.GET_TIPO_ANIMAL();
                }
                respuesta.error = "";
                Db.Desconectar();
            }
            catch
            {
                respuesta.totalElementos = 0;
                respuesta.error = "Se produjo un error";
            }

            respuesta.totalElementos = data.Count;
            respuesta.dataTiposAnimal = data;
            return respuesta;
        }

        // GET: api/TiposAnimal/5
        public RespuestaApi Get(long id)
        {
            RespuestaApi resultado = new RespuestaApi();
            List<TiposAnimal> data = new List<TiposAnimal>();
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
            resultado.dataTiposAnimal = data;
            return resultado;

        }

        // POST: api/TiposAnimal
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TiposAnimal/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TiposAnimal/5
        public void Delete(int id)
        {
        }
    }
}
