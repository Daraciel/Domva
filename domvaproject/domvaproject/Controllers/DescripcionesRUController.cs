using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using domvaproject;

namespace domvaproject.Controllers
{ 
    public class DescripcionesRUController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /DescripcionesRU/

        public ViewResult Index()
        {
            var descripcionesru = db.descripcionesru.Include(d => d.propiedades);
            return View(descripcionesru.ToList());
        }

        //
        // GET: /DescripcionesRU/Details/5

        public ViewResult Details(int id)
        {
            descripcionesru descripcionesru = db.descripcionesru.Find(id);
            return View(descripcionesru);
        }

        //
        // GET: /DescripcionesRU/Create

        public ActionResult Create()
        {
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre");
            return View();
        } 

        //
        // POST: /DescripcionesRU/Create

        [HttpPost]
        public ActionResult Create(descripcionesru descripcionesru)
        {
            if (ModelState.IsValid)
            {
                db.descripcionesru.Add(descripcionesru);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesru.Propiedad);
            return View(descripcionesru);
        }
        
        //
        // GET: /DescripcionesRU/Edit/5
 
        public ActionResult Edit(int id)
        {
            descripcionesru descripcionesru = db.descripcionesru.Find(id);
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesru.Propiedad);
            return View(descripcionesru);
        }

        //
        // POST: /DescripcionesRU/Edit/5

        [HttpPost]
        public ActionResult Edit(descripcionesru descripcionesru)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descripcionesru).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesru.Propiedad);
            return View(descripcionesru);
        }

        //
        // GET: /DescripcionesRU/Delete/5
 
        public ActionResult Delete(int id)
        {
            descripcionesru descripcionesru = db.descripcionesru.Find(id);
            return View(descripcionesru);
        }

        //
        // POST: /DescripcionesRU/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            descripcionesru descripcionesru = db.descripcionesru.Find(id);
            db.descripcionesru.Remove(descripcionesru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}