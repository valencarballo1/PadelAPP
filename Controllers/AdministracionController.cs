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
    }
}