using IIG_School.Models;
using IIG_School.Repository.UnitOfWork;
using IIG_School.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IIG_School.Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(Order order, List<CartItem> cartItems)
        {
            order.OrderPlaced = DateTime.Now;
            double totalPrice = 0;
            List<OrderDetail> lst = new List<OrderDetail>();
            foreach (var cartitem in cartItems!)
            {
                var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == cartitem.product.Id);
                var orderDetail = new OrderDetail()
                {
                    Order = order,
                    Amount = cartitem.quantity,
                    ProductId = cartitem.product.Id,
                    Price = cartitem.product.ProductPrice,
                };
                product!.Inventory -= cartitem.quantity;
                totalPrice += orderDetail.Price * orderDetail.Amount;
                lst.Add(orderDetail);
            }
            order.OrderTotal = totalPrice;
            order.OrderLines = lst;
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.CommitAsync();
            return order.OrderID;
        }
    }
}
