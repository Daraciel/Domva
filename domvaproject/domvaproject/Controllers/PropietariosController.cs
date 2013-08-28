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
    public class PropietariosController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /Propietarios/

        public ViewResult Index()
        {
            return View(db.propietarios.ToList());
        }

        //
        // GET: /Propietarios/Details/5

        public ViewResult Details(int id)
        {
            propietarios propietarios = db.propietarios.Find(id);
            return View(propietarios);
        }

        //
        // GET: /Propietarios/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Propietarios/Create

        [HttpPost]
        public ActionResult Create(propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                db.propietarios.Add(propietarios);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(propietarios);
        }
        
        //
        // GET: /Propietarios/Edit/5
 
        public ActionResult Edit(int id)
        {
            propietarios propietarios = db.propietarios.Find(id);
            return View(propietarios);
        }

        //
        // POST: /Propietarios/Edit/5

        [HttpPost]
        public ActionResult Edit(propietarios propietarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propietarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propietarios);
        }

        //
        // GET: /Propietarios/Delete/5
 
        public ActionResult Delete(int id)
        {
            propietarios propietarios = db.propietarios.Find(id);
            return View(propietarios);
        }

        //
        // POST: /Propietarios/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            propietarios propietarios = db.propietarios.Find(id);
            db.propietarios.Remove(propietarios);
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