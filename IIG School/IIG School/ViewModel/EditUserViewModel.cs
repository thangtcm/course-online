using IIG_School.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IIG_School.ViewModel
{
    public class EditUserViewModel
    {
        public ApplicationUser? User { get; set; }

        public IList<SelectListItem>? Roles { get; set; }
    }
}
