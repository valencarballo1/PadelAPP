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

        public List<HorarioDTO> Load(int idCancha,DateTime fechaSeleccionada)
        {
            return _HorariosRepository.Load(idCancha, fechaSeleccionada);
        }
    }
}
