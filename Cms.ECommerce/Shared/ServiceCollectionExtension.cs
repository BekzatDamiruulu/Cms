using Cms.ECommerce.Modules.Cart.Services;
using Cms.ECommerce.Modules.CommodityCategory.Services;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.ECommerce.Shared;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEcommerceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UidService>();
        serviceCollection.AddScoped<CommodityCategoryService>();
        serviceCollection.AddScoped<CartService>();
        serviceCollection.AddScoped<IInitializer, ECommerceInitializer>();
        return serviceCollection;
    }
}