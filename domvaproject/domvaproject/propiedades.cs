//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace domvaproject
{
    using System;
    using System.Collections.Generic;
    
    public partial class propiedades
    {
        public propiedades()
        {
            this.fotos = new HashSet<fotos>();
        }
    
        public int idPropiedad { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Precio { get; set; }
        public Nullable<int> Propietario { get; set; }
        public string DescripcionInterna { get; set; }
        public string Categoria { get; set; }
    
        public virtual caracteristicas caracteristicas { get; set; }
        public virtual descripcionesesp descripcionesesp { get; set; }
        public virtual descripcionesru descripcionesru { get; set; }
        public virtual ICollection<fotos> fotos { get; set; }
        public virtual propietarios propietarios { get; set; }
    }
}