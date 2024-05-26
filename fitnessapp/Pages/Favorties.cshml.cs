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
    public class FavoritesModel : PageModel
    {
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        private readonly ChallengeDatabaseContext ChallengeDb;
        public List<TblChallenge> tblChallengeList { get; set; } = default!;
        public List<Favorite> favoriteList { get; set; } = default!;
        public string UserId { get; set; } = default!;

        public FavoritesModel (UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;        }

        public void OnGet()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            tblChallengeList = (from item in ChallengeDb.TblChallenges
                     select item).ToList(); 
            favoriteList = (from item in UserChallengeDb.Favorites
                    where item.UserId == UserId
                    where item.IsDeleted == false
                     select item).ToList();

            tblChallengeList = tblChallengeList
                    .Where(tblChallenge => favoriteList
                    .Any(challenge => challenge.ChallengeId == tblChallenge.ChallengeId)).ToList();          
        }
        public IActionResult OnPostDelete(int id)
        {
            if (UserChallengeDb.Challenges != null)
            {
                var find = UserChallengeDb.Favorites.FirstOrDefault(c => c.ChallengeId == id);
                if (find != null)
                {
                    find.IsDeleted = true;
                    UserChallengeDb.SaveChanges();
                }
            }
         return RedirectToAction("Get");   
        }
    }
}
