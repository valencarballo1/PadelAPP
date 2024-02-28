using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Business
{
    public class ReservasBusiness
    {
        private ReservasRepository _ReservasRepository;
        private HorariosRepository _HorariosRepository;

        public ReservasBusiness()
        {
            this._ReservasRepository = new ReservasRepository();
            this._HorariosRepository = new HorariosRepository();
        }

        public int CrearPartido(int idUsuario, int idCancha, string fechaSeleccionada, string horarioDeReserva, int duracion, int jugadoresRestantes)
        {
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

            reserva.HorarioDesde = horarioDesde;
            reserva.HorarioHasta = horarioHasta;
            reserva.Duracion = duracion;

            canchasReservadas.IdCancha = idCancha;
            canchasReservadas.IdUsuario = idUsuario;
            reserva.CanchasReservadas.Add(canchasReservadas);

            if (jugadoresRestantes == 3)
            {
                partidoCreado.IdJugador1 = idUsuario;
            }
            else if (jugadoresRestantes == 2)
            {
                partidoCreado.IdJugador1 = idUsuario;
                partidoCreado.IdJugador2 = idUsuario;

            }
            else if (jugadoresRestantes == 1)
            {
                partidoCreado.IdJugador1 = idUsuario;
                partidoCreado.IdJugador2 = idUsuario;
                partidoCreado.IdJugador3 = idUsuario;
            }

            canchasReservadas.PartidosCreadosUsuarios.Add(partidoCreado);
            _HorariosRepository.Save(reserva);

            return partidoCreado.Id;
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
            string[] partes = fechaSeleccionada.Split('/');

            int day = int.Parse(partes[0]);
            int month = int.Parse(partes[1]);
            int year = int.Parse(partes[2]);

            DateTime fechaUsuario = new DateTime(year, month, day);

            DateTime horarioDesde = fechaUsuario.Add(DateTime.ParseExact(horarioDeReserva, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
            DateTime horarioHasta = horarioDesde.AddMinutes(duracion);

            Horarios reserva = new Horarios();
            CanchasReservadas canchasReservadas = new CanchasReservadas();
            reserva.HorarioDesde = horarioDesde;
            reserva.HorarioHasta = horarioHasta;
            reserva.Duracion = duracion;
            canchasReservadas.IdCancha = idCancha;
            canchasReservadas.IdUsuario = idUsuario;
            reserva.CanchasReservadas.Add(canchasReservadas);
            _HorariosRepository.Save(reserva);


            return true;
        }
    }
}
