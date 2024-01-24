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
            public string Celular { get; set; }
            public string FotoPerfil { get; set; }
        }

        public class PartidoDTO
        {
            public int Id { get; set; }
            public int CanchaNumero { get; set; }
            public DateTime HorarioDesde { get; set; }
            public DateTime HorarioHasta { get; set; }
            public string UsuarioJ1 { get; set; }
            public string UsuarioJ2 { get; set; }
            public string UsuarioJ3 { get; set; }
            public string UsuarioJ4 { get; set; }
            public string FotoPerfil1 { get; set; }
            public string FotoPerfil2 { get; set; }
            public string FotoPerfil3 { get; set; }
            public string FotoPerfil4 { get; set; }

        }

        public class DetallePartidoDTO
        {
            public int Id { get; set; }
            public int CanchaNumero { get; set; }
            public DateTime HorarioDesde { get; set; }
            public DateTime HorarioHasta { get; set; }
            public int CantidadJugadores { get; set; }
            public string UsuarioOrganizador { get; set; }
        }
    }
}
