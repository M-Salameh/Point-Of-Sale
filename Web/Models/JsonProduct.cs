using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models
{
    public class JsonProduct
    {
        [Display(Name = "product Identifier")]
        public int productId { get; set; }

        [Display(Name = "quantity")]
        public int quantity { get; set; }
    }
}
