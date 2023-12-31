//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STGenetics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Animal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animal()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
        }
    
        public int AnimalId { get; set; }
        public int BreedId { get; set; }
        public int StatusId { get; set; }
        public int SexId { get; set; }
        public string Name { get; set; }
        public System.DateTime Birthdate { get; set; }
        public double Price { get; set; }
    
        public virtual Breed Breed { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
