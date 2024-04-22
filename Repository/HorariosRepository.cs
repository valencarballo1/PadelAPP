using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (PadelAppEntities db = new PadelAppEntities())
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
            using (PadelAppEntities db = new PadelAppEntities())
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

        public bool ExisteHorarioByDesdeYDuracion(DateTime horarioDesde, int duracion)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                // Calcular el horario hasta
                DateTime horarioHasta = horarioDesde.AddMinutes(duracion);

                // Verificar si existe algún horario dentro del rango de tiempo de la reserva
                bool existeHorario = db.Horarios.Any(h =>
                    (h.HorarioDesde >= horarioDesde && h.HorarioDesde < horarioHasta) || // Verificar si el horario desde del horario reservado está dentro de otro horario
                    (DbFunctions.AddMinutes(h.HorarioDesde, h.Duracion) > horarioDesde && DbFunctions.AddMinutes(h.HorarioDesde, h.Duracion) <= horarioHasta) // Verificar si el horario hasta del horario reservado está dentro de otro horario
                );

                return existeHorario;
            }
        }
    }
}
