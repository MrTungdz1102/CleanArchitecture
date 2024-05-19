using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchitecture.WebUI.Models.ViewModel
{
    public class VillaVM
    {
        public Villa? Villa { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CityList { get; set; }
    }
}
