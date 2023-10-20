using IIG_School.Data;
using IIG_School.ViewModel;

namespace IIG_School.Services.Interface
{
    public interface IUserService
    {
        public Task<ICollection<ApplicationUser>> GetUsers();
        public Task<ApplicationUser?> GetUser(string id);
        public Task<bool> UpdateUser(UserInfoVM user);
        public Task<ApplicationUser?> GetUser();
        public ApplicationUser? GetUserWithRole();
    }
}
