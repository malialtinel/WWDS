namespace WWD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kampanya")]
    public partial class Kampanya
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kampanya()
        {
            Donates = new HashSet<Donate>();
            Resims = new HashSet<Resim>();
            Stuffs = new HashSet<Stuff>();
        }

        public int KampanyaId { get; set; }

        [Required]
        [StringLength(250)]
        public string Adi { get; set; }

        public string Aciklama { get; set; }

        public Guid Baslatan { get; set; }

        public DateTime? BaslamaTarihi { get; set; }

        public int? StuffID { get; set; }

        public int KategoriID { get; set; }

        public bool? OnaylandiMi { get; set; }

        [Required]
        [StringLength(500)]
        public string Adres { get; set; }

        [Required]
        [StringLength(100)]
        public string Sehir { get; set; }

        [Required]
        [StringLength(150)]
        public string Ulke { get; set; }

        public bool? BasariliMi { get; set; }

        [StringLength(350)]
        public string KampanyaFoto { get; set; }

        [StringLength(350)]
        public string KampanyaFotoB { get; set; }

        [Required]
        [StringLength(50)]
        public string PostalCode { get; set; }

        public int? DonateID { get; set; }

        public int? ResimID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donate> Donates { get; set; }

        public virtual Donate Donate { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Resim Resim { get; set; }

        public virtual Stuff Stuff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stuff> Stuffs { get; set; }
    }
}
