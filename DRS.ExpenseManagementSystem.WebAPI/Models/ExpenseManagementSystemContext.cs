using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class ExpenseManagementSystemContext : DbContext
    {
        public ExpenseManagementSystemContext()
        {
        }

        public ExpenseManagementSystemContext(DbContextOptions<ExpenseManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimStatus> ClaimStatuses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ExpensesCategory> ExpensesCategories { get; set; } = null!;
        public virtual DbSet<ExpensesClaim> ExpensesClaims { get; set; } = null!;
        public virtual DbSet<IndividualExpenditure> IndividualExpenditures { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<UserTable> UserTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0F310TS\\SQLEXPRESS;Initial Catalog=ExpenseManagementSystem;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimStatus>(entity =>
            {
                entity.ToTable("ClaimStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClaimState)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__Departme__014881AE40981412");

                entity.ToTable("Department");

                entity.Property(e => e.DeptId).ValueGeneratedNever();

                entity.Property(e => e.DepartementName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBA7945B9B4AD");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmpID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DateofJoining).HasColumnType("datetime");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.EmpAccountNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmpBankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDesignation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpEmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmpEmailID");

                entity.Property(e => e.EmpFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpIfsc)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("EmpIFSC");

                entity.Property(e => e.EmpLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPanNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__Employees__DeptI__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Employees__UserI__3A81B327");
            });

            modelBuilder.Entity<ExpensesCategory>(entity =>
            {
                entity.ToTable("ExpensesCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpensesClaim>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("PK__Expenses__EF2E139B7E7B2702");

                entity.ToTable("ExpensesClaim");

                entity.Property(e => e.ClaimId).ValueGeneratedNever();

                entity.Property(e => e.ApprovedByFinanceManager).HasColumnType("datetime");

                entity.Property(e => e.ApprovedByManager).HasColumnType("datetime");

                entity.Property(e => e.ClaimingDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.PeriodEnd).HasColumnType("datetime");

                entity.Property(e => e.PeriodStart).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.RemarksFinanceManager)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksManager)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.ExpensesClaims)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__ExpensesC__DeptI__440B1D61");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.ExpensesClaims)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__ExpensesC__EmpID__4316F928");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ExpensesClaims)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ExpensesC__Proje__44FF419A");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ExpensesClaims)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__ExpensesC__Statu__45F365D3");
            });

            modelBuilder.Entity<IndividualExpenditure>(entity =>
            {
                entity.ToTable("IndividualExpenditure");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BillingDate).HasColumnType("datetime");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ExpenseCategoryId).HasColumnName("ExpenseCategoryID");

                entity.Property(e => e.InvoiceDocument)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PurposeofClaim)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.IndividualExpenditures)
                    .HasForeignKey(d => d.ClaimId)
                    .HasConstraintName("FK__Individua__Claim__4AB81AF0");

                entity.HasOne(d => d.ExpenseCategory)
                    .WithMany(p => p.IndividualExpenditures)
                    .HasForeignKey(d => d.ExpenseCategoryId)
                    .HasConstraintName("FK__Individua__Expen__4BAC3F29");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.ProjectEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ProjectEnd_Date");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ProjectStart_Date");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Project__EmpID__3E52440B");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.ToTable("UserTable");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EmployeeId)
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
