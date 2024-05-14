using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class TblChallange
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? EndDate { get; set; }

    public string? UserId { get; set; }
}
