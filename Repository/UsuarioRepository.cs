using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class UsuarioRepository
    {
        public int Save(Usuario usuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                db.Usuario.AddOrUpdate(usuario);
                db.SaveChanges();
                return usuario.Id;
            }
        }

        public Usuario GetByUsuarioNombre(string usuarioNombre)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.Usuario.Where(u => u.NombreUsuario == usuarioNombre).SingleOrDefault();
            }
        }

        public UsuarioDTO GetPerfil(int id)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                Usuario usuario = db.Usuario.Include("Perfil").Where(u => u.Id == id).SingleOrDefault();

                UsuarioDTO perfil = new UsuarioDTO
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Nombre = usuario.Perfil.SingleOrDefault().Nombre,
                    Apellido = usuario.Perfil.SingleOrDefault().Apellido,
                    Celular = usuario.Perfil.SingleOrDefault().Celular,
                    FotoPerfil = usuario.Perfil.SingleOrDefault().FotoPerfil,
                    Categoria = usuario.Perfil.SingleOrDefault().Categorias.Nombre,
                    Puntuacion = usuario.Perfil.SingleOrDefault().Puntuacion.Value
                };

                return perfil;
            }
        }

        public UsuarioDTO GetLogin(string usuarioNombre, string contrasena)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                UsuarioDTO perfil;
                string usuNombre = usuarioNombre.ToLower();
                Usuario usuario = db.Usuario.Include("Perfil").Where(u => u.NombreUsuario == usuNombre).SingleOrDefault();
                bool contraTrue = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena);

                if (usuario != null && contraTrue)
                {
                    perfil = new UsuarioDTO
                    {
                        Id = usuario.Id,
                        NombreUsuario = usuario.NombreUsuario,
                        Nombre = usuario.Perfil.SingleOrDefault().Nombre,
                        Apellido = usuario.Perfil.SingleOrDefault().Apellido,
                        Celular = usuario.Perfil.SingleOrDefault().Celular,
                        FotoPerfil = usuario.Perfil.SingleOrDefault().FotoPerfil
                    };
                }
                else
                {
                    perfil = null;
                }

                return perfil;
            }
        }

        public Perfil GetPerfilById(int id)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.Perfil.Where(p => p.IdUsuario == id).SingleOrDefault();
            }
        }

        public List<RankingDTO> GetRanking()
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                List<RankingDTO> ranking = new List<RankingDTO>();
                int posicion = 0;
                List<Perfil> listaPerfil = db.Perfil.Include("Usuario").OrderByDescending(o => o.Puntuacion).ToList();

                listaPerfil.ForEach(l =>
                {
                    RankingDTO player = new RankingDTO();
                    player.Usuario = l.Usuario.NombreUsuario;
                    player.Posicion = ++posicion;
                    player.Puntuacion = l.Puntuacion.Value;
                    player.FotoPerfil = l.FotoPerfil;
                    ranking.Add(player);
                });

                return ranking;
            }
        }
    }
}
