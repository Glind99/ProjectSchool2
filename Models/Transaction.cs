﻿using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? FkStudentId { get; set; }

    public int? FkCourseId { get; set; }

    public int? FkTeacherId { get; set; }

    public string? Grade { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Student? FkStudent { get; set; }

    public virtual Employee? FkTeacher { get; set; }
}
