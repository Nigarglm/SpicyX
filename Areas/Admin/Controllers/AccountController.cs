﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpicyX.Areas.ViewModels.Account;
using SpicyX.Models;
using SpicyX.Utilities.Enums;

namespace SpicyX.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                Name = userVM.Name,
                Surname = userVM.Surname,
                Email = userVM.Email,
                UserName = userVM.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, userVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());

           // _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM,string? returnUrl)
        {
            if(!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Istifadeci adi, email ve ya parol yalnis daxil edilib");
                    return View();
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsRemembered, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "3 ugursuz cehd. 30 saniye sonra yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Istifadeci adi, email ve ya parol yalnis daxil edilib");
                return View();
            }
            if(returnUrl == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach(UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if(!(await _roleManager.RoleExistsAsync(role.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    });
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
