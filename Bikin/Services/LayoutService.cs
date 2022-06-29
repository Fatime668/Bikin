using Bikin.DAL;
using Bikin.Models;
using Bikin.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<HomeVM> GetDatas()
        {
            List<Setting> settings = await _context.Setting.ToListAsync();
            List<SocialMedia> socialMedias = await _context.SocialMedias.ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Settings=settings,
                SocialMedias=socialMedias
            };

            return homeVM;
        }
    }
}
