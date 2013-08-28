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
    public class CaracteristicasController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /Caracteristicas/

        public ViewResult Index()
        {
            var caracteristicas = db.caracteristicas.Include(c => c.propiedades);
            return View(caracteristicas.ToList());
        }

        //
        // GET: /Caracteristicas/Details/5

        public ViewResult Details(int id)
        {
            caracteristicas caracteristicas = db.caracteristicas.Find(id);
            return View(caracteristicas);
        }

        //
        // GET: /Caracteristicas/Create

        public ActionResult Create()
        {
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre");
            return View();
        } 

        //
        // POST: /Caracteristicas/Create

        [HttpPost]
        public ActionResult Create(caracteristicas caracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.caracteristicas.Add(caracteristicas);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", caracteristicas.Propiedad);
            return View(caracteristicas);
        }
        
        //
        // GET: /Caracteristicas/Edit/5
 
        public ActionResult Edit(int id)
        {
            caracteristicas caracteristicas = db.caracteristicas.Find(id);
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", caracteristicas.Propiedad);
            return View(caracteristicas);
        }

        //
        // POST: /Caracteristicas/Edit/5

        [HttpPost]
        public ActionResult Edit(caracteristicas caracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caracteristicas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Propiedad = new SelectList(db.propiedades, "idPropiedad", "Nombre", caracteristicas.Propiedad);
            return View(caracteristicas);
        }

        //
        // GET: /Caracteristicas/Delete/5
 
        public ActionResult Delete(int id)
        {
            caracteristicas caracteristicas = db.caracteristicas.Find(id);
            return View(caracteristicas);
        }

        //
        // POST: /Caracteristicas/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            caracteristicas caracteristicas = db.caracteristicas.Find(id);
            db.caracteristicas.Remove(caracteristicas);
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