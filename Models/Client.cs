using System;
using System.Collections.Generic;

namespace dbhealthcare.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual Application? Application { get; set; }
}
