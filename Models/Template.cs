using System;
using System.Collections.Generic;

namespace dbhealthcare.Models;

public partial class Template
{
    public int Id { get; set; }

    public string? TempName { get; set; }

    public int? AppId { get; set; }

    public virtual Application IdNavigation { get; set; } = null!;
}
