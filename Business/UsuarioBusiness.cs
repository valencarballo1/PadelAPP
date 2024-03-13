using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Business
{
    public class UsuarioBusiness
    {
        private UsuarioRepository _UsuarioRepository;
        private CategoriaRepository _CategoriaRepository;

        public UsuarioBusiness()
        {
            this._UsuarioRepository = new UsuarioRepository();
            this._CategoriaRepository = new CategoriaRepository();
        }
        public int Registrarme(string nombre, string apellido, string celular, string usuario, string contrasena, int categoriaID, string extension = "")
        {
            Usuario usuarioExiste = _UsuarioRepository.GetByUsuarioNombre(usuario.ToLower());

            if (usuarioExiste == null)
            {
                double categoriaPuntuacion = _CategoriaRepository.GetPuntuacionById(categoriaID);
                string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasena);
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.EsAdmin = false;
                Perfil usuarioPerfil = new Perfil();

                nuevoUsuario.NombreUsuario = usuario.ToLower();
                nuevoUsuario.Contrasena = contrasenaHash;

                usuarioPerfil.Nombre = nombre;
                usuarioPerfil.Apellido = apellido;
                usuarioPerfil.Celular = celular;
                usuarioPerfil.CategoriaID = categoriaID;
                usuarioPerfil.Puntuacion = Convert.ToDecimal(categoriaPuntuacion);
                if (extension == "")
                {
                    usuarioPerfil.FotoPerfil = "LogoPadel.jpg";
                }
                else
                {
                    usuarioPerfil.FotoPerfil = nuevoUsuario.NombreUsuario + extension;
                }
                nuevoUsuario.Perfil.Add(usuarioPerfil);

                return _UsuarioRepository.Save(nuevoUsuario);
            }
            else
            {
                throw new Exception("Usuario ya existe");
            }

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

        public bool EsAdmin(int idUsuario)
        {
            return _CategoriaRepository.EsAdmin(idUsuario);
        }

        public List<RankingDTO> GetRanking()
        {
            return _UsuarioRepository.GetRanking();
        }
    }
}
