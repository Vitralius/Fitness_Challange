using fitnessapp.Data;
using fitnessapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace fitnessapp.Namespace
{
    [Authorize]
    public class MyChallengeListModel : PageModel
    {
        [BindProperty]
        public Challenge newChallenge { get; set; } = default!;
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        private readonly ChallengeDatabaseContext ChallengeDb;
        public List<Challenge> challengeList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public MyChallengeListModel (UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;
        }
        public void OnGet()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            challengeList = (from item in UserChallengeDb.Challenges
                        where item.IsDeleted == false
                        where item.UserId == UserId
                     select item).ToList();
        }

         public IActionResult OnPost()
         {
             if (newChallenge == null)
             {
                 return Page();
             }
             newChallenge.IsDeleted = false;
             newChallenge.UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
             UserChallengeDb.Challenges.Add(newChallenge);
             UserChallengeDb.SaveChanges();
             
             var newTblChallenge = new TblChallenge
             {
                 ParentId = newChallenge.ChallengeId,
                 Title = newChallenge.Title,
                 Description = newChallenge.Description,
                 Category = newChallenge.Category,
                 IsDeleted = newChallenge.IsDeleted,
                 EndDate = newChallenge.EndDate
             };

             ChallengeDb.TblChallenges.Add(newTblChallenge);
             ChallengeDb.SaveChanges();

             return RedirectToAction("Get");
         }

            public IActionResult OnPostDelete(int id)
        {
            var userChallenge = UserChallengeDb.Challenges.FirstOrDefault(c => c.ChallengeId == id);
            var challenge = ChallengeDb.TblChallenges.FirstOrDefault(c => c.ParentId == id);
            
            if (userChallenge == null || challenge == null)
            {
                return NotFound();
            }
            userChallenge.IsDeleted = true;
            challenge.IsDeleted = true;
            UserChallengeDb.SaveChanges();
            ChallengeDb.SaveChanges();

         return RedirectToPage();
        }
               


    }
    
}
