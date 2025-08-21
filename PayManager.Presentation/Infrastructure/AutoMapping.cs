using AutoMapper;
using PayManager.Business.Domain;
using PayManager.Business.Implementation.DTOs;
using BusinessAutoMapping = PayManager.Business.Implementation.Infrastructure.AutoMapping;



namespace PayManager.Presentation.Infrastructure
{
    public class AutoMapping: Profile
    {
        public static IMapper Mapper { get; private set; }

        public static void BuildMappings()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                BusinessAutoMapping.BuildMappings(config);
                config.CreateMap<Product, ProductDTO>();
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<PaymentOrder, PaymentOrderDTO>();
                config.CreateMap<PaymentOrderDTO, PaymentOrder>();
            });
            Mapper = mapperConfig.CreateMapper();
        }
    }
}
