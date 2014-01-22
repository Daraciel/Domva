using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using domvaproject;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq.Expressions;
using domvaproject.ViewModels;

namespace domvaproject.Controllers
{ 
    public class PropiedadesController : Controller
    {
        private domvaEntities db = new domvaEntities();
        private propiedades _services = new propiedades();

        //
        // GET: /Propiedades/

        public ViewResult Index()
        {
            return View(db.propiedades.ToList());
        }

        //
        // GET: /Propiedades/Details/5

        public ViewResult Details(int id)
        {
            propiedades propiedades = db.propiedades.Find(id);
            return View(propiedades);
        }

        //
        // GET: /Propiedades/Create

        public ActionResult Create()
        {
            List<propietarios> props = new List<propietarios>();
            propietarios minion = new propietarios();
            minion.idPropietario = 17;
            minion.Nombre = "Sin Especificar";
            props.Add(minion);
            //props = db.propietarios.ToList();
            ViewBag.Propietarios = new SelectList(props,"idPropietario", "Nombre" );
            List<poblaciones> pobls = new List<poblaciones>();
            pobls = db.poblaciones.ToList();
            ViewBag.Poblaciones = new SelectList(pobls, "Nombre","Nombre");
            return View();
        } 

        //
        // POST: /Propiedades/Create

        [HttpPost]
        public ActionResult Create(propiedades propiedades)
        {
            if (ModelState.IsValid)
            {
                db.propiedades.Add(propiedades);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(propiedades);
        }
        
        //
        // GET: /Propiedades/Edit/5
 
        public ActionResult Edit(int id)
        {
            propiedades propiedades = db.propiedades.Find(id);
            List<propietarios> props = new List<propietarios>();
            props = db.propietarios.ToList();
            ViewBag.Propietarios = new SelectList(props, "idPropietario", "Nombre");
            List<poblaciones> pobls = new List<poblaciones>();
            pobls = db.poblaciones.ToList();
            ViewBag.Poblaciones = new SelectList(pobls, "Nombre", "Nombre");
            return View(propiedades);
        }

        //
        // POST: /Propiedades/Edit/5

        [HttpPost]
        public ActionResult Edit(propiedades propiedades)
        {
            if (ModelState.IsValid)
            {
                propiedades.descripcionesesp.Propiedad = propiedades.idPropiedad;
                descripcionesesp desc = propiedades.descripcionesesp;
                propiedades.descripcionesru.Propiedad = propiedades.idPropiedad;
                descripcionesru desc2 = propiedades.descripcionesru;
                propiedades.caracteristicas.Propiedad = propiedades.idPropiedad;
                caracteristicas carac = propiedades.caracteristicas;

                db.Entry(propiedades).State = EntityState.Modified;
                db.Entry(desc).State = EntityState.Modified;
                db.Entry(desc2).State = EntityState.Modified;
                db.Entry(carac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedades);
        }

        //
        // GET: /Propiedades/Delete/5
 
        public ActionResult Delete(int id)
        {
            propiedades propiedades = db.propiedades.Find(id);
            return View(propiedades);
        }

        //
        // POST: /Propiedades/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var fotos = db.fotos.ToList();
            propiedades propiedades = db.propiedades.Find(id);
            db.propiedades.Remove(propiedades);
            db.SaveChanges();
            foreach (fotos foto in fotos)
            {
                if (foto.Propiedad == id)
                {
                    var path = Path.Combine(Server.MapPath("~/images/photo"), foto.Imagen);
                    var pathThumb = Path.Combine(Server.MapPath("~/images/thumbs"), foto.Imagen);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    if (System.IO.File.Exists(pathThumb))
                        System.IO.File.Delete(pathThumb);
                }

            }
            return RedirectToAction("Index");
        }

        public static string principal(int id)
        {
            string resul = "";
            propiedades propiedades = new domvaEntities().propiedades.Find(id);
            
            foreach (fotos f in propiedades.fotos)
            {
                if (f.Principal==true)
                    resul = f.Imagen;
            }

            return resul;
        }

        /*
        la ubicacion
        tipo de vivienda
        precio maximo
        desde m2
        candidad de dormitorios
        candidad de baños
        la distancia hasta el mar
        vistas al mar
        piscina
        terraza
        plaza de garaje/parquing
        ascensor
        aire acondicionado
         */


        // GET: /Products/Filterable?Nombre=string&Localidad=string&precioMin=number&precioMax=number&Piscina=true|false
        public ActionResult Buscar(int page = 1, string sort = "Nombre", string sortDir = "ASC",
                                    string nombre = null, string localidad = null, 
                                    string tipocompra = null, string tipoedif = null, int? precioMin = null,
                                    int? precioMax = null, int? m2Min = null, int? cantDorms = null,
                                    int? cantBanyos = null, int? distMar = null, bool? vistaMar=null,
                                    bool? piscina = null, bool? terraza=null, bool? garage=null,
                                    bool? ascensor=null, bool? aire=null
                                    )
        {
            int PROPIEDADES_POR_PAGINA = 1000;/*
            var numprops = _services.ContarPropiedades(nombre, localidad, precioMin, precioMax, m2Min, 
                                                        cantDorms, cantBanyos, distMar, vistaMar, piscina, 
                                                        terraza, garage, ascensor, aire);*/
            var props = _services.ObtenerPaginaDePersonasFiltrada(page, PROPIEDADES_POR_PAGINA,
                                           sort, sortDir, nombre, localidad, tipocompra, tipoedif, precioMin, precioMax, m2Min,
                                                        cantDorms, cantBanyos, distMar, vistaMar, piscina,
                                                        terraza, garage, ascensor, aire);
            var numprops = props.Count();

            var datos = new PropiedadesFiltro()
            {
                NumeroDePropiedades = numprops,
                PropiedadesPorPagina = PROPIEDADES_POR_PAGINA,
                Propiedades = props,
                Nombre = nombre,
                Localidad = localidad,
                PrecioMin = precioMin.HasValue ? precioMin.Value : 0,
                PrecioMax = precioMax.HasValue ? precioMax.Value : 100000000,
                M2Min = m2Min.HasValue ? distMar.Value : 0,
                CantDorms = cantDorms.HasValue ? cantDorms.Value : 0,
                CantBanyos = cantBanyos.HasValue ? cantBanyos.Value : 0,
                DistMar = distMar.HasValue ? distMar.Value : 10000,
                Piscina = piscina.HasValue ? piscina.Value : false,
                VistaMar = piscina.HasValue ? piscina.Value : false,
                Terraza = vistaMar.HasValue ? vistaMar.Value : false,
                Garage = terraza.HasValue ? terraza.Value : false,
                Ascensor = ascensor.HasValue ? ascensor.Value : false,
                Aire = aire.HasValue ? aire.Value : false,
                PaginaActual = page,
                
            };

            return View(datos);
        }


        public int ContarPropiedades()
        {
            return db.propiedades.Count();
        }
        /*
        public IEnumerable<propiedades> ObtenerPaginaDePersonas<T>( int paginaActual, int personasPorPagina, Expression<Func<propiedades, T>> ordenacion, Direccion direccion)
        {
            if (paginaActual < 1) paginaActual = 1;
            IQueryable<propiedades> query = db.propiedades;
            if (direccion == Direccion.Ascendente)
                query = query.OrderBy(ordenacion);
            else
                query = query.OrderByDescending(ordenacion);
            return query.Skip((paginaActual - 1) * personasPorPagina)
                        .Take(personasPorPagina)
                        .ToList();
        }*/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}