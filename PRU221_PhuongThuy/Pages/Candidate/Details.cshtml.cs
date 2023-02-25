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
    public class DetailsModel : PageModel
    {
        private readonly ICandidateRepository candidateRepository;

        public DetailsModel(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        public CandidateProfile CandidateProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            int? role = HttpContext.Session.GetInt32("ROLE");
            if (role != 2)
            {
                ViewData["Message"] = "You are not allowed to access this function!";
                return Page();
            }
            if (id == null)
            {
                return NotFound();
            }

            CandidateProfile = candidateRepository.GetCandidateById(id);

            if (CandidateProfile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

