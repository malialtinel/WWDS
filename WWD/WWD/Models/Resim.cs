namespace WWD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resim")]
    public partial class Resim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resim()
        {
            Kampanyas = new HashSet<Kampanya>();
        }

        public int ResimId { get; set; }

        [Required]
        [StringLength(350)]
        public string ResimKucukYol { get; set; }

        [Required]
        [StringLength(350)]
        public string ResimBuyukYol { get; set; }

        public int? KampanyaID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kampanya> Kampanyas { get; set; }

        public virtual Kampanya Kampanya { get; set; }
    }
}
