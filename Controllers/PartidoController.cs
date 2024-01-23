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
    public class PartidoController : Controller
    {
        private PartidoBusiness _PartidoBusiness;

        public PartidoController()
        {
            this._PartidoBusiness = new PartidoBusiness();
        }

        public ActionResult Ver(int idPartido)
        {
            PartidoDTO partido =  _PartidoBusiness.Get(idPartido);
            return View(partido);
        }
    }
}