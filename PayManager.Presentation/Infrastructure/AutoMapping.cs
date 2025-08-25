using AutoMapper;
using Microsoft.OpenApi.Extensions;
using PayManager.ApiService.Models;
using PayManager.Business.Domain;
using PayManager.Business.Enums;
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

                config.CreateMap<Product, ProductModel>();
                config.CreateMap<ProductModel, Product>();

                config.CreateMap<PaymentOrder, PaymentOrderDTO>()
                    .ForMember(d => d.Products,
                       opt => opt.MapFrom(src =>
                           src.OrderProducts.Select(op => op.Product)))
                    .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => new  List<FeeDTO> { new FeeDTO { Name = "Sales Commision", Amount = src.FeesAmount } }))
                    .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.GetDisplayName()))
                    .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => OrderStatus.Pending));
                config.CreateMap<PaymentOrderDTO, PaymentOrder>()
                    .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.PaymentMethod)))
                    .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => OrderStatus.Pending));

                config.CreateMap<OrderCreateModel, PaymentOrder>();

                config.CreateMap<PaymentOrder, OrderCreateModel>()
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.PaymentMethod.GetDisplayName()));
                config.CreateMap<OrderResponse, PaymentOrder>()
                   .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.Method)))
                   .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)))
                   .ForMember(dest => dest.FeesAmount, opt => opt.MapFrom(src => src.Fees != null ? src.Fees.Sum(f => f.Amount) : 0.0))
                   .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));

                config.CreateMap<PaymentOrder, OrderResponse>();

            });
            Mapper = mapperConfig.CreateMapper();
        }
    }
}
