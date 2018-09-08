namespace WWD.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DonationDB : DbContext
    {
        public DonationDB()
            : base("name=DonationDB")
        {
        }

        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_UsersInRoles> aspnet_UsersInRoles { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Donate> Donates { get; set; }
        public virtual DbSet<Kampanya> Kampanyas { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<Resim> Resims { get; set; }
        public virtual DbSet<Stuff> Stuffs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_UsersInRoles)
                .WithRequired(e => e.aspnet_Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.aspnet_UsersInRoles)
                .WithRequired(e => e.aspnet_Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.aspnet_UsersInRoles1)
                .WithRequired(e => e.aspnet_Users1)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.Kullanici)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.Kullanici1)
                .WithRequired(e => e.aspnet_Users1)
                .WillCascadeOnDelete();

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<Donate>()
                .HasMany(e => e.Kampanyas)
                .WithOptional(e => e.Donate)
                .HasForeignKey(e => e.DonateID);

            modelBuilder.Entity<Kampanya>()
                .HasMany(e => e.Donates)
                .WithRequired(e => e.Kampanya)
                .HasForeignKey(e => e.KampanyaID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kampanya>()
                .HasMany(e => e.Resims)
                .WithOptional(e => e.Kampanya)
                .HasForeignKey(e => e.KampanyaID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Kampanya>()
                .HasMany(e => e.Stuffs)
                .WithOptional(e => e.Kampanya)
                .HasForeignKey(e => e.KampanyaID);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Kampanyas)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.Parola)
                .IsFixedLength();

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Donates)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.GonderenID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Kampanyas)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.Baslatan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resim>()
                .HasMany(e => e.Kampanyas)
                .WithOptional(e => e.Resim)
                .HasForeignKey(e => e.ResimID);

            modelBuilder.Entity<Stuff>()
                .Property(e => e.Birim)
                .IsFixedLength();

            modelBuilder.Entity<Stuff>()
                .HasMany(e => e.Donates)
                .WithRequired(e => e.Stuff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stuff>()
                .HasMany(e => e.Kampanyas)
                .WithOptional(e => e.Stuff)
                .HasForeignKey(e => e.StuffID)
                .WillCascadeOnDelete();
        }
    }
}
