using IIG_School.Data;
using IIG_School.Repository.UnitOfWork;
using IIG_School.Services.Interface;
using IIG_School.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace IIG_School.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUser? GetUserWithRole()
        {
            ApplicationUser user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User).Result;
            if (user != null)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToList();
            }
            return user;
        }

        public async Task<ApplicationUser?> GetUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            return user;
        }

        public async Task<ICollection<ApplicationUser>> GetUsers()
            => await _unitOfWork.UserRepository.GetAllAsync();

        public async Task<ApplicationUser?> GetUser(string? id)
            => await _unitOfWork.UserRepository.GetAsync(x => x.Id == id);

        public async Task<bool> UpdateUser(UserInfoVM user)
        {
            if (user.UserId is null) return false;
            var userModel = await _unitOfWork.UserRepository.GetAsync(x => x.Id.Contains(user.UserId!));
            if (userModel is null) return false;
            userModel.PhoneNumber = user.NumberPhone;
            _unitOfWork.UserRepository.Update(userModel);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
