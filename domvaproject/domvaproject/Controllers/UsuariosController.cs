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
    public class UsuariosController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /Usuarios/

        public ViewResult Index()
        {
            return View(db.usuarios.ToList());
        }

        //
        // GET: /Usuarios/Details/5

        public ViewResult Details(string id)
        {
            usuarios usuarios = db.usuarios.Find(id);
            return View(usuarios);
        }

        //
        // GET: /Usuarios/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Usuarios/Create

        [HttpPost]
        public ActionResult Create(usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usuarios);
        }
        [HttpPost]
        public ActionResult Login(string Nombre, string Contra)
        {
            if (ModelState.IsValid)
            {
                usuarios usuario = db.usuarios.Find(Nombre, Contra);
                if(usuario!=null)
                    return RedirectToAction("Index"); 
                else
                    return RedirectToAction("Index"); 
            }
                return RedirectToAction("Index"); 

        }
        
        //
        // GET: /Usuarios/Edit/5
 
        public ActionResult Edit(string id)
        {
            usuarios usuarios = db.usuarios.Find(id);
            return View(usuarios);
        }

        //
        // POST: /Usuarios/Edit/5

        [HttpPost]
        public ActionResult Edit(usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        //
        // GET: /Usuarios/Delete/5
 
        public ActionResult Delete(string id)
        {
            usuarios usuarios = db.usuarios.Find(id);
            return View(usuarios);
        }

        //
        // POST: /Usuarios/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            usuarios usuarios = db.usuarios.Find(id);
            db.usuarios.Remove(usuarios);
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