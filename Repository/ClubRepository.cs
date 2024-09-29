using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class ClubRepository
    {
        public List<ClubDTO> GetAll()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<ClubDTO> lista = new List<ClubDTO>();
                List<Club> clubes = db.Club.Include("ClubDetalle").ToList();

                clubes.ForEach(c =>
                {
                    ClubDTO clubDTO = new ClubDTO
                    {
                        IdClub = c.Id,
                        Nombre = c.Nombre,
                        Image = c.ClubDetalle.Imagen
                    };

                    lista.Add(clubDTO);
                });

                return lista;
            }
        }
    }
}
