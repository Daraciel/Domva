﻿using System;
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
    }
}