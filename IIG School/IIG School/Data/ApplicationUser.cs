using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IIG_School.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        [NotMapped]
        public virtual ICollection<string>? Roles { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {

    }
}
