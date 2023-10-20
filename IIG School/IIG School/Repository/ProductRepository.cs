using IIG_School.Data;
using IIG_School.Models;
using IIG_School.Repository.GenericRepository;
using IIG_School.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace IIG_School.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
