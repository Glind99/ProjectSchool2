using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int? FkEmployeeId { get; set; }

    public int? FkDepartmentId { get; set; }

    public int? SalaryAmount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public virtual Employee? FkEmployee { get; set; }
}
