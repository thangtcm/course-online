using Microsoft.AspNetCore.Identity;

namespace IIG_School.Services.Interface
{
    public interface IRoleService
    {
        public Task<ICollection<IdentityRole>> GetRoles();
    }
}
