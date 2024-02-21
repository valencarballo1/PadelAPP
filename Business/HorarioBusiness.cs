using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Business
{
    public class HorarioBusiness
    {
        private HorariosRepository _HorariosRepository;

        public HorarioBusiness()
        {
            this._HorariosRepository = new HorariosRepository();
        }

        public List<HorarioDTO> Load(int idCancha, string fechaSeleccionada)
        {
            string[] partes = fechaSeleccionada.Split('/');

            // Obtener los componentes de día, mes y año
            int day = int.Parse(partes[0]);
            int month = int.Parse(partes[1]);
            int year = int.Parse(partes[2]);

            DateTime fecha = new DateTime(year, month, day);

            return _HorariosRepository.Load(idCancha, fecha);
        }
    }
}
