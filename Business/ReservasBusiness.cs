using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Data.DTO;

namespace Business
{
    public class ReservasBusiness
    {
        private ReservasRepository _ReservasRepository;
        private HorariosRepository _HorariosRepository;
        private NotificacionBusiness _NotificacionBusiness;
        public ReservasBusiness()
        {
            this._ReservasRepository = new ReservasRepository();
            this._HorariosRepository = new HorariosRepository();
            this._NotificacionBusiness = new NotificacionBusiness();
        }

        public int CrearPartido(int idUsuario, int idCancha, string fechaSeleccionada, string horarioDeReserva, int duracion, int jugadoresRestantes)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                try
                {
                    // Lógica para crear el partido
                    string[] partes = fechaSeleccionada.Split('/');
                    int day = int.Parse(partes[0]);
                    int month = int.Parse(partes[1]);
                    int year = int.Parse(partes[2]);
                    DateTime fechaUsuario = new DateTime(year, month, day);
                    DateTime horarioDesde = fechaUsuario.Add(DateTime.ParseExact(horarioDeReserva, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
                    DateTime horarioHasta = horarioDesde.AddMinutes(duracion);

                    Horarios reserva = new Horarios();
                    CanchasReservadas canchasReservadas = new CanchasReservadas();
                    PartidosCreadosUsuarios partidoCreado = new PartidosCreadosUsuarios();
                    Parejas pareja = new Parejas();
                    pareja.FechaAlta = DateTime.Now;
                    pareja.UsuarioAlta = "Automatico";

                    reserva.HorarioDesde = horarioDesde;
                    reserva.HorarioHasta = horarioHasta;
                    reserva.Duracion = duracion;

                    canchasReservadas.IdCancha = idCancha;
                    canchasReservadas.IdUsuario = idUsuario;
                    canchasReservadas.Estado = ESTADO.PENDIENTE;
                    reserva.CanchasReservadas.Add(canchasReservadas);

                    if (jugadoresRestantes == 3)
                    {
                        partidoCreado.IdJugador1 = idUsuario;
                        pareja.IdJugador1 = idUsuario;
                    }
                    else if (jugadoresRestantes == 2)
                    {
                        partidoCreado.IdJugador1 = idUsuario;
                        partidoCreado.IdJugador2 = idUsuario;
                        pareja.IdJugador1 = idUsuario;
                        pareja.IdJugador2 = idUsuario;
                    }
                    else if (jugadoresRestantes == 1)
                    {
                        partidoCreado.IdJugador1 = idUsuario;
                        partidoCreado.IdJugador2 = idUsuario;
                        partidoCreado.IdJugador3 = idUsuario;
                        pareja.IdJugador1 = idUsuario;
                        pareja.IdJugador2 = idUsuario;
                        pareja.IdJugador3 = idUsuario;
                    }
                    partidoCreado.Parejas.Add(pareja);
                    canchasReservadas.PartidosCreadosUsuarios.Add(partidoCreado);
                    _HorariosRepository.Save(reserva);

                    // Creación de notificación
                    string detalle = "Partido creado " + day + "/" + month + "/" + year + " " + horarioDesde.Hour + ":" + horarioDesde.Minute + " " + horarioHasta.Hour + ":" + horarioHasta.Minute + " faltan " + jugadoresRestantes + " jugadores!";
                    bool notiCreada = _NotificacionBusiness.CrearNotificacion(NOTIFICACIONTIPO.PARTIDO_CREADO, detalle, partidoCreado.Id);
                    if (!notiCreada)
                    {
                        throw new Exception("Error al crear la notificación");
                    }

                    transactionScope.Complete();

                    return partidoCreado.Id;
                }
                catch (Exception ex)
                {
                    // Manejo de la excepción
                    // Por ejemplo, registrar el error o lanzar una excepción personalizada
                    // Logger.LogError("Error al crear partido", ex);
                    // transactionScope.Dispose(); // No es necesario, ya que estamos usando "using"
                    throw; // Relanzar la excepción para que sea manejada por el código que llama a este método
                }
            }
        }

