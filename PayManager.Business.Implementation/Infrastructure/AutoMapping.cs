using AutoMapper;


namespace PayManager.Business.Implementation.Infrastructure
{
    public class AutoMapping : Profile
    {
        public static IMapper Mapper { get; private set; }
        public static void BuildMappings(IMapperConfigurationExpression config)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
              
            });

            Mapper = mapperConfig.CreateMapper();
        }
    }
}
