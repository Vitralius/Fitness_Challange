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
    public class ParticipatedChallengeListModel : PageModel
    {
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        private readonly ChallengeDatabaseContext ChallengeDb;
        public List<TblChallenge> tblChallengeList { get; set; } = default!;
        public List<Participate> participateList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ParticipatedChallengeListModel (UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;
        }
        public void OnGet()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            tblChallengeList = (from item in ChallengeDb.TblChallenges
                     select item).ToList(); 
            participateList = (from item in UserChallengeDb.Participates
                    where item.UserId == UserId
                    where item.IsDeleted == false
                     select item).ToList();

            tblChallengeList = tblChallengeList
                    .Where(tblChallenge => participateList
                    .Any(challenge => challenge.ChallengeId == tblChallenge.ParentId)).ToList();


             // for(int i=0; i<tblChallengeList.Count;i++)
             // {
             //     for(int j=0; j<participateList.Count; j++)
             //     {
             //         if(tblChallengeList[i].ChallengeId == participateList[j].ChallengeId)
             //         {
             //             tblChallengeList.Remove(tblChallengeList[j]);
             //         }
             //     }
             // }
        }

            public IActionResult OnPostGiveUp(int id)
        {
            if (UserChallengeDb.Challenges != null)
            {
                var find = UserChallengeDb.Participates.FirstOrDefault(c => c.ChallengeId == id);
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
