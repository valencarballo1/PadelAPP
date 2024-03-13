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
    public class PartidoBusiness
    {
        private PartidoRepository _PartidoRepository;
        private UsuarioRepository _UsuarioRepository;
        private CategoriaRepository _CategoriaRepository;

        public PartidoBusiness()
        {
            this._PartidoRepository = new PartidoRepository();
            this._UsuarioRepository = new UsuarioRepository();
            this._CategoriaRepository = new CategoriaRepository();
        }
        public PartidoDTO Get(int idPartido)
        {
            return _PartidoRepository.GetDTO(idPartido);
        }

        public bool Unirse(int idPartido, int idUsuario, int posicionJugador)
        {
            bool seUnio = false;
            PartidosCreadosUsuarios partido = _PartidoRepository.Get(idPartido);
            Parejas pareja = _PartidoRepository.GetParejaByPartido(idPartido);

            if (posicionJugador == 2)
            {

                if (partido.IdJugador2 == null)
                {
                    partido.IdJugador2 = idUsuario;
                    pareja.IdJugador2 = idUsuario;
                    _PartidoRepository.AddOrUpdatePareja(pareja);
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            if (posicionJugador == 3)
            {

                if (partido.IdJugador3 == null)
                {
                    partido.IdJugador3 = idUsuario;
                    pareja.IdJugador3 = idUsuario;
                    _PartidoRepository.AddOrUpdatePareja(pareja);
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            if (posicionJugador == 4)
            {
                if (partido.IdJugador4 == null)
                {
                    partido.IdJugador4 = idUsuario;
                    pareja.IdJugador4 = idUsuario;
                    _PartidoRepository.AddOrUpdatePareja(pareja);
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            return seUnio;
        }

        public List<DetallePartidoDTO> GetDetallePartidos()
        {
            return _PartidoRepository.GetDetallePartido();
        }

        public List<PartidoDTO> GetUltimos(int idUsuario)
        {
            return _PartidoRepository.GetUltimos(idUsuario);
        }

        public List<PartidoDTO> GetPartidosDisponibles()
        {
            return _PartidoRepository.GetPartidosDisponibles();
        }

        public ParejaDTO GetParejasDTOByPartido(int idPartido)
        {
            return _PartidoRepository.GetParejasDTOByPartido(idPartido);
        }

        public bool GrabarResultado(int idPareja, ResultadoDTO resultado)
        {
            try
            {
                int cantidadSetPareja1 = 0;
                int cantidadSetPareja2 = 0;
                Parejas pareja = _PartidoRepository.GetParejaById(idPareja);
                PartidoResultado partidoResultado = new PartidoResultado();

                if (resultado != null)
                {
                    partidoResultado.Set1Pareja1 = resultado.set1Pareja1;
                    partidoResultado.Set1Pareja2 = resultado.set1Pareja2;
                    partidoResultado.Set2Pareja1 = resultado.set2Pareja1;
                    partidoResultado.Set2Pareja2 = resultado.set2Pareja2;
                    partidoResultado.Set3Pareja1 = resultado.set3Pareja1;
                    partidoResultado.Set3Pareja2 = resultado.set3Pareja2;
                    partidoResultado.IdPareja = idPareja;
                    partidoResultado.Estado = true;
                    partidoResultado.FechaAlta = DateTime.Now;
                    _PartidoRepository.SaveResultado(partidoResultado);
                }

                cantidadSetPareja1 = partidoResultado.Set1Pareja1.Value + partidoResultado.Set2Pareja1.Value + partidoResultado.Set3Pareja1.Value;
                cantidadSetPareja2 = partidoResultado.Set1Pareja2.Value + partidoResultado.Set2Pareja2.Value + partidoResultado.Set3Pareja2.Value;

                Perfil perfil1 = _UsuarioRepository.GetPerfilById(pareja.IdJugador1.Value);
                Perfil perfil2 = _UsuarioRepository.GetPerfilById(pareja.IdJugador2.Value);
                Perfil perfil3 = _UsuarioRepository.GetPerfilById(pareja.IdJugador3.Value);
                Perfil perfil4 = _UsuarioRepository.GetPerfilById(pareja.IdJugador4.Value);


                decimal promedioPuntuacionPareja1 = perfil1.Puntuacion.Value + perfil2.Puntuacion.Value;
                decimal promedioPuntuacionPareja2 = perfil3.Puntuacion.Value + perfil4.Puntuacion.Value;

                if (cantidadSetPareja1 > cantidadSetPareja2)
                {

                    if (promedioPuntuacionPareja1 >= promedioPuntuacionPareja2)
                    {
                        perfil1.Puntuacion += (decimal)0.1;
                        perfil2.Puntuacion += (decimal)0.1;

                        perfil3.Puntuacion -= (decimal)0.1;
                        perfil4.Puntuacion -= (decimal)0.1;
                    }
                    else
                    {
                        perfil1.Puntuacion += (decimal)0.15;
                        perfil2.Puntuacion += (decimal)0.15;

                        perfil3.Puntuacion -= (decimal)0.15;
                        perfil4.Puntuacion -= (decimal)0.15;
                    }
                }
                else
                {
                    if (promedioPuntuacionPareja2 >= promedioPuntuacionPareja1)
                    {
                        perfil3.Puntuacion += (decimal)0.1;
                        perfil4.Puntuacion += (decimal)0.1;

                        perfil1.Puntuacion -= (decimal)0.1;
                        perfil2.Puntuacion -= (decimal)0.1;
                    }
                    else
                    {
                        perfil3.Puntuacion += (decimal)0.15;
                        perfil4.Puntuacion += (decimal)0.15;

                        perfil1.Puntuacion -= (decimal)0.15;
                        perfil2.Puntuacion -= (decimal)0.15;
                    }
                }

                Categorias categoria1 = _CategoriaRepository.GetByPuntuacion((double)perfil1.Puntuacion);
                Categorias categoria2 = _CategoriaRepository.GetByPuntuacion((double)perfil2.Puntuacion);
                Categorias categoria3 = _CategoriaRepository.GetByPuntuacion((double)perfil3.Puntuacion);
                Categorias categoria4 = _CategoriaRepository.GetByPuntuacion((double)perfil4.Puntuacion);

                perfil1.CategoriaID = categoria1.CategoriaID;
                perfil2.CategoriaID = categoria2.CategoriaID;
                perfil3.CategoriaID = categoria3.CategoriaID;
                perfil4.CategoriaID = categoria4.CategoriaID;

                _PartidoRepository.AddOrUpdatePerfil(perfil1);
                _PartidoRepository.AddOrUpdatePerfil(perfil2);
                _PartidoRepository.AddOrUpdatePerfil(perfil3);
                _PartidoRepository.AddOrUpdatePerfil(perfil4);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
