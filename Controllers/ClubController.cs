using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Data.DTO;

namespace ReservaPadel.Controllers
{
    public class ClubController : Controller
    {
        private ClubBusiness _ClubBusiness;

        public ClubController()
        {
            this._ClubBusiness = new ClubBusiness();
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            string rutaDirectorio = Url.Content("~/" + "club/");
            string nombreArchivo;
            string rutaCompleta;

            List<ClubDTO> clubes = _ClubBusiness.GetAll();

            clubes.ForEach(c =>
            {
                nombreArchivo = c.Image;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                c.Image = rutaCompleta;
            });

            return Json(clubes, JsonRequestBehavior.AllowGet);
        }
    }
}