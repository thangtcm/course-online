using IIG_School.Repository.UnitOfWork;
using IIG_School.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace IIG_School.Services
{
    public class RoleService : IRoleService
    {
        public IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<IdentityRole>> GetRoles()
            => await _unitOfWork.RoleRepository.GetAllAsync();
    }
}
