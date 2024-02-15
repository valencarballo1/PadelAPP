using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservaPadel.Controllers
{
    public class ReservasController : Controller
    {
        private ReservasBusiness _ReservasBusiness;

        public ReservasController()
        {
            this._ReservasBusiness = new ReservasBusiness();
        }
        public JsonResult Reservar(int idCancha, DateTime fechaSeleccionada ,string horarioDeReserva, int duracion, int idUsuario)
        {
            bool reservo = _ReservasBusiness.Reservar(idCancha, fechaSeleccionada, horarioDeReserva, duracion, idUsuario);
            return Json(reservo);
        }

        public JsonResult GetReservas()
        {
            List<DTO.ReservaDTO> reservas = _ReservasBusiness.GetReservas();
            return Json(reservas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearPartido(int idUsuario, int idCancha, DateTime fechaSeleccionada, string horarioDeReserva, int duracion, int jugadoresRestantes)
        {
            int idPartidoCreado = _ReservasBusiness.CrearPartido(idUsuario, idCancha, fechaSeleccionada, horarioDeReserva, duracion, jugadoresRestantes);
            return Json(idPartidoCreado);
        }
    }
}