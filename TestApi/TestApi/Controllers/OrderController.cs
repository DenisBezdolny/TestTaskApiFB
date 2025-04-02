using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.DTOs.CreateDTOs;
using TestApi.Domain.Entities;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.DTOs;
using TestApi.DTOs.CreateDTOs.TestApi.DTOs.CreateDTOs;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // GET: api/orders?number=...&startDate=...&endDate=...&providerId=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders([FromQuery] OrderFilterDto filter)
        {
            var orders = await _orderService.GetOrdersAsync(filter);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                // Маппим DTO для создания в доменную модель Order
                var order = _mapper.Map<Order>(createOrderDto);
                await _orderService.CreateOrderAsync(order);
                // После создания, можно вернуть OrderDto, если нужно
                var orderDto = _mapper.Map<OrderDto>(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
            }
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            try 
            {
                if (orderDto.Id != id)
                    return BadRequest("Некорректный ID заказа.");
                var order = _mapper.Map<Order>(orderDto);
                await _orderService.UpdateOrderAsync(order);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
            }

        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
