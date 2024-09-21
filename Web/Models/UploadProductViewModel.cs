using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class UploadProductViewModel
    {

        [AllowNull]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Enter The Name Of The Product Please..🙂")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Numbers are not allowed.")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Enter The Description Of The Product Please..🙂")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Enter The Price Of The Product Please..🙂")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be non-negative.")]
        public int Price { get; set; }


        [AllowNull]
        [Display(Name = "Image URL")]
        public string Image { get; set; }
        
        
        [AllowNull]
        public int Quantity { get; set; }

        [Display (Name = "Alert Quantity")]
        [Required(ErrorMessage = "Enter The Alert Quantity Of The Product Please..🙂")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be non-negative.")]
        public int AlertQuantity { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Please Upload An Image..🙂")]
        public IFormFile ImageFile { get; set; }

    }
}
