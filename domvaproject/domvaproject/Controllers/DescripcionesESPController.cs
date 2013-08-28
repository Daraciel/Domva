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
    public class DescripcionesESPController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /DescripcionesESP/

        public ViewResult Index()
        {
            var descripcionesesp = db.descripcionesesp.Include(d => d.propiedades);
            return View(descripcionesesp.ToList());
        }

        //
        // GET: /DescripcionesESP/Details/5

        public ViewResult Details(int id)
        {
            descripcionesesp descripcionesesp = db.descripcionesesp.Find(id);
            return View(descripcionesesp);
        }

        //
        // GET: /DescripcionesESP/Create

        public ActionResult Create()
        {
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre");
            return View();
        } 

        //
        // POST: /DescripcionesESP/Create

        [HttpPost]
        public ActionResult Create(descripcionesesp descripcionesesp)
        {
            if (ModelState.IsValid)
            {
                db.descripcionesesp.Add(descripcionesesp);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesesp.Propiedad);
            return View(descripcionesesp);
        }
        
        //
        // GET: /DescripcionesESP/Edit/5
 
        public ActionResult Edit(int id)
        {
            descripcionesesp descripcionesesp = db.descripcionesesp.Find(id);
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesesp.Propiedad);
            return View(descripcionesesp);
        }

        //
        // POST: /DescripcionesESP/Edit/5

        [HttpPost]
        public ActionResult Edit(descripcionesesp descripcionesesp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descripcionesesp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", descripcionesesp.Propiedad);
            return View(descripcionesesp);
        }

        //
        // GET: /DescripcionesESP/Delete/5
 
        public ActionResult Delete(int id)
        {
            descripcionesesp descripcionesesp = db.descripcionesesp.Find(id);
            return View(descripcionesesp);
        }

        //
        // POST: /DescripcionesESP/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            descripcionesesp descripcionesesp = db.descripcionesesp.Find(id);
            db.descripcionesesp.Remove(descripcionesesp);
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