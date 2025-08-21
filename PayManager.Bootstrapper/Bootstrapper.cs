using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayManager.ApiService;
using PayManager.ApiService.Implementation;
using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Implementation.ApplicationService;
using PayManager.Business.Implementation.Service;
using PayManager.DataAccess.Contracts.Repository;
using PayManager.DataAccess.Implementation.Repository;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace PayManager.Bootstrapper;

public static class Bootstrapper
{
	public static void BootstrapServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddSingleton(configuration);

        services.AddScoped<IPaymentProviderApiService, PaymentProviderApiService>(_ => new PaymentProviderApiService(
            new HttpClient(), 
            configuration));
        services.AddScoped<IPaymentProviderSelectorService, PaymentProviderSelectorService>();

        services.AddScoped<IProductApplicationService, ProductApplicationService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderProductApplicationService, OrderProductApplicationService>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();
        services.AddScoped<IPaymentOrderApplicationService, PaymentOrderApplicationService>();
    }
}
