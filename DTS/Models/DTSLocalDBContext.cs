using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DTS.Models
{
    public partial class DTSLocalDBContext : DbContext
    {
        public DTSLocalDBContext()
        {
        }

        public DTSLocalDBContext(DbContextOptions<DTSLocalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TemplateState> TemplateState { get; set; }
        public virtual DbSet<TemplateVersionControl> TemplateVersionControll { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DTSLocalDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("template");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.template_dbo.user_id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.template_dbo.template_state_id");
            });

            modelBuilder.Entity<TemplateState>(entity =>
            {
                entity.ToTable("template_state");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateVersionControl>(entity =>
            {
                entity.ToTable("template_version_controll");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.Template)
                    .IsRequired()
                    .HasColumnName("template")
                    .HasColumnType("ntext");

                entity.Property(e => e.TemplateId).HasColumnName("template_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TemplateVersionControll)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.template_version_controll_dbo.user_id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TemplateVersionControll)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.template_version_controll_dbo.template_state_id");

                entity.HasOne(d => d.TemplateNavigation)
                    .WithMany(p => p.TemplateVersionControll)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.template_version_controll_dbo.template_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(30);

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.user_dbo.user_status_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.user_dbo.user_type_id");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("user_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
