using System;
using System.Collections.Generic;

namespace fitnessapp.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public string? UserId { get; set; }

    public int? ChallengeId { get; set; }

    public bool? IsDeleted { get; set; }
}
