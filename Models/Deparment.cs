using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Deparment
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
