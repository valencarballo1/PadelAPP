using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class NotificacionBusiness
    {
        private NotificacionRepository _NotificacionRepository;

        public NotificacionBusiness()
        {
            this._NotificacionRepository = new NotificacionRepository();
        }

        public bool CrearNotificacion(int tipoNotificacion, string detalle)
        {
            try
            {
                Notificaciones notificacion = new Notificaciones();
                notificacion.TipoNotificacion = tipoNotificacion;
                notificacion.Detalle = detalle;
                notificacion.FechaAlta = DateTime.Now;
                notificacion.Estado = true;
                return _NotificacionRepository.SaveNotificacion(notificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Notificaciones> GetAll(int idUsuario)
        {
            return _NotificacionRepository.GetAll(idUsuario);
        }
    }
}
