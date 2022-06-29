using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int SettingId { get; set; }
        public Setting Setting { get; set; }
    }
}
