using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Data
{
    public class NotificacionRepository
    {
        public List<NotificacionesDTO> GetAll(int idUsuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<NotificacionesDTO> listaNoti = new List<NotificacionesDTO>();
                List<Notificaciones> notificacionesNoLeidas = db.Notificaciones.Include("LecturasNotificaciones")
                    .Where(n => !db.LecturasNotificaciones
                    .Any(ln => ln.IdNotificacion == n.IdNotificacion && ln.IdUsuario == idUsuario))
                    .ToList();

                notificacionesNoLeidas.ForEach(n =>
                {
                    NotificacionesDTO nueva = new NotificacionesDTO
                    {
                        IdNotificacion = n.IdNotificacion,
                        Detalle = n.Detalle,
                        IdCanchaReservada = n.idCanchaReservada.Value,
                        TipoNotificacion = n.TipoNotificacion.Value
                    };
                    listaNoti.Add(nueva);
                });
                return listaNoti;
            }
        }

        public bool LeerNotificaciones(List<LecturasNotificaciones> notificacionesLeidas)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                bool notiLeida = false;
                if (notificacionesLeidas.Count > 0)
                {
                    notificacionesLeidas.ForEach(n =>
                    {
                        db.LecturasNotificaciones.AddOrUpdate(n);
                    });
                    db.SaveChanges();
                    notiLeida = true;
                }
                return notiLeida;
            }
        }

        public bool SaveNotificacion(Notificaciones notificacion)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                bool grabo = false;
                if (notificacion != null)
                {
                    db.Notificaciones.AddOrUpdate(notificacion);
                    db.SaveChanges();
                    grabo = true;
                }
                return grabo;
            }
        }
    }
}
