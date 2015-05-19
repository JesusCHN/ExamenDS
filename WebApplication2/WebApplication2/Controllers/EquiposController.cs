using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class EquiposController : Controller
    {
        private ExamenDSEntities db = new ExamenDSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                var lista = db.Equipo.ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllTecnico()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                var lista = db.Tecnico.ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult Create(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipo.Add(equipo);
                db.SaveChanges();
                return Json(equipo, JsonRequestBehavior.AllowGet);
            }

            return Json(equipo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Update(Equipo equipo)
        {
            if (!ModelState.IsValid)
                return Json("Actualizado correctarmente", JsonRequestBehavior.AllowGet);

            db.Entry(equipo).State = EntityState.Modified;

            db.SaveChanges();

            return Json(equipo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                if (id == null)
                    return Json("El registro no es valido.", JsonRequestBehavior.AllowGet);

                var nId = int.Parse(id);
                var carrera = db.Equipo.FirstOrDefault(x => x.IdEquipo == nId);

                if (carrera == null)
                    return Json("No se encontro ningun registro.", JsonRequestBehavior.AllowGet);

                db.Equipo.Remove(carrera);
                db.SaveChanges();

                return Json("Eliminado correctarmente", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error:" + ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
