using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooDb
{
    public class Especies
    {
        public long idEspecies { get; set; }

        public int idClasificacion { get; set; }

        public long idTipoAnimal { get; set; }

        public string nombre { get; set; }

        public int nPatas { get; set; }

        public bool esMascota { get; set; }


    }
}