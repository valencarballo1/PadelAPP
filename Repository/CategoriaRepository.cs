using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class CategoriaRepository
    {
        public bool EsAdmin(int idUsuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.Usuario.Where(u => u.Id == idUsuario).Single().EsAdmin.Value;
            }
        }

        public List<CategoriaDTO> GetAllCategorias()
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                List<CategoriaDTO> lista = new List<CategoriaDTO>();
                List<Categorias> categorias = db.Categorias.ToList();

                categorias.ForEach(c =>
                {
                    CategoriaDTO categoria = new CategoriaDTO { 
                        Id = c.CategoriaID,
                        Nombre = c.Nombre,
                        Puntuacion = c.PuntuacionMin.Value
                    };
                    lista.Add(categoria);
                });

                return lista;
            }
        }

        public Categorias GetByPuntuacion(double puntuacion)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                return db.Categorias.Where(c => c.PuntuacionMin <= puntuacion && c.PuntuacionMax >= puntuacion).SingleOrDefault();
            }
        }

        public double GetPuntuacionById(int categoriaID)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.Categorias.Where(c => c.CategoriaID == categoriaID).SingleOrDefault().PuntuacionMin.Value;
            }
        }
    }
}
