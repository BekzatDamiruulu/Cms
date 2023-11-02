using Cms.Shared.Shared;
using Cms.Shared.Shared.Services;

namespace Cms.ECommerce.Shared;

public class ECommerceInitializer : IInitializer
{
    private readonly InitializerService _initializerService;

    public ECommerceInitializer(InitializerService initializerService)
    {
        _initializerService = initializerService;
    }

    public async Task Initialize()
    {
    }
}