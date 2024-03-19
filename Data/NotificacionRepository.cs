using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NotificacionRepository
    {
        public List<Notificaciones> GetAll(int idUsuario)
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<Notificaciones> notificacionesNoLeidas = db.Notificaciones.Include("LecturasNotificaciones")
                    .Where(n => !db.LecturasNotificaciones
                    .Any(ln => ln.IdNotificacion == n.IdNotificacion && ln.IdUsuario == idUsuario))
                    .ToList();
                return notificacionesNoLeidas;
            }
        }

        public bool SaveNotificacion(Notificaciones notificacion)
        {
            using(PadelAppEntities db = new PadelAppEntities())
            {
                bool grabo = false; ;
                if(notificacion != null)
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
