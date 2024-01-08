using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? Classname { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
