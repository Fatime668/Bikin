using Bikin.Models;
using Bikin.Utilities;
using Bikin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Controllers
{
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                UserName = register.Username,
                Firstname = register.Firstname,
                Lastname = register.Lastname,
                Email = register.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return NotFound();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault(r => r.ToLower().Trim() == Roles.Member.ToString().ToLower().Trim());
            if (role == null)
            {
                ModelState.AddModelError("", "Something went wrong.Please contact with admins");
                return View();
            }
            else
            {
                if (login.RememberMe)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "You have been for dismissed 5 minutes");
                            return View();
                        }
                        ModelState.AddModelError("", "Username or password is incorrect");
                        return View();
                    }
                }
                else
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "You have been for dismissed 5 minutes");
                            return View();
                        }
                        ModelState.AddModelError("", "Username or password is incorrect");
                        return View();
                    }
                }
                return RedirectToAction("Index", "Home");

            }


        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();
            EditVM edit = new EditVM
            {
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };

            return View(edit);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditVM user)
        {
            AppUser existed = await _userManager.FindByNameAsync(User.Identity.Name);
            EditVM edit = new EditVM
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email
            };
            bool result = user.ConfirmPassword == null && user.Password == null && user.CurrentPassword != null;
            if (user.Email == null || user.Email != edit.Email)
            {
                ModelState.AddModelError("", "You can not change your email");
                return View(edit);
            }
            if (result)
            {
                existed.UserName = user.Username;
                existed.Lastname = user.Lastname;
                existed.Firstname = user.Firstname;
                await _userManager.UpdateAsync(existed);
            }
            else
            {
                existed.UserName = user.Username;
                existed.Lastname = user.Lastname;
                existed.Firstname = user.Firstname;
                IdentityResult resultEdit = await _userManager.ChangePasswordAsync(existed, user.CurrentPassword, user.Password);
                if (!resultEdit.Succeeded)
                {
                    foreach (IdentityError err in resultEdit.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(edit);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Show()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
        public async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SuperAdmin.ToString() });
        }
    }
}
