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
using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace domvaproject.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        private domvaEntities db = new domvaEntities();
        private propiedades _services = new propiedades();

        public ActionResult Index()
        {
            //return View(db.propiedades.Where(p => p.Destacada == true));
            return View();
        }

        public ActionResult Propiedad(int id)
        {
            propiedades propiedades = db.propiedades.Find(id);
            return View(propiedades);
        }

        public static string principal(int id)
        {
            string resul = "";
            propiedades propiedades = new domvaEntities().propiedades.Find(id);

            foreach (fotos f in propiedades.fotos)
            {
                if (f.Principal == true)
                    resul = f.Imagen;
            }

            return resul;
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        public ActionResult Contactar(string Casa = "CASA", string Name = "Name", string Surname = "Surname", string Email = "Email", string Tlf = "Email")
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            //set the FROM address
            mail.From = new MailAddress("contacto@domvalicante.com");
            //set the RECIPIENTS
            mail.To.Add("yulia@domvalicante.com");
            mail.CC.Add("soporte@domvalicante.com");
            //enter a SUBJECT
            mail.Subject = "[NOTIFICACION DOMVALICANTE] Alguien se ha interesado por la casa "+Casa;
            //Enter the message BODY
            mail.Body = "Datos del interesado:       Nombre: " + Name + " Apellidos: " + Surname + " Email: " + Email + " Telefono: " + Tlf;
            //set the mail server (default should be auth.smtp.1and1.co.uk)
            SmtpClient smtp = new SmtpClient("auth.smtp.1and1.co.uk");
            //Enter your full e-mail address and password
            smtp.Credentials = new NetworkCredential("contacto@domvalicante.com", "A1b2C3d4");
            //send the message 
            smtp.Send(mail);
            /*
            MailMessage mail = new MailMessage();
            //mail.To = "yulia@domvalicante.com";
            mail.To = "soporte@domvalicante.com";
            mail.From = "contacto@domvalicante.com";
            //mail.Cc = "soporte@domvalicante.com";
            mail.Priority = MailPriority.Normal;
            mail.Subject = "Un cliente se ha interesado por la casa";
            mail.Body = "Nombre: " + Nombre + "       Apellidos: " + Apellidos + " Email: " + Email;
            
            SmtpMail.SmtpServer = "smtp.1and1.com";
            //Enter your full e-mail address and password
            //send the message 
            SmtpMail.Send(mail);
            */
            return View();
        }

        public ActionResult Buscar(int page = 1, string sort = "Nombre", string sortDir = "ASC",
                            string nombre = null, string localidad = null, string tipocompra = null, 
                            string tipoedif = null, int? precioMin = null,
                            int? precioMax = null, int? m2Min = null, int? cantDorms = null,
                            int? cantBanyos = null, int? distMar = null, bool? vistaMar = null,
                            bool? piscina = null, bool? terraza = null, bool? garage = null,
                            bool? ascensor = null, bool? aire = null
                            )
        {
            int PROPIEDADES_POR_PAGINA = int.MaxValue;/*
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
                ListaLocalidades = (IQueryable<SelectListItem>)db.poblaciones.Select(pob => new SelectListItem{Value = pob.Nombre, Text = pob.Nombre}) ,
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


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