        public bool DarDeBaja(int idCancha)
        {
            try
            {
                bool baja = false;

                CanchasReservadas reserva = _ReservasRepository.GetCanchaReservada(idCancha);
                if (reserva != null)
                {
                    reserva.Estado = ESTADO.BAJA;
                    _ReservasRepository.SaveCancha(reserva);
                    baja = true;
                }

                string fecha = reserva.Horarios.HorarioDesde.Value.Day.ToString() + "/" + reserva.Horarios.HorarioDesde.Value.Month.ToString() + "/" + reserva.Horarios.HorarioDesde.Value.Year.ToString();

                string horarioDesde = reserva.Horarios.HorarioDesde.Value.Hour.ToString() + ":" + reserva.Horarios.HorarioDesde.Value.Minute.ToString();
                string horarioHasta = reserva.Horarios.HorarioHasta.Value.Hour.ToString() + ":" + reserva.Horarios.HorarioHasta.Value.Minute.ToString();

                string detalle = "Se libero el dia: " + fecha + " desde: " + horarioDesde + " hasta: " + horarioHasta;

                bool notiCreada = _NotificacionBusiness.CrearNotificacion(NOTIFICACIONTIPO.HORARIO_BAJA, detalle);
                if (notiCreada == false)
                {
                    throw new Exception("Error notificacion");
                }
                return baja;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool FinalizarTurno(int idCancha, decimal importeCancha, decimal adicional)
        {
            try
            {
                bool finalizo = false;
                CanchasReservadas reserva = _ReservasRepository.GetCanchaReservada(idCancha);
                if (reserva != null)
                {
                    reserva.Estado = ESTADO.FINALIZADO;
                    _ReservasRepository.SaveCancha(reserva);
                }

                RecaudacionCancha recaudacion = new RecaudacionCancha
                {
                    FechaRecaudacion = DateTime.Now,
                    CanchaReservadaId = idCancha,
                    MontoCancha = importeCancha,
                    MontoAdicional = adicional,
                    MontoFinal = importeCancha + adicional
                };
                bool graboRecaudacion = _ReservasRepository.SaveRecaudacion(recaudacion);
                if (!graboRecaudacion)
                {
                    throw new Exception("Error al grabar");
                }
                else
                {
                    finalizo = true;
                }
                return finalizo;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReservaDTO> GetReservas()
        {
            return _ReservasRepository.GetReservas();
        }

        public List<ReservaDTO> GetReservasAdmin(DateTime fecha)
        {
            return _ReservasRepository.GetReservasAdmin(fecha);
        }

        public List<ReservaDTO> GetReservasByUsuario(int idUSuario)
        {
            return _ReservasRepository.GetReservasByUsuario(idUSuario);
        }

        public bool Reservar(int idCancha, string fechaSeleccionada, string horarioDeReserva, int duracion, int idUsuario)
        {

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                try
                {
                    string[] partes = fechaSeleccionada.Split('/');

                    int day = int.Parse(partes[0]);
                    int month = int.Parse(partes[1]);
                    int year = int.Parse(partes[2]);

                    DateTime fechaUsuario = new DateTime(year, month, day);

                    DateTime horarioDesde = fechaUsuario.Add(DateTime.ParseExact(horarioDeReserva, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
                    DateTime horarioHasta = horarioDesde.AddMinutes(duracion);

                    bool existeReserva = _HorariosRepository.ExisteHorarioByDesdeYDuracion(horarioDesde, duracion);
                    if (!existeReserva)
                    {
                        Horarios reserva = new Horarios();
                        CanchasReservadas canchasReservadas = new CanchasReservadas();
                        reserva.HorarioDesde = horarioDesde;
                        reserva.HorarioHasta = horarioHasta;
                        reserva.Duracion = duracion;
                        canchasReservadas.IdCancha = idCancha;
                        canchasReservadas.IdUsuario = idUsuario;
                        canchasReservadas.Estado = ESTADO.PENDIENTE;
                        reserva.CanchasReservadas.Add(canchasReservadas);

                        bool seReservo = _HorariosRepository.Save(reserva);

                        transactionScope.Complete();

                        return seReservo;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Problemas en el servidor");
                }
            }
        }
    }
}
