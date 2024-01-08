using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectSchool2.Models;

public partial class ProjectSchoolContext : DbContext
{
    public ProjectSchoolContext()
    {
    }

    public ProjectSchoolContext(DbContextOptions<ProjectSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Deparment> Deparments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<StoredProcedure> StoredProcedures { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ProjectSchool; Integrated Security = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("ClassID");
            entity.Property(e => e.Classname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Deparment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("Deparment");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First Name");
            entity.Property(e => e.FkDepartmentId).HasColumnName("FK_DepartmentID");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last Name");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkDepartment).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkDepartmentId)
                .HasConstraintName("FK_Employee_Deparment");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.ToTable("Grade");

            entity.Property(e => e.GradeId)
                .ValueGeneratedNever()
                .HasColumnName("GradeID");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.FkTeacherId).HasColumnName("FK_TeacherID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_Grade_Course");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_Grade_Student");

            entity.HasOne(d => d.FkTeacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkTeacherId)
                .HasConstraintName("FK_Grade_Employee");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.MenuId)
                .ValueGeneratedNever()
                .HasColumnName("MenuID");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuItemName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId).HasColumnName("SalaryID");
            entity.Property(e => e.FkDepartmentId).HasColumnName("FK_DepartmentID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK_Salary_Employee");
        });

        modelBuilder.Entity<StoredProcedure>(entity =>
        {
            entity.HasKey(e => e.ProcedureId);

            entity.ToTable("StoredProcedure");

            entity.Property(e => e.ProcedureId)
                .ValueGeneratedNever()
                .HasColumnName("ProcedureID");
            entity.Property(e => e.ProcedureDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProcedureName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First Name");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last Name");

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_Student_Class");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.FkTeacherId).HasColumnName("FK_TeacherID");
            entity.Property(e => e.Grade)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_Transaction_Course");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_Transaction_Student");

            entity.HasOne(d => d.FkTeacher).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkTeacherId)
                .HasConstraintName("FK_Transaction_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
