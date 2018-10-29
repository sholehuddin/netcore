using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DashboardPenggunaBMN.Models
{
    public partial class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aplikasi> Aplikasi { get; set; }
        public virtual DbSet<AplikasiFavorit> AplikasiFavorit { get; set; }
        public virtual DbSet<Layanan> Layanan { get; set; }
        public virtual DbSet<Notifikasi> Notifikasi { get; set; }
        public virtual DbSet<SlideShow> SlideShow { get; set; }
        public virtual DbSet<UserExecutive> UserExecutive { get; set; }
        public virtual DbSet<SampleData> SampleData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aplikasi>(entity =>
            {
                entity.HasKey(e => e.Idaplikasi);

                entity.Property(e => e.Idaplikasi).HasColumnName("IDAplikasi");

                entity.Property(e => e.Color)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Deskripsi).HasColumnType("text");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idlayanan).HasColumnName("IDLayanan");

                entity.Property(e => e.Judul)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdlayananNavigation)
                    .WithMany(p => p.Aplikasi)
                    .HasForeignKey(d => d.Idlayanan)
                    .HasConstraintName("FK__Aplikasi__IDLaya__7EF6D905");

				entity.Ignore(a => a.IconData);
			});

            modelBuilder.Entity<Layanan>(entity =>
            {
                entity.HasKey(e => e.Idlayanan);

                entity.Property(e => e.Idlayanan).HasColumnName("IDLayanan");

                entity.Property(e => e.Deskripsi).HasColumnType("text");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Judul)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AplikasiFavorit>(entity =>
            {
                entity.HasKey(e => e.IdaplikasiFavorit);

                entity.Property(e => e.IdaplikasiFavorit).HasColumnName("IDAplikasiFavorit");

                entity.Property(e => e.Idaplikasi).HasColumnName("IDAplikasi");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.Aplikasi)
                    .WithMany(p => p.AplikasiFavorit)
                    .HasForeignKey(d => d.Idaplikasi)
                    .HasConstraintName("FK_AplikasiFavorit_IDAplikasi");
            });

            modelBuilder.Entity<Notifikasi>(entity =>
            {
                entity.Property(e => e.NotifikasiId).HasColumnName("NotifikasiID");

                entity.Property(e => e.ActionLink)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dismissable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isi).HasColumnType("text");

                entity.Property(e => e.JabatanID).HasColumnName("JabatanID");

                entity.Property(e => e.Judul)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PegawaiID).HasColumnName("PegawaiID");

                entity.Property(e => e.RefSistemID).HasColumnName("RefSistemID");

                entity.Property(e => e.SourceUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusID).HasColumnName("StatusID");

                entity.Property(e => e.Topic)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.RefSistem)
                    .WithMany(p => p.Notifikasi)
                    .HasForeignKey(d => d.RefSistemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notifikas__RefSi__7FEAFD3E");
            });

            modelBuilder.Entity<UserExecutive>(entity =>
            {
                entity.Property(e => e.UserExecutiveId).HasColumnName("UserExecutiveID");

                entity.Property(e => e.AplikasiId).HasColumnName("AplikasiID");

                entity.Property(e => e.PegawaiId).HasColumnName("PegawaiID");

                entity.HasOne(d => d.Aplikasi)
                    .WithMany(p => p.UserExecutive)
                    .HasForeignKey(d => d.AplikasiId)
                    .HasConstraintName("FK_UserExecutive_AplikasiID");
            });

            modelBuilder.Entity<SlideShow>(entity =>
            {
                entity.Property(e => e.SlideShowId).HasColumnName("SlideShowID");

                entity.Property(e => e.Deskripsi)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Judul)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
				entity.Ignore(e => e.ImageSlide);
			});

            modelBuilder.Entity<SampleData>(entity =>
            {
                entity.HasKey(e => e.QuoteId);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quote)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
