using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gamerz.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData); 
            }

            var addUserDto = new AddUserDto()
            {
                Name = formData.Name.Trim(),
                LastName = formData.LastName.Trim(),
                Email = formData.Email.Trim().ToLower(),
                Password = formData.Password
                
            };
            var result = _userService.AddUser(addUserDto);

            if (result.IsSucceed)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(formData);
            }
            
        }
        [HttpGet]
        public IActionResult LoginPage() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
                
            }

            var loginUserDto = new LoginUserDto()
            {
                Email = formData.Email,
                Password = formData.Password
            };

            var userInfo = _userService.LoginUser(loginUserDto);

            if (userInfo is null)
            {
                return RedirectToAction("Index", "Home");
            }


            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("name", userInfo.Name));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("userType", userInfo.UserType.ToString())); 

           

            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString())); 

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true, 
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(24)) 
            };
            


            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); 

            return RedirectToAction("Index", "Home");
        }


    }
    }

