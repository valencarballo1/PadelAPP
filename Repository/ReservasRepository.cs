using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class ReservasRepository
    {
        public CanchasReservadas GetCanchaReservada(int idCancha)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                return db.CanchasReservadas.Include("Horarios").Where(c => c.Id == idCancha && c.Estado == ESTADO.PENDIENTE).SingleOrDefault();
            }
        }

        public List<DTO.ReservaDTO> GetReservas()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<CanchasReservadas> lista = db.CanchasReservadas.Include("Horarios").Where(r => r.Estado != ESTADO.BAJA).ToList();

                List<ReservaDTO> reservas = lista.Select(h => new ReservaDTO
                {
                    Id = h.Id,
                    HorarioDesde = h.Horarios.HorarioDesde.Value,
                    HorarioHasta = h.Horarios.HorarioHasta.Value,
                    CanchaNumero = h.IdCancha.Value,
                    UsuarioNombre = h.Usuario.NombreUsuario
                })
                .OrderBy(r => r.HorarioDesde)
                .ToList();

                return reservas;
            }
        }

        public List<ReservaDTO> GetReservasAdmin(DateTime fecha)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime inicioDia = fecha.Date;
                DateTime finDia = fecha.Date.AddDays(1).AddTicks(-1);

                List<CanchasReservadas> lista = db.CanchasReservadas
                    .Include("Horarios")
                    .Where(r => r.Horarios.HorarioDesde >= inicioDia && r.Horarios.HorarioHasta <= finDia
                    && r.Estado == ESTADO.PENDIENTE)
                    .ToList();

                List<ReservaDTO> reservas = lista.Select(h => new ReservaDTO
                {
                    Id = h.Id,
                    HorarioDesde = h.Horarios.HorarioDesde.Value,
                    HorarioHasta = h.Horarios.HorarioHasta.Value,
                    CanchaNumero = h.IdCancha.Value,
                    UsuarioNombre = h.Usuario.NombreUsuario
                })
                .OrderBy(r => r.HorarioDesde)
                .ToList();

                return reservas;
            }
        }

        public List<ReservaDTO> GetReservasByUsuario(int idUsuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<ReservaDTO> reservasDTO = db.CanchasReservadas
                    .Include("Canchas")
                    .Include("Horarios")
                    .Where(c => c.IdUsuario == idUsuario && c.Estado != ESTADO.BAJA)
                    .OrderByDescending(r => r.Horarios.HorarioDesde) // Asumiendo que tienes una propiedad FechaDeCreacion en CanchasReservadas
                    .Take(3)
                    .Select(r => new ReservaDTO
                    {
                        CanchaNumero = r.Canchas.NumeroCancha.Value,
                        HorarioDesde = r.Horarios.HorarioDesde.Value,
                        HorarioHasta = r.Horarios.HorarioHasta.Value,
                        Duracion = r.Horarios.Duracion.Value,
                    })
                    .ToList();

                return reservasDTO;
            }
        }

        public void SaveCancha(CanchasReservadas canchaReservada)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                db.CanchasReservadas.AddOrUpdate(canchaReservada);
                db.SaveChanges();
            }
        }

        public bool SaveRecaudacion(RecaudacionCancha recaudacion)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                bool grabo = false;
                if (recaudacion != null)
                {
                    db.RecaudacionCancha.AddOrUpdate(recaudacion);
                    db.SaveChanges();
                    grabo = true;
                }
                return grabo;
            }
        }
    }
}
