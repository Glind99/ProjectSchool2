using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public DateOnly? YearsOfWork { get; set; }

    public int? FkDepartmentId { get; set; }

    public virtual Deparment? FkDepartment { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
