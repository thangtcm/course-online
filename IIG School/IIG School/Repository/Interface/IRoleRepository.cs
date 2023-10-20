using IIG_School.Repository.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace IIG_School.Repository.Interface
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
    }
}
