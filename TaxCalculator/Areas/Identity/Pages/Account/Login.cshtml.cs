using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ReachUtility.Areas.Identity.Pages.Account
{
  
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;        
        private readonly ILogger<LoginModel> _logger;
        

        public LoginModel(
                            UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<IdentityUser> signInManager,
                            ILogger<LoginModel> logger)
                   
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            bool isValid = false;
            if (ModelState.IsValid)
            {              
                var user = new IdentityUser();
                // Check if admin user
                if (Input.Email.ToLower() == "admin@media24.com")
                {
                    user = await _userManager.FindByNameAsync("Admin");
                }
                else
                {
                    // The above Commented lines of code are meant for Active directory Account

                    //First authenticate AD account
                   // bool isValid = false;
                    //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "ZA"))
                    //{
                    //    // validate the credentials
                    //    isValid = pc.ValidateCredentials(Input.Email, Input.Password);
                    //}

                    user = await _userManager.FindByEmailAsync(Input.Email);
                    if (isValid)
                    {
                        // Create user if user doesn't already exist for this application
                        if (user == null)
                        {
                            IdentityUser newUser = new IdentityUser
                            {
                                UserName = Input.Email.Split("@")[0],
                                Email = Input.Email,
                                EmailConfirmed = true,
                                SecurityStamp = Guid.NewGuid().ToString()
                            };
                            // Create user
                            var create = await _userManager.CreateAsync(newUser, "P@ssw0rd");
                            if (create.Succeeded)
                            {
                                if (!await _roleManager.RoleExistsAsync("User"))
                                {
                                    // Create User role
                                    var userRole = new IdentityRole("User");
                                    await _roleManager.CreateAsync(userRole);
                                }
                                await _userManager.AddToRoleAsync(newUser, "User");
                            }
                            user = await _userManager.FindByEmailAsync(Input.Email);
                        }
                    }

                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                    // Check user roles
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Count == 0)
                    {
                        ModelState.AddModelError(string.Empty, "You don't have permission to access this site.");
                        return Page();
                    }
                }
                //var result = await _signInManager.PasswordSignInAsync(user.UserName, user.UserName != "Admin" ? "P@ssw0rd" : Input.Password, Input.RememberMe, lockoutOnFailure: false);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.UserName != "Admin" ? "P@ssw0rd" : Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded)//if (result.Succeeded && isValid)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
