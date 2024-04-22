using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                try
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

                        int usuarioId = _UsuarioRepository.Save(nuevoUsuario);

                        transactionScope.Complete();

                        return usuarioId;
                    }
                    else
                    {
                        throw new Exception("Usuario ya existe");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Problemas en el servidor!"); 
                }
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

        public bool UpdateCelular(string nuevoCelular, int idUsuario)
        {
            Perfil perfilActualiza = _UsuarioRepository.GetPerfilById(idUsuario);
            perfilActualiza.Celular = nuevoCelular;
            return _UsuarioRepository.SavePerfil(perfilActualiza);
        }
    }
}
