using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace domvaproject.ViewModels
{
    public class PropiedadesFiltro
    {
        public int NumeroDePropiedades { get; set; }
        public IEnumerable<propiedades> Propiedades { get; set; }
        public int PropiedadesPorPagina { get; set; }

        public string Localidad { get; set; }
        public string Nombre { get; set; }
        public int PrecioMin { get; set; }
        public decimal PrecioMax { get; set; }
        public bool Piscina { get; set; }

        public IEnumerable ListaLocalidades { get; set; }
    }
}