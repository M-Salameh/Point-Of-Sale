using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginViewModel
    {

        [Display(Name = "Email")]
        [Required (ErrorMessage = "The Email Is Required, Please Give me This Information...")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "The Password Is Required, Please Enter It ....")]
        public string Password { get; set; }
    }
}