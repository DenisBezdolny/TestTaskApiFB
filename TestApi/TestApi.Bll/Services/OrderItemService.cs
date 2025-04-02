using LinqKit;
using System.Linq.Expressions;
using TestApi.Domain.DTOs;
using TestApi.Domain.Entities;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.Interfaces.Repositories;

namespace TestApi.Bll.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Order> _orderRepository;

        public OrderItemService(IRepository<OrderItem> orderItemRepository,
                                IRepository<Order> orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId, OrderItemFilterDto filter)
        {
            if (filter == null)
            {
                return await _orderItemRepository.GetAllAsync(oi => oi.OrderId == orderId);
            }

            var predicate = PredicateBuilder.New<OrderItem>(oi => oi.OrderId == orderId);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                predicate = predicate.And(oi => oi.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Unit))
            {
                predicate = predicate.And(oi => oi.Unit.Contains(filter.Unit));
            }

            Expression<Func<OrderItem, bool>> filterExpression = (Expression<Func<OrderItem, bool>>)predicate.Expand();
            return await _orderItemRepository.GetAllAsync(filterExpression);
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _orderItemRepository.GetByIdAsync(id);
        }

        public async Task CreateOrderItemAsync(OrderItem orderItem)
        {
            // Получаем заказ для валидации
            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);
            if (order != null && orderItem.Name == order.Number)
            {
                throw new InvalidOperationException("Имя элемента заказа не может совпадать с номером заказа.");
            }
            await _orderItemRepository.CreateAsync(orderItem);
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);
            if (order != null && orderItem.Name == order.Number)
            {
                throw new InvalidOperationException("Имя элемента заказа не может совпадать с номером заказа.");
            }
            await _orderItemRepository.UpdateAsync(orderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            await _orderItemRepository.DeleteAsync(id);
        }
    }
}
