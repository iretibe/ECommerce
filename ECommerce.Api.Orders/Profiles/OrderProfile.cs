using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, OrderModel>();
            CreateMap<Db.OrderItem, OrderItemModel>();
        }
    }
}
