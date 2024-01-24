using Business;
using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
            PartidoDTO partido = _PartidoBusiness.Get(idPartido);

            string rutaDirectorio = Url.Content("~/" + "imgPerfiles/");

            string nombreArchivo;
            string rutaCompleta;

            if (partido.UsuarioJ1 != null)
            {
                nombreArchivo = partido.FotoPerfil1;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil1 = rutaCompleta;
            }

            if (!partido.UsuarioJ2.IsEmpty() && partido.UsuarioJ2 != partido.UsuarioJ1)
            {
                nombreArchivo = partido.FotoPerfil2;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil2 = rutaCompleta;
            }
            else if (partido.UsuarioJ2 == "")
            {
                nombreArchivo = "AgregarAPartido.jpeg";
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil2 = rutaCompleta;
            }
            else if (partido.UsuarioJ1 == partido.UsuarioJ2)
            {
                partido.UsuarioJ2 = "Invitado de " + partido.UsuarioJ1;
                nombreArchivo = partido.FotoPerfil2;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil2 = rutaCompleta;
            }

            if (!partido.UsuarioJ3.IsEmpty() && partido.UsuarioJ3 != partido.UsuarioJ1 && partido.UsuarioJ3 != partido.UsuarioJ2)
            {
                nombreArchivo = partido.FotoPerfil3;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil3 = rutaCompleta;
            }
            else if (partido.UsuarioJ3 == "")
            {
                nombreArchivo = "AgregarAPartido.jpeg";
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil3 = rutaCompleta;
            }
            else if (partido.UsuarioJ3 == partido.UsuarioJ1)
            {
                partido.UsuarioJ3 = "Invitado de: " + partido.UsuarioJ1;
                nombreArchivo = partido.FotoPerfil1;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil3 = rutaCompleta;
            }
            else if (partido.UsuarioJ3 == partido.UsuarioJ2)
            {
                partido.UsuarioJ3 = "Invitado de: " + partido.UsuarioJ2;
                nombreArchivo = partido.FotoPerfil2;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil3 = rutaCompleta;
            }


            if (!partido.UsuarioJ4.IsEmpty())
            {
                nombreArchivo = partido.FotoPerfil4;
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil4 = rutaCompleta;
            }
            else if (partido.UsuarioJ4 == "")
            {
                nombreArchivo = "AgregarAPartido.jpeg";
                rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
                partido.FotoPerfil4 = rutaCompleta;
            }

            return View(partido);
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Disponibles()
        {
            return View();
        }

        public JsonResult Unirse(int idPartido, int idUsuario, int posicionJugador)
        {
            bool seUnio = _PartidoBusiness.Unirse(idPartido, idUsuario, posicionJugador);

            return Json(seUnio);
        }

        public JsonResult GetDetallesPartidos()
        {
            List<DetallePartidoDTO> detalles = _PartidoBusiness.GetDetallePartidos();
            return Json(detalles, JsonRequestBehavior.AllowGet);
        }
    }
}