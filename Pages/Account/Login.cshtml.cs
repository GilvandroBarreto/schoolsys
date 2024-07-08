using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using WebApp.Models;
using WebApp.Resources;



namespace WebApp.Pages.Account
{
    public class Login : PageModel
    {
        private readonly ILogger<Login> _logger;

        public Login(ILogger<Login> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Credential? Credential { get; set; } = null!;


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await using var db = new AppDbContext();
            
            var username = 
                (from Credentials in db.Credentials
                where Credentials.Id == 1
                select Credentials.Username).SingleOrDefault();


           var password = 
                (from Credentials in db.Credentials
                where Credentials.Id == 1
                select Credentials.Password).SingleOrDefault();

            
            if (!ModelState.IsValid) return Page();

            if (Credential.Username == username && Credential.Password == password)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@website.com")
                };
                var identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookie", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            return Page();
        }

    }
   
}






