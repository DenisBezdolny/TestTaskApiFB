namespace TestApi.DTOs.CreateDTOs
{
    namespace TestApi.DTOs.CreateDTOs
    {
        public class CreateOrderDto
        {
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public int ProviderId { get; set; }
            public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
        }
    }

}
