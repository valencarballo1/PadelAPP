using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AdministracionBusiness
    {
        private AdministracionRepository _AdministracionRepository;

        public AdministracionBusiness()
        {
            this._AdministracionRepository = new AdministracionRepository();
        }

        public decimal RecaudacionAnual()
        {
            return _AdministracionRepository.RecaudacionAnual();
        }

        public decimal RecaudacionMensual()
        {
            return _AdministracionRepository.RecaudacionMensual();
        }
    }
}
