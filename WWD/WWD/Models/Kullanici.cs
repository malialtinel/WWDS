namespace WWD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Donates = new HashSet<Donate>();
            Kampanyas = new HashSet<Kampanya>();
        }

        public Guid KullaniciId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserAdi { get; set; }

        [StringLength(50)]
        public string KullaniciIsim { get; set; }

        [StringLength(50)]
        public string KullaniciSoyisim { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLeader { get; set; }

        public bool IsDonator { get; set; }

        [Required]
        [StringLength(150)]
        public string GizliSoru { get; set; }

        [Required]
        [StringLength(150)]
        public string GizliCevap { get; set; }

        [Required]
        [StringLength(150)]
        public string Parola { get; set; }

        [StringLength(250)]
        public string ProfilFoto { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        public virtual aspnet_Users aspnet_Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donate> Donates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kampanya> Kampanyas { get; set; }
    }
}
