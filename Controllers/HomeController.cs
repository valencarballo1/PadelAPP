using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservaPadel.Controllers
{
    public class HomeController : Controller
    {
        private HorarioBusiness _HorariosBusiness;

        public HomeController()
        {
            this._HorariosBusiness = new HorarioBusiness();
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Reserva()
        {
            return View();
        }

        public ActionResult TurnosReservados()
        {
            return View();
        }

        public ActionResult CrearPartido()
        {
            return View();
        }
    }
}