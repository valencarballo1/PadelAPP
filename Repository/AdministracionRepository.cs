using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.DTO;

namespace Repository
{
    public class AdministracionRepository
    {
        public List<decimal> GetRecaAllMeses()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                List<decimal> recaudaciones = new List<decimal>();

                for (int mes = 1; mes <= 12; mes++)
                {
                    DateTime inicioMes = new DateTime(DateTime.Now.Year, mes, 1);
                    DateTime finMes = inicioMes.AddMonths(1).AddTicks(-1);

                    decimal recaudacionMes = db.RecaudacionCancha
                        .Where(r => r.FechaRecaudacion >= inicioMes && r.FechaRecaudacion <= finMes)
                        .Sum(r => r.MontoFinal) ?? 0m;

                    recaudaciones.Add(recaudacionMes);
                }

                return recaudaciones;
            }
        }

        public decimal RecaudacionAnual()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaActual = DateTime.Now;
                DateTime inicioAnio = new DateTime(fechaActual.Year, 1, 1);
                DateTime finAnio = new DateTime(fechaActual.Year, 12, 31, 23, 59, 59, 999);
                decimal? total = db.RecaudacionCancha
                    .Where(r => r.FechaRecaudacion >= inicioAnio && r.FechaRecaudacion <= finAnio)
                    .Sum(r => r.MontoFinal) ?? 0;
                return total.Value;
            }
        }

        public ReservasYExtras RecaudacionCanchaYExtras()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                ReservasYExtras reservasYExtras = new ReservasYExtras();
                DateTime fechaActual = DateTime.Now;
                DateTime inicioMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddTicks(-1);

                decimal? totalCancha = db.RecaudacionCancha
                    .Where(r => r.FechaRecaudacion >= inicioMes && r.FechaRecaudacion <= finMes)
                    .Sum(r => r.MontoCancha) ?? 0;

                decimal? totalExtra = db.RecaudacionCancha
                    .Where(r => r.FechaRecaudacion >= inicioMes && r.FechaRecaudacion <= finMes)
                    .Sum(r => r.MontoAdicional) ?? 0;

                reservasYExtras.TotalMontoCancha = totalCancha.Value;
                reservasYExtras.TotalMontoAdicional = totalExtra.Value;
                return reservasYExtras;
            }
        }

        public decimal RecaudacionMensual()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaActual = DateTime.Now;
                DateTime inicioMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddTicks(-1);
                decimal? total = db.RecaudacionCancha
                    .Where(r => r.FechaRecaudacion >= inicioMes && r.FechaRecaudacion <= finMes)
                    .Sum(r => r.MontoFinal) ?? 0;
                
                return total.Value;
            }
        }
    }
}
