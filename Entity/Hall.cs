//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaHallSimulation.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hall
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hall()
        {
            this.Showtimes = new HashSet<Showtimes>();
        }
    
        public int HallId { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
    
        public virtual Movie Movie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Showtimes> Showtimes { get; set; }
    }
}