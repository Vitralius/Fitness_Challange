using System.Security.Claims;
using fitnessapp.Data;
using fitnessapp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace fitnessapp.Areas.Identity.Pages.Account.Manage
{
    public class FavoritesModel : PageModel
    {
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        private readonly ChallengeDatabaseContext ChallengeDb;
        private readonly UserManager<IdentityUser> UserManager;
        public List<TblChallenge> tblChallengeList { get; set; } = new List<TblChallenge>();
        public List<Favorite> favoriteList { get; set; } = new List<Favorite>();
        public FavoritesModel (UserManager<IdentityUser> _userManager, UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserManager = _userManager;
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            tblChallengeList = await ChallengeDb.TblChallenges.ToListAsync();
            favoriteList = await UserChallengeDb.Favorites.Where(item => item.UserId == userId && item.IsDeleted == false).ToListAsync();

            tblChallengeList = tblChallengeList.Where(tblChallenge => favoriteList.Any(challenge => challenge.ChallengeId == tblChallenge.ChallengeId)).ToList();          
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var find = await UserChallengeDb.Favorites.FirstOrDefaultAsync(c => c.ChallengeId == id);
            if (find != null)
            {
                find.IsDeleted = true;
            }   
            
            await UserChallengeDb.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
