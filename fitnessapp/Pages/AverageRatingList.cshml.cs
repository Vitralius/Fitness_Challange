using System.Security.Claims;
using fitnessapp.Data;
using fitnessapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace fitnessapp.Namespace
{
    [Authorize]
    public class AverageRatingListModel : PageModel
    {
        [BindProperty]
        public TblChallenge NewTblChallenge { get; set; } = default!;
        private readonly ChallengeDatabaseContext ChallengeDb;
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        public List<TblChallenge> tblChallengeList { get; set; } = default!;
        public List<AverageRating> AverageRatings { get;set; } = default!;
        public List<UserRate> UserRates { get;set; } = default!;
        public List<float> Ratings { get;set;} = default!;
        public string UserId { get; set; } = default!;
        public AverageRatingListModel (UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;
        }

        public void OnGet()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;

            tblChallengeList = (from item in ChallengeDb.TblChallenges
                     where item.IsDeleted == false
                     select item).ToList();
            UserRates = (from item in UserChallengeDb.UserRates
                     select item).ToList();


            // Calculate average rating for each product
            AverageRatings = (from challenge in tblChallengeList
                                 join rate in UserRates on challenge.ChallengeId equals rate.ChallengeId into gj
                                 from subRate in gj.DefaultIfEmpty()
                                 group subRate by challenge into g
                                 select new AverageRating
                                 {
                                    TodoId = g.Key.ChallengeId,
                                    Average = g.Any() ? (float)g.Average(r => r?.Rate ?? 0) : 0
                                }).ToList();
            
             Ratings = AverageRatings.Select(x => x.Average).ToList();
        }
    }
}
