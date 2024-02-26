using Business;
using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Data.DTO;

namespace ReservaPadel.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;

        public UsuarioController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
        }
        public ActionResult InicioSesion()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Registrarme(string nombre, string apellido, string celular, string usuario, string contrasena, int categoriaId, HttpPostedFileBase fotoPerfil)
        {
            int id = 0;
            if (fotoPerfil != null && fotoPerfil.ContentLength > 0)
            {
                id = _UsuarioBusiness.Registrarme(nombre, apellido, celular, usuario, contrasena, categoriaId, Path.GetExtension(fotoPerfil.FileName));
                string rutaDirectorio = Server.MapPath("~/imgPerfiles/");

                if (!Directory.Exists(rutaDirectorio))
                {
                    Directory.CreateDirectory(rutaDirectorio);
                }

                string nombreArchivo = usuario + Path.GetExtension(fotoPerfil.FileName);
                string rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);

                fotoPerfil.SaveAs(rutaCompleta);
            }
            else
            {
                id = _UsuarioBusiness.Registrarme(nombre, apellido, celular, usuario, contrasena, categoriaId);
            }

            if (id > 0)
            {
                this.LogIn(usuario, contrasena);
            }
            return Json(id);
        }

        public JsonResult GetByUsuarioNombre(string usuarioNombre)
        {
            bool existeUsuario = false;
            Usuario usuarioExistente = _UsuarioBusiness.GetByUsuarioNombre(usuarioNombre);
            if (usuarioExistente != null)
            {
                existeUsuario = true;
            }
            return Json(existeUsuario, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Perfil(int id)
        {

            UsuarioDTO usuario = _UsuarioBusiness.GetPerfil(id);
            string rutaDirectorio = Url.Content("~/" + "imgPerfiles/");

            // Nombre del archivo basado en el nombre de usuario
            string nombreArchivo = usuario.FotoPerfil;
            string rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);
            usuario.FotoPerfil = rutaCompleta;
            return View(usuario);

        }

        public JsonResult LogIn(string usuarioNombre, string contrasena)
        {
            UsuarioDTO usuarioExiste = _UsuarioBusiness.GetLogin(usuarioNombre, contrasena);

            if (usuarioExiste != null)
            {
                HttpCookie cookie = new HttpCookie("UsuarioSesion");
                cookie["Id"] = usuarioExiste.Id.ToString(); // Puedes almacenar más información según tus necesidades
                Response.Cookies.Add(cookie);
            }

            return Json(usuarioExiste, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CerrarSesion()
        {
            if (Request.Cookies["UsuarioSesion"] != null)
            {
                var cookie = new HttpCookie("UsuarioSesion")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookie);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EsAdmin(int idUsuario)
        {
            bool esAdmin = _UsuarioBusiness.EsAdmin(idUsuario);
            return Json(esAdmin, JsonRequestBehavior.AllowGet);
        }

    }
}