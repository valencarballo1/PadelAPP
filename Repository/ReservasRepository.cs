using Data;
using System;
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
        public List<DTO.ReservaDTO> GetReservas()
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                List<CanchasReservadas> lista = db.CanchasReservadas.Include("Horarios").ToList();

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

        public void SaveCancha(CanchasReservadas canchaReservada)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                db.CanchasReservadas.AddOrUpdate(canchaReservada);
                db.SaveChanges();
            }
        }
    }
}
