﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESCHOOLING.DataAccess.EntityModel
{
    public partial class ECOM_WebContext : DbContext
    {
        public ECOM_WebContext()
        {
        }

        public ECOM_WebContext(DbContextOptions<ECOM_WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAttendance> TblAttendances { get; set; } = null!;
        public virtual DbSet<TblBehaviour> TblBehaviours { get; set; } = null!;
        public virtual DbSet<TblEvent> TblEvents { get; set; } = null!;
        public virtual DbSet<TblStudentMark> TblStudentMarks { get; set; } = null!;
        public virtual DbSet<TblUserRegistration> TblUserRegistrations { get; set; } = null!;
        public virtual DbSet<TblChatMessage> TblChatMessages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-FEVP5A4;Initial Catalog=ESDB;User ID=sa;Password=GPKK@Cin7");
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
                    .HasConstraintName("FK__TblAttend__Stude__76969D2E");
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
                    .HasConstraintName("FK__TblBehavi__Stude__7D439ABD");
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
                    .HasConstraintName("FK__TblStuden__Stude__7E37BEF6");
            });

            modelBuilder.Entity<TblUserRegistration>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__TblUserR__1788CC4C938262B3");

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

            modelBuilder.Entity<TblChatMessage>(entity =>
            {
                entity.ToTable("TblChatMessage");

                entity.HasKey(e => e.Id).HasName("PK_TblMessage");            

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FromUserId).HasColumnType("bigint");

                entity.Property(e => e.ToUserId).HasColumnType("bigint");

                entity.Property(e => e.HasRecieverRed).HasColumnType("bit");

                entity.Property(e => e.MessageText).IsUnicode(true);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
