using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class PartidoRepository
    {
        private UsuarioRepository _UsuarioRepository;
        public PartidoRepository()
        {
            this._UsuarioRepository = new UsuarioRepository();
        }

        public PartidosCreadosUsuarios Get(int idPartido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.PartidosCreadosUsuarios.Include("CanchasReservadas").Where(p => p.Id == idPartido).SingleOrDefault();
            }
        }

        public PartidoDTO GetDTO(int idPartido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                PartidosCreadosUsuarios partidoEncontrado = db.PartidosCreadosUsuarios.Include("CanchasReservadas").Where(p => p.Id == idPartido).SingleOrDefault();
                UsuarioDTO jugador1 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador1.Value);
                UsuarioDTO jugador2 = null;
                UsuarioDTO jugador3 = null;
                UsuarioDTO jugador4 = null;

                if (partidoEncontrado.IdJugador2 != null)
                {
                    jugador2 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador2.Value);
                }

                if (partidoEncontrado.IdJugador3 != null)
                {
                    jugador3 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador3.Value);
                }

                if (partidoEncontrado.IdJugador4 != null)
                {
                    jugador4 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador4.Value);
                }
                PartidoDTO partido = new PartidoDTO
                {
                    Id = partidoEncontrado.Id,
                    IdUsuarioOrganiza = partidoEncontrado.CanchasReservadas.IdUsuario.Value,
                    CanchaNumero = partidoEncontrado.CanchasReservadas.IdCancha.Value,
                    HorarioDesde = partidoEncontrado.CanchasReservadas.Horarios.HorarioDesde.Value,
                    HorarioHasta = partidoEncontrado.CanchasReservadas.Horarios.HorarioHasta.Value,
                    UsuarioJ1 = jugador1.NombreUsuario,
                    FotoPerfil1 = jugador1.FotoPerfil,
                    Categoria1 = jugador1.Categoria,
                    Puntuacion1 = jugador1.Puntuacion,
                    UsuarioJ2 = jugador2?.NombreUsuario ?? string.Empty,
                    FotoPerfil2 = jugador2?.FotoPerfil ?? string.Empty,
                    Categoria2 = jugador2?.Categoria ?? string.Empty,
                    Puntuacion2 = jugador2?.Puntuacion,
                    UsuarioJ3 = jugador3?.NombreUsuario ?? string.Empty,
                    FotoPerfil3 = jugador3?.FotoPerfil ?? string.Empty,
                    Categoria3 = jugador3?.Categoria ?? string.Empty,
                    Puntuacion3 = jugador3?.Puntuacion,
                    UsuarioJ4 = jugador4?.NombreUsuario ?? string.Empty,
                    FotoPerfil4 = jugador4?.FotoPerfil ?? string.Empty,
                    Categoria4 = jugador4?.Categoria ?? string.Empty,
                    Puntuacion4 = jugador4?.Puntuacion,

                };

                return partido;
            }
        }

        public void Save(PartidosCreadosUsuarios partido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                db.PartidosCreadosUsuarios.AddOrUpdate(partido);
                db.SaveChanges();
            }
        }

        public List<DetallePartidoDTO> GetDetallePartido()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaHoy = DateTime.Now;

                List<PartidosCreadosUsuarios> partidosEncontrado = db.PartidosCreadosUsuarios
                    .Include("CanchasReservadas")
                    .Where(p => p.CanchasReservadas.Horarios.HorarioDesde >= fechaHoy)
                    .ToList();

                List<DetallePartidoDTO> detalles = partidosEncontrado.Select(h => new DetallePartidoDTO
                {
                    Id = h.Id,
                    HorarioDesde = h.CanchasReservadas.Horarios.HorarioDesde.Value,
                    HorarioHasta = h.CanchasReservadas.Horarios.HorarioHasta.Value,
                    CanchaNumero = h.CanchasReservadas.IdCancha.Value,
                    CantidadJugadores = this.CantidadJugadores(h.Id),
                    UsuarioOrganizador = h.Usuario.NombreUsuario
                })
                .OrderBy(r => r.HorarioDesde)
                .ToList();

                return detalles;
            }
        }

        public int CantidadJugadores(int idPartido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                int jugadores = 0;
                PartidosCreadosUsuarios partidoCreado = db.PartidosCreadosUsuarios.Where(p => p.Id == idPartido).Single();
                if (partidoCreado.IdJugador1 != null)
                {
                    jugadores++;
                }
                if (partidoCreado.IdJugador2 != null)
                {
                    jugadores++;
                }
                if (partidoCreado.IdJugador3 != null)
                {
                    jugadores++;
                }
                if (partidoCreado.IdJugador4 != null)
                {
                    jugadores++;
                }
                return jugadores;

            }
        }

        public List<PartidoDTO> GetUltimos(int idUsuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<PartidoDTO> partidosLista = new List<PartidoDTO>();
                List<PartidosCreadosUsuarios> partidosEncontrado = db.PartidosCreadosUsuarios.Include("CanchasReservadas")
                    .Where(p => p.IdJugador1 == idUsuario || p.IdJugador2 == idUsuario || p.IdJugador3 == idUsuario || p.IdJugador3 == idUsuario)
                    .OrderByDescending(p => p.Id)
                    .Take(3)
                    .ToList();

                partidosEncontrado.ForEach(partidoEncontrado =>
                {
                    UsuarioDTO jugador1 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador1.Value);
                    UsuarioDTO jugador2 = null;
                    UsuarioDTO jugador3 = null;
                    UsuarioDTO jugador4 = null;

                    if (partidoEncontrado.IdJugador2 != null)
                    {
                        jugador2 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador2.Value);

                    }

                    if (partidoEncontrado.IdJugador3 != null)
                    {
                        jugador3 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador3.Value);

                    }

                    if (partidoEncontrado.IdJugador4 != null)
                    {
                        jugador4 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador4.Value);

                    }

                    PartidoDTO partido = new PartidoDTO
                    {
                        Id = partidoEncontrado.Id,
                        CanchaNumero = partidoEncontrado.CanchasReservadas.IdCancha.Value,
                        HorarioDesde = partidoEncontrado.CanchasReservadas.Horarios.HorarioDesde.Value,
                        HorarioHasta = partidoEncontrado.CanchasReservadas.Horarios.HorarioHasta.Value,
                        UsuarioJ1 = jugador1.NombreUsuario,
                        FotoPerfil1 = jugador1.FotoPerfil,
                        Categoria1 = jugador1.Categoria,
                        Puntuacion1 = jugador1.Puntuacion,
                        UsuarioJ2 = jugador2?.NombreUsuario ?? string.Empty,
                        FotoPerfil2 = jugador2?.FotoPerfil ?? string.Empty,
                        Categoria2 = jugador2?.Categoria ?? string.Empty,
                        Puntuacion2 = jugador2?.Puntuacion,
                        UsuarioJ3 = jugador3?.NombreUsuario ?? string.Empty,
                        FotoPerfil3 = jugador3?.FotoPerfil ?? string.Empty,
                        Categoria3 = jugador3?.Categoria ?? string.Empty,
                        Puntuacion3 = jugador3?.Puntuacion,
                        UsuarioJ4 = jugador4?.NombreUsuario ?? string.Empty,
                        FotoPerfil4 = jugador4?.FotoPerfil ?? string.Empty,
                        Categoria4 = jugador4?.Categoria ?? string.Empty,
                        Puntuacion4 = jugador4?.Puntuacion,

                    };

                    partidosLista.Add(partido);

                });
                return partidosLista;

            }
        }

        public List<PartidoDTO> GetPartidosDisponibles()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaHoy = DateTime.Now;
                List<PartidoDTO> partidosLista = new List<PartidoDTO>();
                List<PartidosCreadosUsuarios> partidosEncontrado = db.PartidosCreadosUsuarios.Include("CanchasReservadas")
                    .Where(p => p.CanchasReservadas.Horarios.HorarioDesde >= fechaHoy)
                    .OrderByDescending(p => p.Id)
                    .ToList();

                partidosEncontrado.ForEach(partidoEncontrado =>
                {
                    UsuarioDTO jugador1 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador1.Value);
                    UsuarioDTO jugador2 = null;
                    UsuarioDTO jugador3 = null;
                    UsuarioDTO jugador4 = null;

                    if (partidoEncontrado.IdJugador2 != null)
                    {
                        jugador2 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador2.Value);

                    }

                    if (partidoEncontrado.IdJugador3 != null)
                    {
                        jugador3 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador3.Value);

                    }

                    if (partidoEncontrado.IdJugador4 != null)
                    {
                        jugador4 = _UsuarioRepository.GetPerfil(partidoEncontrado.IdJugador4.Value);

                    }

                    PartidoDTO partido = new PartidoDTO
                    {
                        Id = partidoEncontrado.Id,
                        CanchaNumero = partidoEncontrado.CanchasReservadas.IdCancha.Value,
                        HorarioDesde = partidoEncontrado.CanchasReservadas.Horarios.HorarioDesde.Value,
                        HorarioHasta = partidoEncontrado.CanchasReservadas.Horarios.HorarioHasta.Value,
                        UsuarioJ1 = jugador1.NombreUsuario,
                        FotoPerfil1 = jugador1.FotoPerfil,
                        Categoria1 = jugador1.Categoria,
                        Puntuacion1 = jugador1.Puntuacion,
                        UsuarioJ2 = jugador2?.NombreUsuario ?? string.Empty,
                        FotoPerfil2 = jugador2?.FotoPerfil ?? string.Empty,
                        Categoria2 = jugador2?.Categoria ?? string.Empty,
                        Puntuacion2 = jugador2?.Puntuacion,
                        UsuarioJ3 = jugador3?.NombreUsuario ?? string.Empty,
                        FotoPerfil3 = jugador3?.FotoPerfil ?? string.Empty,
                        Categoria3 = jugador3?.Categoria ?? string.Empty,
                        Puntuacion3 = jugador3?.Puntuacion,
                        UsuarioJ4 = jugador4?.NombreUsuario ?? string.Empty,
                        FotoPerfil4 = jugador4?.FotoPerfil ?? string.Empty,
                        Categoria4 = jugador4?.Categoria ?? string.Empty,
                        Puntuacion4 = jugador4?.Puntuacion,

                    };

                    partidosLista.Add(partido);

                });
                return partidosLista;
            }
        }

        public Parejas GetParejaByPartido(int idPartido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.Parejas.Where(p => p.IdPartido == idPartido).SingleOrDefault();
            }
        }

        public ParejaDTO GetParejasDTOByPartido(int idPartido)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                Parejas pareja = db.Parejas.Include("PartidosCreadosUsuarios").Where(p => p.IdPartido == idPartido).SingleOrDefault();

                UsuarioDTO jugador1 = _UsuarioRepository.GetPerfil(pareja.PartidosCreadosUsuarios.IdJugador1.Value);
                UsuarioDTO jugador2 = null;
                UsuarioDTO jugador3 = null;
                UsuarioDTO jugador4 = null;

                if (pareja.PartidosCreadosUsuarios.IdJugador2 != null)
                {
                    jugador2 = _UsuarioRepository.GetPerfil(pareja.PartidosCreadosUsuarios.IdJugador2.Value);

                }

                if (pareja.PartidosCreadosUsuarios.IdJugador3 != null)
                {
                    jugador3 = _UsuarioRepository.GetPerfil(pareja.PartidosCreadosUsuarios.IdJugador3.Value);

                }

                if (pareja.PartidosCreadosUsuarios.IdJugador4 != null)
                {
                    jugador4 = _UsuarioRepository.GetPerfil(pareja.PartidosCreadosUsuarios.IdJugador4.Value);

                }



                ParejaDTO parejaDTO = new ParejaDTO
                {
                    Id = pareja.IdPareja,
                    Usuario1 = jugador1.NombreUsuario,
                    Usuario2 = jugador2.NombreUsuario,
                    Usuario3 = jugador3.NombreUsuario,
                    Usuario4 = jugador4.NombreUsuario
                };
                return parejaDTO;
            }
        }

        public void AddOrUpdatePareja(Parejas pareja)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                db.Parejas.AddOrUpdate(pareja);
                db.SaveChanges();
            }
        }

        public Parejas GetParejaById(int idPareja)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                return db.Parejas.Where(p => p.IdPareja == idPareja).SingleOrDefault();
            }
        }

        public void SaveResultado(PartidoResultado resultado)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                db.PartidoResultado.AddOrUpdate(resultado);
                db.SaveChanges();
            }
        }

        public void AddOrUpdatePerfil(Perfil perfil)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                db.Perfil.AddOrUpdate(perfil);
                db.SaveChanges();
            }
        }
    }
}
