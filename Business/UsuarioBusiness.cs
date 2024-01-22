using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioBusiness
    {
        private UsuarioRepository _UsuarioRepository;

        public UsuarioBusiness()
        {
            this._UsuarioRepository = new UsuarioRepository();
        }
        public int Registrarme(string nombre, string apellido, string celular, string usuario, string contrasena, string extension = "")
        {
            Usuario nuevoUsuario = new Usuario();
            Perfil usuarioPerfil = new Perfil();

            nuevoUsuario.NombreUsuario = usuario.ToLower();
            nuevoUsuario.Contrasena = contrasena;

            usuarioPerfil.Nombre = nombre;
            usuarioPerfil.Apellido = apellido;
            usuarioPerfil.Celular = celular;
            if (extension == "")
            {
                usuarioPerfil.FotoPerfil = nuevoUsuario.NombreUsuario;
            }
            else
            {
                usuarioPerfil.FotoPerfil = nuevoUsuario.NombreUsuario + extension;
            }
            nuevoUsuario.Perfil.Add(usuarioPerfil);

            return _UsuarioRepository.Save(nuevoUsuario);
        }

        public Usuario GetByUsuarioNombre(string usuarioNombre)
        {
            return _UsuarioRepository.GetByUsuarioNombre(usuarioNombre);
        }

        public DTO.UsuarioDTO GetPerfil(int id)
        {
            return _UsuarioRepository.GetPerfil(id);
        }

        public DTO.UsuarioDTO GetLogin(string usuarioNombre, string contrasena)
        {
            return _UsuarioRepository.GetLogin(usuarioNombre, contrasena);
        }
    }
}
