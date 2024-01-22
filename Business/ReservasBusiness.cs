using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int CrearPartido(int idUsuario, int idCancha, DateTime fechaSeleccionada, string horarioDeReserva, int duracion, int jugadoresRestantes)
        {
            DateTime horarioDesde = fechaSeleccionada.Add(DateTime.ParseExact(horarioDeReserva, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
            DateTime horarioHasta = horarioDesde.AddMinutes(duracion);
            int i = 0;

            Horarios reserva = new Horarios();
            CanchasReservadas canchasReservadas = new CanchasReservadas();
            PartidosCreadosUsuarios partidoCreado = new PartidosCreadosUsuarios();

            reserva.HorarioDesde = horarioDesde;
            reserva.HorarioHasta = horarioHasta;
            reserva.Duracion = duracion;

            canchasReservadas.IdCancha = idCancha;
            reserva.CanchasReservadas.Add(canchasReservadas);

            while (jugadoresRestantes > i)
            {
                if(i == 0)
                {
                    partidoCreado.IdJugador1 = idUsuario;
                }else if(i == 1)
                {
                    partidoCreado.IdJugador2 = idUsuario;

                }else if(i == 2)
                {
                    partidoCreado.IdJugador3 = idUsuario;
                }
                i++;
            }

            canchasReservadas.PartidosCreadosUsuarios.Add(partidoCreado);
            _HorariosRepository.Save(reserva);

            return partidoCreado.Id;
        }

        public List<DTO.ReservaDTO> GetReservas()
        {
            return _ReservasRepository.GetReservas();
        }

        public bool Reservar(int idCancha, DateTime fechaSeleccionada, string horarioDeReserva, int duracion)
        {
            DateTime horarioDesde = fechaSeleccionada.Add(DateTime.ParseExact(horarioDeReserva, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
            DateTime horarioHasta = horarioDesde.AddMinutes(duracion);

            Horarios reserva = new Horarios();
            CanchasReservadas canchasReservadas = new CanchasReservadas();
            reserva.HorarioDesde = horarioDesde;
            reserva.HorarioHasta = horarioHasta;
            reserva.Duracion = duracion;
            canchasReservadas.IdCancha = idCancha;
            reserva.CanchasReservadas.Add(canchasReservadas);
            _HorariosRepository.Save(reserva);


            return true;
        }
    }
}
