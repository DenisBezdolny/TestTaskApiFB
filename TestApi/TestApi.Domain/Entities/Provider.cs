﻿namespace TestApi.Domain.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
