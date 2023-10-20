using IIG_School.Models;

namespace IIG_School.Services.Interface
{
    public interface IProductService
    {
        public Task<Product?> GetAsync(int id);
        public Task<ICollection<Product>> GetList();
        public Task Add(Product data, IFormFile upload);
        public Task Update(Product data, IFormFile upload);
        public Product? Get(int id);
        public Task Remove(int Id);
    }
}
