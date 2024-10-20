using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EventRegistrationSystem.Models.Repositories;
using EventRegistrationSystem.Models;

namespace EventRegistrationSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository repository;
        public AuthController(UserRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email,string password) {
            if (!string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }
            User user =  repository.GetUserByEmail(email);
            if (user == null)
            {
                ViewBag.msg = "User Not found";
                ViewBag.msgType = "danger";
                return View();
            }

            if (email == user.Email && password == user.Password)
            {
                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user)
        {
                if(repository.GetUserByUsername(user.UserName) != null )
                {
                    ViewBag.msg = "Username Already exists";
                    ViewBag.msgType = "danger";
                    return View();
                }
                if(repository.GetUserByEmail(user.Email) != null )
                {
                    ViewBag.msg = "Email Already exists";
                    ViewBag.msgType = "danger";
                    return View();
                }
                repository.Add(user);
                return RedirectToAction("Login");
           

            return View();
        }
    }
}
