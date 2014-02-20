using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace domvaproject.ViewModels
{
    public class PropiedadesFiltro
    {
        private readonly domvaEntities _datos = new domvaEntities();

        public int NumeroDePropiedades { get; set; }
        public IEnumerable<propiedades> Propiedades { get; set; }
        public int PropiedadesPorPagina { get; set; }

        public string Localidad { get; set; }
        public string Nombre { get; set; }
        public int PrecioMin { get; set; }
        public int PrecioMax { get; set; }
        public int M2Min { get; set; } 
        public int CantDorms { get; set; }
        public int CantBanyos { get; set; }
        public int DistMar { get; set; }
        public bool Piscina { get; set; }
        public bool VistaMar { get; set; }
        public bool Terraza { get; set; }
        public bool Garage { get; set; }
        public bool Ascensor { get; set; }
        public bool Aire { get; set; }


        public string TipoCompra { get; set; }
        public string TipoEdif { get; set; }


        public int PaginaActual { get; set; }
        public IEnumerable<SelectListItem> GetPropiedades()
        {
            IEnumerable<SelectListItem> lista = _datos.poblaciones.Select(pob => new SelectListItem { Value = pob.Nombre, Text = pob.Nombre });

            return lista.Where(loc => loc.Text != "Otros");
        }
        public int CantPaginas
        {
            get
            {
                return this.NumeroDePropiedades / this.PropiedadesPorPagina;
            }

        }
        public int NumericPageCount { get; set; }

        public IEnumerable ListaLocalidades { get; set; }
    }
}