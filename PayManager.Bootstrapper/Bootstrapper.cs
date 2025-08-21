using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


        services.AddScoped<IPaymentProviderSelector, PaymentProviderSelector>(_ => new PaymentProviderSelector(
            configuration["PaymentProviders"]));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();
        services.AddScoped<IProductApplicationService,ProductApplicationService>();
        services.AddScoped<IPaymentOrderApplicationService, PaymentOrderApplicationService>();
    }
}
