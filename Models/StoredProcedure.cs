using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class StoredProcedure
{
    public int ProcedureId { get; set; }

    public string? ProcedureName { get; set; }

    public string? ProcedureDescription { get; set; }
}
