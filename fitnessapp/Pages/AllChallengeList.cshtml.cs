using System.Collections.Immutable;
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
    public class AllChallengeListModel : PageModel
    {
        private readonly UserChallengeDatabaseContext UserChallengeDb;
        private readonly ChallengeDatabaseContext ChallengeDb;
        public List<Challenge> challengeList { get; set; } = default!;
        public List<TblChallenge> tblChallengeList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string TitleSort { get; set; } = null!;
        public string DescriptionSort { get; set; } = null!;
        public string CategorySort { get; set; } = null!;
        public string DateSort { get; set; }= null!;
        public string IsDeletedSort { get; set; }= null!;
        public string TitleFilter { get; set; } = null!;
        public string CategoryFilter { get; set; } = null!;
        public bool IsDeletedFilter { get; set; }
        public DateTime DateFilter { get; set; }
        public AllChallengeListModel (UserChallengeDatabaseContext UserDb, ChallengeDatabaseContext ChDb)
        {
            UserChallengeDb = UserDb;
            ChallengeDb = ChDb;
        }

        public void OnGet(string sortOrder, string searchString1, string searchString2, bool searchString3, string searchString4)
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;

            tblChallengeList = (from item in ChallengeDb.TblChallenges
                     select item).ToList();
            challengeList = (from item in UserChallengeDb.Challenges
                        where item.UserId == UserId
                     select item).ToList();

            tblChallengeList = tblChallengeList
            .Where(tblChallenge => !challengeList
                .Any(challenge => challenge.ChallengeId == tblChallenge.ParentId)).ToList();


            //Filtering and Sorting part
            TitleFilter = searchString1;
            CategoryFilter = searchString2;
            IsDeletedFilter = searchString3;
            if(!String.IsNullOrEmpty(searchString4))
            {
                DateFilter = DateTime.Parse(searchString4);
            }           
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "Title";
            DescriptionSort = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "Description";
            IsDeletedSort = String.IsNullOrEmpty(sortOrder) ? "isdeleted_desc" : "IsDeleted";
            CategorySort = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "Category";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IQueryable<TblChallenge> sortingList = from c in tblChallengeList.AsQueryable()
                                        select c;
            if (!String.IsNullOrEmpty(searchString1))
            {
                sortingList = sortingList.Where(c => c.Title.Contains(searchString1));
            }
            else if (!String.IsNullOrEmpty(searchString2))
            {
                sortingList = sortingList.Where(c => c.Category.Contains(searchString2));
            }
            else if (searchString3)
            {
                sortingList = sortingList.Where(c => c.IsDeleted == searchString3);
            }
            else if (!String.IsNullOrEmpty(searchString4))
            {
                sortingList = sortingList.Where(c => c.EndDate == DateTime.Parse(searchString4));
            }

            switch (sortOrder)
            {
                case "Title":
                    sortingList = sortingList.OrderBy(c => c.Title);
                   break;
                case "title_desc":
                    sortingList = sortingList.OrderByDescending(c => c.Title);
                    break;
                case "Date":
                    sortingList = sortingList.OrderBy(c => c.EndDate);
                   break;
                case "date_desc":
                    sortingList = sortingList.OrderByDescending(c => c.EndDate);
                    break;
                case "Category":
                    sortingList = sortingList.OrderBy(c => c.Category);
                    break;
                case "category_desc":
                    sortingList = sortingList.OrderByDescending(c => c.Category);
                    break;
                case "Description":
                    sortingList = sortingList.OrderBy(c => c.Description);
                    break;
                case "description_desc":
                    sortingList = sortingList.OrderByDescending(c => c.Description);
                    break; 
                case "IsDeleted":
                    sortingList = sortingList.OrderBy(c => c.IsDeleted);
                    break;
                case "isdeleted_desc":
                    sortingList = sortingList.OrderByDescending(c => c.IsDeleted);
                    break;
                default:
                    sortingList = sortingList.OrderBy(c => c.Title);
                    break;
            }
            tblChallengeList = sortingList.ToList();
        }

         public IActionResult OnPostParticipate(int id)
         {
            var findChallenge = ChallengeDb.TblChallenges.FirstOrDefault(c => c.ChallengeId == id);
             if (findChallenge == null)
             {
                 return Page();
             }

             var newParticipate = new Participate
             {
                 ChallengeId = findChallenge.ParentId,
                 IsDeleted = findChallenge.IsDeleted,
                 UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
             };

             UserChallengeDb.Participates.Add(newParticipate);
             UserChallengeDb.SaveChanges();

             return RedirectToAction("Get");
         }

         public IActionResult OnPostFavorite(int id)
         {
            var findChallenge = ChallengeDb.TblChallenges.FirstOrDefault(c => c.ChallengeId == id);
             if (findChallenge == null)
             {
                 return Page();
             }

             var newFavorite = new Favorite
             {
                 ChallengeId = findChallenge.ParentId,
                 IsDeleted = findChallenge.IsDeleted,
                 UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
             };

             UserChallengeDb.Favorites.Add(newFavorite);
             UserChallengeDb.SaveChanges();

             return RedirectToAction("Get");
         }

    }
    
}
