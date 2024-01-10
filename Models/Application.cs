using System;
using System.Collections.Generic;

namespace dbhealthcare.Models;

public partial class Application
{
    public int Id { get; set; }

    public string? AppName { get; set; }

    public string? AppDesc { get; set; }

    public int? Clientid { get; set; }

    public virtual Client IdNavigation { get; set; } = null!;

    public virtual Template? Template { get; set; }
}
