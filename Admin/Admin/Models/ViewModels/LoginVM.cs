using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Användarnamn")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Du måste ange ett användarnamn")]
        public string Username { get; set; }

        [Display(Name = "Lösenord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Du måste ange ditt lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Access Access { get; set; } = new Access();
        
    }
}