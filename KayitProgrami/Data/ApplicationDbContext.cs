using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KayitProgrami.Models;
using Microsoft.AspNetCore.Identity;

namespace KayitProgrami.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        

     
        public DbSet<IzinTalebi> IzinTalepleri { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //      modelBuilder.Entity<IzinTalebi>()
            //.HasOne(i => i.Kullanici)
            //.WithMany(k => k.IzinTalepleri)
            //.HasForeignKey(i => i.KullaniciId);
            modelBuilder.Entity<IzinTalebi>()
            .HasOne(i => i.AspNetUsers)
            .WithMany(u => u.IzinTalepleri)
            .HasForeignKey(it => it.KullaniciId)
            .IsRequired();
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permissions");

                entity.Property(e => e.KullaniciId).HasColumnName("UserId");
                entity.Property(e => e.BaslangicTarihi).HasColumnName("StarDate");
                entity.Property(e => e.BitisTarihi).HasColumnName("EndDate");
                entity.Property(e => e.KalanIzin).HasColumnName("AnnualLeave");
                entity.Property(e => e.KullanilanIzin).HasColumnName("UsedLeave");
                entity.Property(e => e.Name).HasColumnName("Username");
            });

            modelBuilder.Entity<IzinTalebi>().ToTable("LeaveRequests");

            modelBuilder.Entity<IzinTalebi>(entity =>
            {
                entity.ToTable("LeaveRequests");

                entity.Property(e=>e.Aciklama).HasColumnName("Description");
                entity.Property(e=>e.IzinTarihiBaslangic).HasColumnName("LeaveDateStart");
                entity.Property(e=>e.IzinTarihiBitis).HasColumnName("LeaveDateEnd");
                entity.Property(e=>e.KullaniciId).HasColumnName("UserId");
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=OKAN\\SQLEXPRESS;Database=KayitProgramiDB;TrustServerCertificate=True;");
            }
        }
    }
}
