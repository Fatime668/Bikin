using Bikin.DAL;
using Bikin.Models;
using Bikin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid) return View();
            await _context.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Service");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            return View(service);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Service service)
        {
            if (!ModelState.IsValid) return View();
            Service existedService = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (existedService == null) return NotFound();
            existedService.Icon = service.Icon;
            existedService.Title = service.Title;
            existedService.Description = service.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Service");
        }
        public async Task<IActionResult> Detail(int id)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            return View(service);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            return View(service);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteService(int id)
        {
            Service existedService = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            _context.Remove(existedService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Service");
        }
    }
}
