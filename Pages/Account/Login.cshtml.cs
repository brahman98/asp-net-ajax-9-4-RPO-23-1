using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aspnetWebApp.Models;

namespace aspnetWebApp.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public LoginModel(
        SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    [BindProperty]
    public LoginInput Input { get; set; } = new();
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        var result = await _signInManager.PasswordSignInAsync(
            Input.Email,
            Input.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );
        if (result.Succeeded)
            return RedirectToPage("/Index");
        ModelState.AddModelError("", "Неверный email или пароль");
        return Page();   
    }
}
