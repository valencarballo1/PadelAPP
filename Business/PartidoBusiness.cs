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
    public class PartidoBusiness
    {
        private PartidoRepository _PartidoRepository;

        public PartidoBusiness()
        {
            this._PartidoRepository = new PartidoRepository();
        }
        public PartidoDTO Get(int idPartido)
        {
            return _PartidoRepository.Get(idPartido);
        }
    }
}
