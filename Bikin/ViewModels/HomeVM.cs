using Bikin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.ViewModels
{
    public class HomeVM
    {
        public List<Setting> Settings { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<Service> Services { get; set; }
        public List<Logo> Logos { get; set; }
    }
}
