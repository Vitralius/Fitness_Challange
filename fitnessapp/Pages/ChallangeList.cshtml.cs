using fitnessapp.Data;
using fitnessapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fitnessapp.Namespace
{
    public class ChallangeListModel : PageModel
    {
        [BindProperty]
        public TblChallangelist NewChallangeList { get; set; } = default!;

        public ChallengeDbContext ChallengeDb = new();
        public List<TblChallangelist> cList { get; set; } = default!;
        public void OnGet()
        {
            // LINQ query to retrieve items where IsDeleted is false
            cList = (from item in ChallengeDb.Challenges
                     where item.IsDeleted == false
                     select item).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewChallangeList == null)
            {
                return Page();
            }
            NewChallangeList.IsDeleted = false;
            cList.Add(NewChallangeList);
            ChallengeDb.SaveChanges();
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
    {
        // var itemToUpdate = ToDoList.FirstOrDefault(item => item.Id == id);
        if (ChallengeDb.Challenges != null)
        {
            var challenge = ChallengeDb.Challenges.Find(id);
            if (challenge != null)
            {
                challenge.IsDeleted = true;
                ChallengeDb.SaveChanges();
            }
        }

        return RedirectToAction("Get");
    }
    }
    
}
