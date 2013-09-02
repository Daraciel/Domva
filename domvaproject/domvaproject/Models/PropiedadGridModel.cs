using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace domvaproject.Models
{
    public class PropiedadGridModel
    {
        // Data properties
        public IEnumerable<propiedades> Propiedades { get; set; }


        // Sorting-related properties
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public string SortExpression
        {
            get
            {
                return this.SortAscending ? this.SortBy + " asc" : this.SortBy + " desc";
            }
        }
    }
}