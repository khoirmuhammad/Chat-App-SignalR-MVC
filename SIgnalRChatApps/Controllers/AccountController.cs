using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SIgnalRChatApps.Models;
using System.Security.Claims;
using SIgnalRChatApps.Data;
using Microsoft.EntityFrameworkCore;
using SIgnalRChatApps.Hubs;

namespace SIgnalRChatApps.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.Where(
                    x => x.Username == objLoginModel.UserName && x.Password == objLoginModel.Password)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {  
                    ViewBag.Message = "Invalid Credential";
                    return View(objLoginModel);
                }
                else
                {
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Fullname", user.Fullname),
                        new Claim("Photo", user.Photo)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });

                    
                    return LocalRedirect(objLoginModel.ReturnUrl);
                }
            }
            return View(objLoginModel);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return LocalRedirect("/");
        }

    }
}
