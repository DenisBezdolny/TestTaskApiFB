using LinqKit;
using System.Linq.Expressions;
using TestApi.Domain.DTOs;
using TestApi.Domain.Entities;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.Interfaces.Repositories;

namespace TestApi.Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderFilterDto filter)
        {
            if (filter == null)
            {
                return await _orderRepository.GetAllAsync();
            }

            if (!filter.StartDate.HasValue)
            {
                filter.StartDate = DateTime.Today.AddMonths(-1);
            }

            // Start with always true predicete
            var predicate = PredicateBuilder.New<Order>(true);

            if (!string.IsNullOrEmpty(filter.Number))
            {
                predicate = predicate.And(o => o.Number.Contains(filter.Number));
            }

            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                predicate = predicate.And(o => o.Date >= filter.StartDate.Value && o.Date <= filter.EndDate.Value);
            }

            if (filter.ProviderId.HasValue)
            {
                predicate = predicate.And(o => o.ProviderId == filter.ProviderId.Value);
            }

            Expression<Func<Order, bool>> filterExpression = (Expression<Func<Order, bool>>)predicate.Expand();
            return await _orderRepository.GetAllAsync(filterExpression);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task CreateOrderAsync(Order order)
        {
            
            var existingOrders = await _orderRepository.GetAllAsync();
            if (existingOrders.Any(o => o.Number == order.Number && o.ProviderId == order.ProviderId))
            {
                throw new InvalidOperationException("Заказ с таким номером уже существует для данного поставщика.");
            }

            if (order.OrderItems != null && order.OrderItems.Any(oi => oi.Name == order.Number))
            {
                throw new InvalidOperationException("Имя элемента заказа не может совпадать с номером заказа.");
            }

            await _orderRepository.CreateAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders.Any(o => o.Id != order.Id && o.Number == order.Number && o.ProviderId == order.ProviderId))
            {
                throw new InvalidOperationException("Заказ с таким номером уже существует для данного поставщика.");
            }

            if (order.OrderItems != null && order.OrderItems.Any(oi => oi.Name == order.Number))
            {
                throw new InvalidOperationException("Имя элемента заказа не может совпадать с номером заказа.");
            }

            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
