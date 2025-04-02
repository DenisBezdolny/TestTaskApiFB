using TestApi.Domain.DTOs;
using TestApi.Domain.Entities;

namespace TestApi.Domain.Interfaces.Bll
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(OrderFilterDto filter = null);
        Task<Order> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
