using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ClubBusiness
    {
        private ClubRepository _ClubRepository;

        public ClubBusiness()
        {
            _ClubRepository = new ClubRepository();
        }
        public List<DTO.ClubDTO> GetAll()
        {
            return _ClubRepository.GetAll();
        }
    }
}
