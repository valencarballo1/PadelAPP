using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DTO
    {
        public class HorarioDTO
        {
            public int Id { get; set; }
            public DateTime HorarioDesde { get; set; }
            public DateTime HorarioHasta { get; set; }
            public int Duracion { get; set; }
        }

        public class ReservaDTO
        {
            public int Id { get; set; }
            public int CanchaNumero { get; set; }
            public DateTime HorarioDesde { get; set; }
            public DateTime HorarioHasta { get; set; }
        }

        public class UsuarioDTO
        {
            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Celular {  get; set; }
            public string FotoPerfil { get; set; }
        }
    }
}
