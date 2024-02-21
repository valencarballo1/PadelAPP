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
    public class HorarioController : Controller
    {
        private HorarioBusiness _HorariosBusiness;

        public HorarioController()
        {
            this._HorariosBusiness = new HorarioBusiness();
        }
        public JsonResult Load(int idCancha, string fechaSeleccionada)
        {
            List<HorarioDTO> lista = _HorariosBusiness.Load(idCancha, fechaSeleccionada);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}