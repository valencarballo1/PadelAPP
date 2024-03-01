using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Data.DTO;

namespace ReservaPadel.Controllers
{
    public class AdministracionController : Controller
    {
        private ReservasBusiness _ReservasBusiness;
        private UsuarioBusiness _UsuarioBusiness;

        public AdministracionController()
        {
            this._ReservasBusiness = new ReservasBusiness();
            this._UsuarioBusiness = new UsuarioBusiness();
        }
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["UsuarioSesion"];
            if (cookie != null)
            {
                string idUsuario = cookie["Id"];
                bool esAdmin = _UsuarioBusiness.EsAdmin(int.Parse(idUsuario));
                if (esAdmin)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Panel()
        {
            return View();
        }

        public ActionResult Reservas()
        {
            HttpCookie cookie = Request.Cookies["UsuarioSesion"];
            if (cookie != null)
            {
                string idUsuario = cookie["Id"];
                bool esAdmin = _UsuarioBusiness.EsAdmin(int.Parse(idUsuario));
                if (esAdmin)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult GetReservasAdmin(DateTime fecha)
        {
            List<ReservaDTO> reservas = _ReservasBusiness.GetReservasAdmin(fecha);
            return Json(reservas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FinalizarTurno(int idCancha, decimal importeCancha, decimal adicional)
        {
            bool finalizo = _ReservasBusiness.FinalizarTurno(idCancha, importeCancha, adicional);
            return Json(finalizo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DarDeBaja(int idCancha)
        {
            bool finalizo = _ReservasBusiness.DarDeBaja(idCancha);
            return Json(finalizo, JsonRequestBehavior.AllowGet);
        }
    }
}