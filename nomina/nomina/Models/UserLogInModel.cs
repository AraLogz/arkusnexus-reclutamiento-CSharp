using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina.Models
{
    public class UserLogInModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.EmailRequired)) ]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjEmail))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.PasswordRequired)) ]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjPassword))]
        public string Password { get; set; }
    }
}