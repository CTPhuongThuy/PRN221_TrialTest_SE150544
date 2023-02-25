using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;

namespace CadidateManagement_CaoThiPhuongThuy.Pages.Can
{
    public class IndexModel : PageModel
    {
        private readonly ICandidateRepository candidateRepository;

        [BindProperty(SupportsGet = true)]
        public string CandidateSearchName { get; set; } 
        [BindProperty]
        public DateTime? CandidateSearchDate { get; set; }
        public IndexModel(ICandidateRepository candidateRepository, IConfiguration configuration)
        {
            this.candidateRepository = candidateRepository;
            Configuration = configuration;
        }
        private readonly IConfiguration Configuration;
       public PaginatedList<CandidateProfile> CandidateProfile { get; set; }

       // public IEnumerable<CandidateProfile> CandidateProfiles { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            try
            {
                int? role = HttpContext.Session.GetInt32("ROLE");
                if (role == 2)
                {
                    //  CandidateProfiles = candidateRepository.GetCandidates();
                    var pageSize = Configuration.GetValue("PageSize", 3);
                    var candidates =  candidateRepository.GetCandidatesAsQueryable();
                    CandidateProfile = PaginatedList<CandidateProfile>.CreateAsync(
                        candidates.AsNoTracking(), pageIndex ?? 1, pageSize);

                    if (CandidateSearchName != null || CandidateSearchDate != null)
                    {
                        var test = candidateRepository.SearchCandidatesAsQueryable(CandidateSearchName, CandidateSearchDate);
                        CandidateProfile = PaginatedList<CandidateProfile>.CreateAsync(
                            test.AsNoTracking(), pageIndex ?? 1, pageSize);

                        //var test = CandidateProfile
                        //    .Where(candidate => candidate.Fullname.ToLower().Contains(CandidateSearchName.ToLower()) || CandidateSearchDate == candidate.Birthday).AsQueryable().ToList();
                        //CandidateProfile = PaginatedList<CandidateProfile>.CreateAsync(
                        //test.AsQueryable().AsNoTracking(), pageIndex ?? 1, pageSize);

                        //var test = CandidateProfile
                        //     .Where(candidate => candidate.Fullname.ToLower().Contains(CandidateSearchName.ToLower()) || CandidateSearchDate == candidate.Birthday).AsQueryable().ToList();
                        // CandidateProfile = PaginatedList<CandidateProfile>.CreateAsync(
                        // test.AsQueryable().AsNoTracking(), pageIndex ?? 1, pageSize);

                        //|| 
                        //CandidateProfile = CandidateProfile
                        //    .Where(candidate => candidate.Fullname.ToLower().Contains(CandidateSearchName.ToLower()) || CandidateSearchDate == candidate.Birthday).ToList();
                        ////|| 
                    }
                }
                else
                {
                    ViewData["Message"] = "You are not allowed to access this function!";
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }

        }
    }
}