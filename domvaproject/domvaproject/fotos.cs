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
    
    public partial class fotos
    {
        public int idFoto { get; set; }
        public string Imagen { get; set; }
        public int Propiedad { get; set; }
        public Nullable<bool> Principal { get; set; }
    
        public virtual propiedades propiedades { get; set; }
    }
}