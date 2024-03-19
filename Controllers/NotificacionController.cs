using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservaPadel.Controllers
{
    public class NotificacionController : Controller
    {
        private NotificacionBusiness _NotificacionBusiness;

        public NotificacionController()
        {
            this._NotificacionBusiness = new NotificacionBusiness();
        }
        public JsonResult Notificaciones()
        {
            HttpCookie cookie = Request.Cookies["UsuarioSesion"];
            if (cookie != null)
            {
                string idUsuario = cookie["Id"];
                List<Notificaciones> lista = _NotificacionBusiness.GetAll(int.Parse(idUsuario));
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public bool CrearNotificacion(int tipoNotificacion, string detalle)
        {
            return _NotificacionBusiness.CrearNotificacion(tipoNotificacion, detalle);
        }
    }
}