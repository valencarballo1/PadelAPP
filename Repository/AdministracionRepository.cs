using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdministracionRepository
    {
        public decimal RecaudacionAnual()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaActual = DateTime.Now;
                DateTime inicioAnio = new DateTime(fechaActual.Year, 1, 1);
                DateTime finAnio = new DateTime(fechaActual.Year, 12, 31, 23, 59, 59, 999);

                return db.RecaudacionCancha.Where(r => r.FechaRecaudacion >= inicioAnio && r.FechaRecaudacion <= finAnio).Sum(r => r.MontoFinal).Value;
            }
        }

        public decimal RecaudacionMensual()
        {
            using (PadelAppEntities db = new PadelAppEntities())
            {
                DateTime fechaActual = DateTime.Now;
                DateTime inicioMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddTicks(-1);

                return db.RecaudacionCancha.Where(r => r.FechaRecaudacion >= inicioMes && r.FechaRecaudacion <= finMes).Sum(r => r.MontoFinal).Value;
            }
        }
    }
}
