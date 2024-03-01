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
    public class CategoriaBusiness
    {
        private CategoriaRepository _CategoriaRepository;

        public CategoriaBusiness()
        {
            this._CategoriaRepository = new CategoriaRepository();
        }
        public List<CategoriaDTO> GetAll()
        {
            return _CategoriaRepository.GetAll();
        }
    }
}
