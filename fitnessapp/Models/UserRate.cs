using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class UserRate
{
    public int RateId { get; set; }

    public string? UserId { get; set; }

    public int? ChallengeId { get; set; }

    public short? Rate { get; set; }
}
