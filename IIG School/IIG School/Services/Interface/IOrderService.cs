using IIG_School.Models;

namespace IIG_School.Services.Interface
{
    public interface IOrderService
    {
        public Task<int> Add(Order order, List<CartItem> cartItems);
    }
}
