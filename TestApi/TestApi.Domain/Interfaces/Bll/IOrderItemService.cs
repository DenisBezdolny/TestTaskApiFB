using TestApi.Domain.DTOs;
using TestApi.Domain.Entities;

namespace TestApi.Domain.Interfaces.Bll
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId, OrderItemFilterDto filter = null);
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task CreateOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int id);
    }
}
