using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models
{
    public class CartViewModel
    {

        [Display(Name = "products")]
        public List<JsonProduct> products { get; set; }


        [Display(Name = "Identifier")] 
        public int Id { get; set; }
    }
}
