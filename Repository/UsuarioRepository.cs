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
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                db.Usuario.AddOrUpdate(usuario);
                db.SaveChanges();
                return usuario.Id;
            }
        }

        public Usuario GetByUsuarioNombre(string usuarioNombre)
        {
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                return db.Usuario.Where(u => u.NombreUsuario == usuarioNombre).SingleOrDefault();
            }
        }

        public UsuarioDTO GetPerfil(int id)
        {
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                Usuario usuario = db.Usuario.Include("Perfil").Where(u => u.Id == id).SingleOrDefault();

                UsuarioDTO perfil = new UsuarioDTO
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Nombre = usuario.Perfil.SingleOrDefault().Nombre,
                    Apellido = usuario.Perfil.SingleOrDefault().Apellido,
                    Celular = usuario.Perfil.SingleOrDefault().Celular,
                    FotoPerfil = usuario.Perfil.SingleOrDefault().FotoPerfil
                };

                return perfil;
            }
        }

        public UsuarioDTO GetLogin(string usuarioNombre, string contrasena)
        {
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                UsuarioDTO perfil;
                string usuNombre = usuarioNombre.ToLower();
                Usuario usuario = db.Usuario.Include("Perfil").Where(u => u.NombreUsuario == usuNombre && u.Contrasena == contrasena).SingleOrDefault();

                if (usuario != null)
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
    }
}
