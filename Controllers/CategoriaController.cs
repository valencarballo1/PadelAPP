using Antlr.Runtime.Misc;
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
    public class CategoriaController : Controller
    {
        private CategoriaBusiness _CategoriaBusiness;

        public CategoriaController()
        {
            this._CategoriaBusiness = new CategoriaBusiness();
        }
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            List<CategoriaDTO> lista = _CategoriaBusiness.GetAll();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}