using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;

namespace CadidateManagement_CaoThiPhuongThuy.Pages.Can
{
    public class EditModel : PageModel
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IJobRepository jobRepository;


        public EditModel(ICandidateRepository candidateRepository, IJobRepository jobRepository)
        {
            this.candidateRepository = candidateRepository;
            this.jobRepository = jobRepository;
        }

        [BindProperty]
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
            ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");
                return Page();
            }

            //String c = CandidateProfile.Fullname;
            //if (c[0] <= 'A' || c[0] >= 'Z')
            //{

            //    ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");
            //    ViewData["ErrorMessage"] = "musst cap";
            //    return Page(ErrorMessage);
            //    // throw new Exception("Must captial");
            //}

            try
            {
                candidateRepository.UpdateCandidate(CandidateProfile);
            }
            catch (Exception ex)
            {
                ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
