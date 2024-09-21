using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UserViewModel : IdentityUser
    {

        [Display(Name = "Password")]
        [Required]
         [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*(),.?\:{}|<>]).{8,}$", ErrorMessage = "The password must contain at least one capital letter, one small letter, one number, and one symbol.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Password Is Required, Please Enter It...")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
