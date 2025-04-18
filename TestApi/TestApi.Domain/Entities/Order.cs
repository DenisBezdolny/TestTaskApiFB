﻿namespace TestApi.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
