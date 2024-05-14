using fitnessapp.Data;
using fitnessapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace fitnessapp.Namespace
{
    public class ChallangeListModel : PageModel
    {
        [BindProperty]
        public TblChallange TblChallange { get; set; } = default!;

        public UserChallangeDbContext ChallengeDb = new();
        public List<TblChallange> cList { get; set; } = default!;
        public void OnGet()
        {
            cList = (from item in ChallengeDb.tbl_challange
                     select item).ToList();
        }

         public IActionResult OnPost()
         {
             if (TblChallange == null)
             {
                 return Page();
             }
             TblChallange temp = new TblChallange();
             TblChallange.IsDeleted = false;
             TblChallange.Id = 2;
             TblChallange.UserId = "5";
             TblChallange.Title = "Running";
             TblChallange.EndDate = DateTime.Now;
             TblChallange.Category = "Yok";
             TblChallange.Description = "Var";
             cList.Add(temp);
             ChallengeDb.SaveChanges();
             return RedirectToAction("Get");
         }

            public IActionResult OnPostDelete(int id)
        {
            var itemToUpdate = cList.FirstOrDefault(item => item.Id == id);
            if (ChallengeDb.tbl_challange != null)
            {
                var challenge = ChallengeDb.tbl_challange.Find(id);
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
