//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notificaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notificaciones()
        {
            this.LecturasNotificaciones = new HashSet<LecturasNotificaciones>();
        }
    
        public int IdNotificacion { get; set; }
        public Nullable<int> TipoNotificacion { get; set; }
        public string Detalle { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<int> idCanchaReservada { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LecturasNotificaciones> LecturasNotificaciones { get; set; }
    }
}
