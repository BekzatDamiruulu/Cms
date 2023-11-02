using System.Reflection;
using Cms.ECommerce;
using Cms.ECommerce.Shared;
using Cms.Setup;
using Cms.Shared.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath("/Users/damirbekuulubekzatbekzat/RiderProjects/Cms/Cms.Setup")
    .AddJsonFile("appsettings.json")
    .Build();
Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
try
{
    string? connectionString = configuration.GetConnectionString("DefaultConnection");
    if (connectionString == null) throw new NullReferenceException("connection string is null");
    Console.WriteLine(connectionString);
    var eCommerce = ECommerceAssemblyProvider.GetECommerceAssembly();
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging();
    serviceCollection.AddServices(configuration, new List<Assembly>{eCommerce});
    serviceCollection.AddEcommerceServices();
    serviceCollection.AddScoped<InitializeProvider>();
    var serviceProvider = serviceCollection.BuildServiceProvider();
    var provider = serviceProvider.GetRequiredService<InitializeProvider>();
    await provider.Initialize();
}
catch (Exception e)
{
    Console.WriteLine(e);
}

