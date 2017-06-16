using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooDb
{
    public class RespuestaApi
    {
        public int totalElementos { get; set; }

        public string error { get; set; }

        public List<TiposAnimal> dataTiposAnimal { get; set; }

        public List<Clasificaciones> dataClasificaciones { get; set; }

        public List<Especies> dataEspecies { get; set; }
    }
}