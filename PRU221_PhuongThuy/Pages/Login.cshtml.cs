using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CadidateManagement_CaoThiPhuongThuy.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public LoginModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Hraccount account = accountRepository.GetHraccount(Email);
                if (account == null || account.Password != Password)
                {
                    ViewData["Message"] = "Wrong email or password";
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetString("EMAIL", account.Email);
                    HttpContext.Session.SetInt32("ROLE", (int)account.MemberRole);
                    return RedirectToPage("/Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Page();
            }


        }
    }
}
