using IIG_School.Data;
using IIG_School.Repository.GenericRepository;
using IIG_School.Repository.Interface;

namespace IIG_School.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
