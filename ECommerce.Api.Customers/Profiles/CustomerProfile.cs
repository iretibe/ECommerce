using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, CustomerModel>();
        }
    }
}
