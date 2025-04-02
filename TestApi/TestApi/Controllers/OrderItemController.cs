using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.DTOs.CreateDTOs;
using TestApi.Domain.Entities;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.DTOs;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemsController> _logger;

        public OrderItemsController(IOrderItemService orderItemService, IMapper mapper, ILogger<OrderItemsController> logger)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/OrderItems?orderId=1&name=Widget&unit=pcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(
            [FromQuery] int orderId,
            [FromQuery] string name = null,
            [FromQuery] string unit = null)
        {
            var filterDto = new OrderItemFilterDto
            {
                Name = name,
                Unit = unit
            };

            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId, filterDto);
            var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
            return Ok(orderItemDtos);
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
                return NotFound();
            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return Ok(orderItemDto);
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult> CreateOrderItem([FromBody] CreateOrderItemDto createOrderItemDto)
        {
            _logger.LogInformation("Received CreateOrderItemDto: {@CreateOrderItemDto}", createOrderItemDto);

            var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);
            await _orderItemService.CreateOrderItemAsync(orderItem);

            _logger.LogInformation("Created OrderItem with ID: {OrderItemId}", orderItem.Id);

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItemDto);
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDto orderItemDto)
        {
            if (orderItemDto.Id != id)
                return BadRequest("Некорректный ID элемента заказа.");
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _orderItemService.UpdateOrderItemAsync(orderItem);
            return NoContent();
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}
