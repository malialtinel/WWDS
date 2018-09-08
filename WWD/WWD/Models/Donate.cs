namespace WWD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donate")]
    public partial class Donate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donate()
        {
            Kampanyas = new HashSet<Kampanya>();
        }

        public int DonateId { get; set; }

        public Guid GonderenID { get; set; }

        public int GonderiMiktari { get; set; }

        public bool GonderdiMi { get; set; }

        public bool VardiMi { get; set; }

        [StringLength(50)]
        public string KargoTakipNo { get; set; }

        public int StuffID { get; set; }

        public bool BasladiMi { get; set; }

        public int KampanyaID { get; set; }

        public virtual Kampanya Kampanya { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Stuff Stuff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kampanya> Kampanyas { get; set; }
    }
}
