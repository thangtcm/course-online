using IIG_School.Data;
using IIG_School.Models;
using IIG_School.Repository.GenericRepository;
using IIG_School.Repository.Interface;

namespace IIG_School.Repository
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
