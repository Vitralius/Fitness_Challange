using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class UserDetail
{
    public int DetailId { get; set; }

    public string? City { get; set; }

    public byte[]? Photo { get; set; }

    public string? UserId { get; set; }

    public string? Bio { get; set; }
}
