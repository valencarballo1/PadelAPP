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
    public class HorariosRepository
    {
        public bool Save(Horarios horario)
        {
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                bool grabo = false;
                if (horario != null)
                {
                    db.Horarios.AddOrUpdate(horario);
                    db.SaveChanges();
                    grabo = true;
                }
                return grabo;
            }
        }
        public List<HorarioDTO> Load(int idCancha, DateTime fechaSeleccionada)
        {
            using (PadelAPPEntities db = new PadelAPPEntities())
            {
                List<int> listaReserva = db.CanchasReservadas.Where(c => c.IdCancha == idCancha).Select(c => c.IdHorario.Value).ToList();
                DateTime inicioDelDia = fechaSeleccionada.Date;
                DateTime finDelDia = inicioDelDia.AddDays(1).AddTicks(-1);
                List<Horarios> lista = db.Horarios
                    .Where(h => h.HorarioDesde >= inicioDelDia && h.HorarioHasta <= finDelDia && listaReserva.Contains(h.Id))
                    .ToList();

                List<HorarioDTO> horariosDTO = lista.Select(h => new HorarioDTO
                {
                    Id = h.Id,
                    HorarioDesde = h.HorarioDesde.Value,
                    HorarioHasta = h.HorarioHasta.Value,
                    Duracion = h.Duracion.Value
                    // Mapea otras propiedades según sea necesario
                }).ToList();

                return horariosDTO;
            }
        }
    }
}
