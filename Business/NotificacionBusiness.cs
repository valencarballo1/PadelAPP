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

        public bool CrearNotificacion(int tipoNotificacion, string detalle, int idPartido = 0)
        {
            try
            {
                Notificaciones notificacion = new Notificaciones();
                notificacion.TipoNotificacion = tipoNotificacion;
                notificacion.Detalle = detalle;
                notificacion.FechaAlta = DateTime.Now;
                notificacion.Estado = true;
                notificacion.idCanchaReservada = idPartido;

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

        public bool LeerNotificaciones(int idUsuario)
        {
            try
            {
                List<Notificaciones> notificacionesNoLeidas = _NotificacionRepository.GetAll(idUsuario);
                List<LecturasNotificaciones> notifiacionesLeidas = new List<LecturasNotificaciones>();

                notificacionesNoLeidas.ForEach(n =>
                {
                    LecturasNotificaciones leerNotificacion = new LecturasNotificaciones
                    {
                        FechaLectura = DateTime.Now,
                        IdNotificacion = n.IdNotificacion,
                        IdUsuario = idUsuario,
                    };
                    notifiacionesLeidas.Add(leerNotificacion);
                });

                return _NotificacionRepository.LeerNotificaciones(notifiacionesLeidas);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
