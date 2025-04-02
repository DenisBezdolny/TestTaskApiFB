namespace TestApi.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; } 
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
