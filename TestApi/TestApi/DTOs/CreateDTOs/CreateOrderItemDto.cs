namespace TestApi.DTOs.CreateDTOs
{
    public class CreateOrderItemDto
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int OrderId { get; set; }
    }
}
