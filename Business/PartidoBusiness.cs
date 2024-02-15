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
            return _PartidoRepository.GetDTO(idPartido);
        }

        public bool Unirse(int idPartido, int idUsuario, int posicionJugador)
        {
            bool seUnio = false;
            PartidosCreadosUsuarios partido = _PartidoRepository.Get(idPartido);

            if(posicionJugador == 2)
            {
                if(partido.IdJugador2 == null)
                {
                    partido.IdJugador2 = idUsuario;
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            if (posicionJugador == 3)
            {
                if (partido.IdJugador3 == null)
                {
                    partido.IdJugador3 = idUsuario;
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            if (posicionJugador == 4)
            {
                if (partido.IdJugador4 == null)
                {
                    partido.IdJugador4 = idUsuario;
                    _PartidoRepository.Save(partido);
                    seUnio = true;
                }
            }

            return seUnio;
        }

        public List<DetallePartidoDTO> GetDetallePartidos()
        {
            return _PartidoRepository.GetDetallePartido();
        }
    }
}
