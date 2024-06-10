using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchitecture.WebUI.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Villa>? VillaList { get; set; }
        public DateOnly CheckInDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly CheckOutDate { get; set; }
        public string? Keyword { get; set; }
        public int? CityId { get; set; }
        public int Nights { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public IEnumerable<City>? CityList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? SelectCity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? SelectPrice { get; set; }
    }
}
