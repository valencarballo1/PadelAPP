﻿using System;
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
            public int Duracion { get; set; }
            public string UsuarioNombre { get; set; }
        }

        public class UsuarioDTO
        {
            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Celular { get; set; }
            public string FotoPerfil { get; set; }
            public string Categoria { get; set; }
            public decimal Puntuacion { get; set; }
            public bool EsAdmin { get; set; }
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
            public string Categoria1 { get; set; }
            public decimal Puntuacion1 { get; set; }
            public string Categoria2 { get; set; }
            public decimal? Puntuacion2 { get; set; }
            public string Categoria3 { get; set; }
            public decimal? Puntuacion3 { get; set; }
            public string Categoria4 { get; set; }
            public decimal? Puntuacion4 { get; set; }
            public int IdUsuarioOrganiza { get; set; }
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

        public class CategoriaDTO
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public double Puntuacion { get; set; }
        }

        public class ParejaDTO
        {
            public int Id { get; set; }
            public string Usuario1 { get; set; }
            public string Usuario2 { get; set; }
            public string Usuario3 { get; set; }
            public string Usuario4 { get; set; }

        }

        public class ResultadoDTO
        {
            public int set1Pareja1 { get; set; }
            public int set1Pareja2 { get; set; }
            public int set2Pareja1 { get; set; }
            public int set2Pareja2 { get; set; }
            public int set3Pareja1 { get; set; }
            public int set3Pareja2 { get; set; }
        }

        public class RankingDTO
        {
            public int Posicion { get; set; }
            public string Usuario { get; set; }
            public string FotoPerfil { get; set; }
            public decimal Puntuacion { get; set; }
        }

        public class ReservasYExtras
        {
            public decimal TotalMontoCancha { get; set; }
            public decimal TotalMontoAdicional { get; set; }
        }

        public class NotificacionesDTO
        {
            public int IdNotificacion { get; set; }
            public string Detalle { get; set; }
            public int IdCanchaReservada { get; set; }
            public int TipoNotificacion { get; set; }
        }
    }
}
