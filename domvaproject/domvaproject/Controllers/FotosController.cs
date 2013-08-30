using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using domvaproject;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace domvaproject.Controllers
{ 
    public class FotosController : Controller
    {
        private domvaEntities db = new domvaEntities();

        //
        // GET: /Fotos/

        public ViewResult Index()
        {
            var fotos = db.fotos.Include(f => f.propiedades);
            return View(fotos.ToList());
        }

        //
        // GET: /Fotos/Details/5

        public ViewResult Details(int id, int propiedad)
        {
            fotos fotos = db.fotos.Find(id, propiedad);
            return View(fotos);
        }

        //
        // GET: /Fotos/Create

        public ActionResult Create()
        {
            List<propiedades> props = new List<propiedades>();
            props = db.propiedades.ToList();
            ViewBag.Propiedades = new SelectList(props, "idPropiedad", "Nombre");
            return View();
        }


        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input+System.DateTime.Now.Millisecond.ToString()));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //
        // POST: /Fotos/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(fotos fotos)
        {
            int tamW = 220;
            int tamH = 140;
            if (ModelState.IsValid)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    HttpPostedFileBase archivo = Request.Files[0];
                    string md5 = GetMd5Hash(md5Hash, archivo.FileName)+".jpg";
                    fotos.Imagen = md5;
                    var path = Path.Combine(Server.MapPath("~/images/photo"), md5);
                    var pathThumb = Path.Combine(Server.MapPath("~/images/thumbs"), md5);
                    archivo.SaveAs(path);
                    Bitmap bmp = CreateThumbnail(path, tamW, tamH);
                    string OutputFilename = null;
                    OutputFilename = pathThumb;

                    if (OutputFilename != null)
                    {
                        bmp.Save(OutputFilename);
                    }

                    bmp.Save(OutputFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmp.Dispose();

                }
                db.fotos.Add(fotos);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idFoto = new SelectList(db.propiedades, "idPropiedad", "Nombre", fotos.idFoto);
            return View(fotos);
        }
        
        //
        // GET: /Fotos/Edit/5
 
        public ActionResult Edit(int id, int propiedad)
        {
            fotos fotos = db.fotos.Find(id, propiedad);
            ViewBag.idFoto = new SelectList(db.propiedades, "idPropiedad", "Nombre", fotos.idFoto);
            return View(fotos);
        }

        //
        // POST: /Fotos/Edit/5

        [HttpPost]
        public ActionResult Edit(fotos fotos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idFoto = new SelectList(db.propiedades, "idPropiedad", "Nombre", fotos.idFoto);
            return View(fotos);
        }

        //
        // GET: /Fotos/Delete/5

        public ActionResult Delete(int id, int propiedad)
        {
            fotos fotos = db.fotos.Find(id, propiedad);
            return View(fotos);
        }

        //
        // POST: /Fotos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, int propiedad)
        {            
            fotos fotos = db.fotos.Find(id, propiedad);
            db.fotos.Remove(fotos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;


                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }
    }
}