namespace WWD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stuff")]
    public partial class Stuff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stuff()
        {
            Donates = new HashSet<Donate>();
            Kampanyas = new HashSet<Kampanya>();
        }

        public int StuffId { get; set; }

        [Required]
        [StringLength(100)]
        public string StuffName { get; set; }

        [Required]
        [StringLength(10)]
        public string Birim { get; set; }

        public int Miktari { get; set; }

        public int KalanMiktar { get; set; }

        public int? KampanyaID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donate> Donates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kampanya> Kampanyas { get; set; }

        public virtual Kampanya Kampanya { get; set; }
    }
}
