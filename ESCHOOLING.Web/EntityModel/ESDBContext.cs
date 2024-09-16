using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESCHOOLING.Web.EntityModel
{
    public partial class ESDBContext : DbContext
    {
        public ESDBContext()
        {
        }

        public ESDBContext(DbContextOptions<ESDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAttendance> TblAttendances { get; set; } = null!;
        public virtual DbSet<TblBehaviour> TblBehaviours { get; set; } = null!;
        public virtual DbSet<TblEvent> TblEvents { get; set; } = null!;
        public virtual DbSet<TblStudentMark> TblStudentMarks { get; set; } = null!;
        public virtual DbSet<TblUserRegistration> TblUserRegistrations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-FEVP5A4;Initial Catalog=ESDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAttendance>(entity =>
            {
                entity.ToTable("TblAttendance");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MonthForSearch).HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblAttendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TblAttend__Stude__3F466844");
            });

            modelBuilder.Entity<TblBehaviour>(entity =>
            {
                entity.ToTable("TblBehaviour");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Behaviour1)
                    .HasMaxLength(50)
                    .HasColumnName("behaviour1");

                entity.Property(e => e.Behaviour2)
                    .HasMaxLength(50)
                    .HasColumnName("behaviour2");

                entity.Property(e => e.Behaviour3)
                    .HasMaxLength(50)
                    .HasColumnName("behaviour3");

                entity.Property(e => e.Behaviour4)
                    .HasMaxLength(50)
                    .HasColumnName("behaviour4");

                entity.Property(e => e.Behaviour5)
                    .HasMaxLength(50)
                    .HasColumnName("behaviour5");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblBehaviours)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__TblBehavi__Stude__403A8C7D");
            });

            modelBuilder.Entity<TblEvent>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EventName).HasMaxLength(50);

                entity.Property(e => e.Place).HasMaxLength(50);

                entity.Property(e => e.Time).HasMaxLength(50);
            });

            modelBuilder.Entity<TblStudentMark>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PredictedMark).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblStudentMarks)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__TblStuden__Stude__412EB0B6");
            });

            modelBuilder.Entity<TblUserRegistration>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__TblUserR__1788CC4C4E31E81F");

                entity.ToTable("TblUserRegistration");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
