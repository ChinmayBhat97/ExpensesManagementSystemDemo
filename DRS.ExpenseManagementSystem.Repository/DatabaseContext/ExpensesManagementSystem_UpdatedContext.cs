using System;
using System.Collections.Generic;
using DRS.ExpenseManagementSystem.Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class ExpensesManagementSystem_UpdatedContext : DbContext
    {
        public ExpensesManagementSystem_UpdatedContext()
        {
        }

        public ExpensesManagementSystem_UpdatedContext(DbContextOptions<ExpensesManagementSystem_UpdatedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimStatus> ClaimStatuses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public virtual DbSet<ExpenseClaim> ExpenseClaims { get; set; } = null!;
        public virtual DbSet<IndividualExpenditure> IndividualExpenditures { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0F310TS\\SQLEXPRESS;Initial Catalog=ExpensesManagementSystem_Updated;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimStatus>(entity =>
            {
                entity.ToTable("ClaimStatus");

                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                //entity.ToTable("Department");

                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsc)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PanNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__Employees__DeptI__2C3393D0");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Employees__EmpId__2B3F6F97");
            });

            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.ToTable("ExpenseCategory");

                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpenseClaim>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.ClaimRequestDate).HasColumnType("datetime");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FinanceManagerApprovedOn).HasColumnType("datetime");

                entity.Property(e => e.FinanceManagerRemarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerApprovedOn).HasColumnType("datetime");

                entity.Property(e => e.ManagerRemarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.ExpenseClaims)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__ExpenseCl__DeptI__49C3F6B7");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.ExpenseClaims)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__ExpenseCl__EmpID__48CFD27E");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ExpenseClaims)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ExpenseCl__Proje__4AB81AF0");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.ExpenseClaims)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK__ExpenseCl__Statu__4BAC3F29");
            });

            modelBuilder.Entity<IndividualExpenditure>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AttachmentPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ExpenditureDate).HasColumnType("datetime");

                entity.Property(e => e.ExpenseCategoryId).HasColumnName("ExpenseCategoryID");

                entity.Property(e => e.FinanceManagerRemarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.ReceiptNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.IndividualExpenditures)
                    .HasForeignKey(d => d.ClaimId)
                    .HasConstraintName("FK__Individua__Claim__4E88ABD4");

                entity.HasOne(d => d.ExpenseCategory)
                    .WithMany(p => p.IndividualExpenditures)
                    .HasForeignKey(d => d.ExpenseCategoryId)
                    .HasConstraintName("FK__Individua__Expen__4F7CD00D");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.Client)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Projects__EmpID__398D8EEE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("ID");

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
