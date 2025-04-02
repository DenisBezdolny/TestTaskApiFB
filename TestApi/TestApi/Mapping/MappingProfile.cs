using AutoMapper;
using TestApi.Domain.Entities;
using TestApi.DTOs;
using TestApi.DTOs.CreateDTOs.TestApi.DTOs.CreateDTOs;
using TestApi.DTOs.CreateDTOs;

namespace TestApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Order <-> OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            // OrderItem <-> OrderItemDto
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            // Provider <-> ProviderDto
            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderDto, Provider>();

            // Маппинг для создания заказа и элементов заказа
            CreateMap<CreateOrderDto, Order>()
                // При маппинге коллекции OrderItems преобразуем каждый элемент
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}
