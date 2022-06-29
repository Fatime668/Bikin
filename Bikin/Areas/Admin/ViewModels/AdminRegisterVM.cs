using Bikin.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.Areas.Admin.ViewModels
{
    public class AdminRegisterVM
    {
        [Required, StringLength(maximumLength: 15)]
        public string Firstname { get; set; }
        [Required, StringLength(maximumLength: 15)]

        public string Lastname { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(maximumLength: 15)]

        public string Username { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password")]

        public string ConfirmPassword { get; set; }
        [Required]
        public bool TermsCondition { get; set; }
        [Required]
        public Roles Roles { get; set; }
    }
}
