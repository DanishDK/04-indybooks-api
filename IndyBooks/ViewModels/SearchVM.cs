using System.ComponentModel.DataAnnotations;

namespace IndyBooks.ViewModels
{
    public class SearchVM
    {
        [Display(Name = "Title to Find: ")]
        public String Title { get; set; } = "";

        //Adds properties and Display annotation needed for searching
        [Display(Name = "Author Last Name: ")]
        public String AuthorLastName { get; set; } = "";

        [Display(Name = "Min Price: ")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max Price: ")]
        public decimal MaxPrice { get; set; }

        [Display(Name = "SKU")]
        public string SKU {get; set;}

        public Boolean HalfPriceSale { get; set; }

    }
}
