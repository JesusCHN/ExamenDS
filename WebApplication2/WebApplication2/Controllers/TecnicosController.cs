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
    public class TecnicosController : Controller
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

                var lista = db.Tecnico.ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create(Tecnico Tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Tecnico.Add(Tecnico);
                db.SaveChanges();
                return Json(Tecnico, JsonRequestBehavior.AllowGet);
            }

            return Json(Tecnico, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Update(Tecnico Tecnico)
        {
            if (!ModelState.IsValid)
                return Json("Actualizado correctarmente", JsonRequestBehavior.AllowGet);

            db.Entry(Tecnico).State = EntityState.Modified;

            db.SaveChanges();

            return Json(Tecnico, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                if (id == null)
                    return Json("El registro no es valido.", JsonRequestBehavior.AllowGet);

                var nId = int.Parse(id);
                var tecnico = db.Tecnico.FirstOrDefault(x => x.IdTecnico == nId);

                if (tecnico == null)
                    return Json("No se encontro ningun registro.", JsonRequestBehavior.AllowGet);

                db.Tecnico.Remove(tecnico);
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
