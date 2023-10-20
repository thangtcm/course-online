using IIG_School.Data;
using IIG_School.Models;
using IIG_School.Repository.GenericRepository;
using IIG_School.Repository.Interface;

namespace IIG_School.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
