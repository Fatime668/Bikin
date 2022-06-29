using Bikin.DAL;
using Bikin.Extensions;
using Bikin.Models;
using Bikin.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LogoController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Logo> logos = await _context.Logos.ToListAsync();
            return View(logos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Logo logo)
        {
            if (!ModelState.IsValid) return View();
            if (logo.Photo != null)
            {
                if (!logo.Photo.IsOkay(1))
                {
                    logo.Image = await logo.Photo.FileCreate(_env.WebRootPath, @"assets\img");

                    await _context.AddAsync(logo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Logo");
                }
                else
                {
                    ModelState.AddModelError("", "Zehmet olmasa 1 mb-in altinda bir sekil secin");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Please,choose image file");
                return View();           
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid) return View();
            Logo logo = await _context.Logos.FirstOrDefaultAsync(s => s.Id == id);
            if (logo == null) return NotFound();
            return View(logo);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Logo logo)
        {
            if (!ModelState.IsValid) return View();
            Logo existedLogo = await _context.Logos.FirstOrDefaultAsync(s => s.Id == id);
            if (logo.Photo != null)
            {
                if (!logo.Photo.IsOkay(1))
                {
                    string path = _env.WebRootPath + @"\assets\img\" + existedLogo.Image;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    existedLogo.Image = await logo.Photo.FileCreate(_env.WebRootPath, @"assets\img");
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Logo");

                }
                else
                {
                    ModelState.AddModelError("", "Zehmet olmasa 1 mb-nin altinda sekil daxil edin");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Zehmet olmasa sekil secin");
                return View();
            }
        }
        public async Task<IActionResult> Detail(int id)
        {
            Logo logo = await _context.Logos.FirstOrDefaultAsync(l => l.Id == id);
            return View(logo);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Logo logo = await _context.Logos.FirstOrDefaultAsync(l=>l.Id==id);
            return View(logo);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteLogo(int id)
        {
            Logo logo = await _context.Logos.FirstOrDefaultAsync(l=>l.Id==id);
            if (logo == null) return NotFound();
            string path = _env.WebRootPath + @"\assets\img\" + logo.Image;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Logos.Remove(logo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Logo");


        }
    }
}
