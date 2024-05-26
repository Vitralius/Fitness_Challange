using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class Participate
{
    public int ParticipateId { get; set; }

    public int? ChallengeId { get; set; }

    public string? UserId { get; set; }

    public bool? IsDeleted { get; set; }
}
