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

namespace domvaproject.Controllers
{ 
    public class PropiedadesController : Controller
    {
        private domvaEntities db = new domvaEntities();

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
            props = db.propietarios.ToList();
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}