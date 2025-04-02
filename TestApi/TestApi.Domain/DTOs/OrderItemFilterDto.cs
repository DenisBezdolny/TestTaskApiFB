namespace TestApi.Domain.DTOs
{
    public class OrderItemFilterDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
    }
}
