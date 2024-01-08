using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? FkClassId { get; set; }

    public virtual Class? FkClass { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
