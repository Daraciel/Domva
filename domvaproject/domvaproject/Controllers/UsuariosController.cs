using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using domvaproject;
using System.Data.Objects.SqlClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

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

        public ActionResult Login()
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
        public ActionResult Login(usuarios usu)
        {
            if (ModelState.IsValid)
            {
                var query2 = db.usuarios.Where(u => u.Nombre == usu.Nombre & u.Pass == "password('"+usu.Pass+"')" );
                Type t = typeof(int);
                var fun = db.Database.SqlQuery<int>("SELECT Login(@param1,@param2)", 
                    new MySqlParameter("@param1", usu.Nombre), 
                    new MySqlParameter("@param2", usu.Pass));
                //return query.FirstOrDefault();
                if(fun.First()==1)
                    return RedirectToAction("Create");  
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