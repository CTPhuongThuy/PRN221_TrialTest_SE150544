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
public class DeleteModel : PageModel
{
    private readonly ICandidateRepository candidateRepository;

    public DeleteModel(ICandidateRepository candidateRepository)
    {
        this.candidateRepository = candidateRepository;
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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {

        if (id == null)
        {
            return NotFound();
        }
        int? role = HttpContext.Session.GetInt32("ROLE");
        if (role != 2)
        {
            ViewData["Message"] = "You are not allowed to access this function!";
            return Page();
        }

        try
        {
            candidateRepository.DeleteCandidate(id);
        }
        catch (Exception ex)
        {

        }

        return RedirectToPage("./Index");
    }
}
}
