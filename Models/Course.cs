using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
