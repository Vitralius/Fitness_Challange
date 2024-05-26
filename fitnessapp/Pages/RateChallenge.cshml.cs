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
    public class RateChallengeModel : PageModel
    {
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        bool flag = false;
        public RateChallengeModel (UserChallengeDatabaseContext UserDb)
        {
            UserChallengeDb = UserDb;
        }

        public void OnGet()
        {
          
        }
        public void OnPostRate(int id, int _rate)
        {
            var rating = new UserRate();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId!=null && id!=0 && _rate!=0)
            {
                rating.UserId = userId;
                rating.Rate = (short?)_rate;
                rating.ChallengeId = id;
                UserChallengeDb.UserRates.Add(rating);
                UserChallengeDb.SaveChanges();
                flag=true;
            }
            if (flag)
            {
                Response.Redirect("/AverageRatingList"); 
            }
            else{
                Page();
            }    
        }
    }
}
