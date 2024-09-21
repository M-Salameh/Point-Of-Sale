using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            Order = new HashSet<OrderDTO>();
        }

        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }





        [Required  (ErrorMessage = "You Should Enter The Name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Numbers are not allowed, This Is A Name.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "You Should Enter The Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }



        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please Enter A Valid Email Address.")]
        [Required(ErrorMessage = "You Should Enter The Email Address")]
        [EmailAddress(ErrorMessage = "This Is Not EmailAddress")]
        [Display(Name = "EmailAddress")]

        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "You Should Enter The PhoneNumber")]
        [Phone(ErrorMessage = "This Is Not A PhoneNumber")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [AllowNull]
        public DateTime CreatedAt { get; set; }

        [AllowNull]
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<OrderDTO> Order { get; set; }
    }
}
