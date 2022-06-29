using Bikin.Areas.Admin.ViewModels;
using Bikin.Models;
using Bikin.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> Register(AdminRegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                Firstname = register.Firstname,
                Lastname = register.Lastname,
                Email = register.Email,
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, register.Roles.ToString());
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Dashboard");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AdminLoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return View();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string adminRole = roles.FirstOrDefault(a => a.ToLower().Trim() == Roles.Admin.ToString().ToLower().Trim());
            string superAdminRole = roles.FirstOrDefault(s => s.ToLower().Trim() == Roles.SuperAdmin.ToString().ToLower().Trim());
            if (adminRole == null && superAdminRole == null)
            {
                ModelState.AddModelError("", "Something went wrong.Please contact with admins");
                return View();
            }
            else
            {
                if (login.RememberMe)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "You have been for 5 minutes");
                            return View();
                        }
                        ModelState.AddModelError("", "Username or password is incorrect");
                        return View();
                    }
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "You have been for 5 minutes");
                            return View();
                        }
                        ModelState.AddModelError("", "Username or password is incorrect");
                        return View();
                    }

                }
                return RedirectToAction("Index", "Dashboard");
            }

        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();
            
            AdminEditVM edit = new AdminEditVM
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.UserName,
                Email = user.Email  
            };
            return View(edit);
        }        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AdminEditVM user)
        {
            AppUser existedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AdminEditVM edit = new AdminEditVM
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Email = user.Email
            };
            if (!ModelState.IsValid) return View(edit);
            
            bool result = user.Password == null && user.ConfirmPassword == null && user.CurrentPassword != null;
            if (user.Email == null || user.Email != existedUser.Email)
            {
                ModelState.AddModelError("", "You can not change email");
                return View(edit);
            }
            if (result)
            {
                existedUser.UserName = user.Username;
                existedUser.Lastname = user.Lastname;
                existedUser.Firstname = user.Firstname;
                await _userManager.UpdateAsync(existedUser);
            }
            else
            {
                existedUser.UserName = user.Username;
                existedUser.Lastname = user.Lastname;
                existedUser.Firstname = user.Firstname;
               
                IdentityResult resulEdit = await _userManager.ChangePasswordAsync(existedUser,user.CurrentPassword,user.Password);
                if (!resulEdit.Succeeded)
                {
                    foreach (IdentityError err in resulEdit.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(edit);
                }
            }
            
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
        }
        public IActionResult Show()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
    }
}
