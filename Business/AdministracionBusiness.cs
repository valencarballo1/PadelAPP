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
    public class AdministracionBusiness
    {
        private AdministracionRepository _AdministracionRepository;

        public AdministracionBusiness()
        {
            this._AdministracionRepository = new AdministracionRepository();
        }

        public List<decimal> GetRecaAllMeses()
        {
            return _AdministracionRepository.GetRecaAllMeses();
        }

        public decimal RecaudacionAnual()
        {
            return _AdministracionRepository.RecaudacionAnual();
        }

        public ReservasYExtras RecaudacionCanchaYExtras()
        {
            return _AdministracionRepository.RecaudacionCanchaYExtras();
        }

        public decimal RecaudacionMensual()
        {
            return _AdministracionRepository.RecaudacionMensual();
        }
    }
}
