namespace TestApi.Domain.DTOs
{
    public class OrderFilterDto
    {
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProviderId { get; set; }
    }
}
