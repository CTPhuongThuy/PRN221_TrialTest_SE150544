using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.Repository;

namespace CadidateManagement_CaoThiPhuongThuy.Pages.Can
{
    public class CreateModel : PageModel
    {
        private readonly IJobRepository jobRepository;

        private readonly ICandidateRepository candidateRepository;

        public CreateModel(ICandidateRepository candidateRepository, IJobRepository jobRepository)
        {
            this.candidateRepository = candidateRepository;
            this.jobRepository = jobRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                int? role = HttpContext.Session.GetInt32("ROLE");
                if (role == 2)
                {
                    //   ViewData["PostingId"] = new SelectList(_context.JobPostings, "PostingId", "PostingId"); khong duoc dung context
                    ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");
            return Page();
                }
                else
                {
                    ViewData["Message"] = "You are not allowed to access this function!";
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Page();
            }
            return RedirectToPage("./Index");
        }

        [BindProperty]
        public CandidateProfile CandidateProfile { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");

                return Page();
            }
            try
            {
                candidateRepository.AddCandidate(CandidateProfile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CandidateProfile.CandidateId", ex.Message);
                ViewData["PostingId"] = new SelectList(jobRepository.GetJob(), "PostingId", "JobPostingTitle");

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
