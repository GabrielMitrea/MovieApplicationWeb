using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Areas.Account.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "You need to share with us your first name.")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
         [Display(Name = "Email Address")]
         [Required(ErrorMessage = "You need to share with us your email adress.")]
         public string EmailAddress { get; set; }

         [Display(Name = "Confirm Email")]
         [Compare("EmailAddress", ErrorMessage = "The email and the confirm email must match.")]
         public string ConfirmEmail { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have a password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "You need to provide a longer password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Passowrd")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
