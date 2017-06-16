using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZooDb.Controllers
{
    public class EspeciesController : ApiController
    {
        // GET: api/Especies
        public RespuestaApi Get()
        {
            RespuestaApi respuesta = new RespuestaApi();
            List<Especies> listaEspecies = new List<Especies>();
            try
            {
                Db.Conectar();

                if (Db.EstaLaConexionAbierta())
                {
                    listaEspecies = Db.GET_ESPECIES();
                    respuesta.error = "";

                }
            }
            catch (Exception ex)
            {

                respuesta.error = "Error";
            }
            respuesta.totalElementos = listaEspecies.Count();
            respuesta.dataEspecies = listaEspecies;
            return respuesta;
        }

        // GET: api/Especies/5
        public RespuestaApi Get(long id)
        {
            RespuestaApi resultado = new RespuestaApi();
            List<Especies> data = new List<Especies>();
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
            resultado.dataEspecies = data;
            return resultado;

        }

        // POST: api/Especies
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Especies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Especies/5
        public void Delete(int id)
        {
        }
    }
}
