using System;
using System.Collections.Generic;

namespace ProjectSchool2.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string? MenuItemName { get; set; }

    public string? Action { get; set; }
}
