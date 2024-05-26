using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class TblChallenge
{
    public int ChallengeId { get; set; }

    public int? ParentId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? EndDate { get; set; }
}
