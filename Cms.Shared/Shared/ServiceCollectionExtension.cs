using System.Reflection;
using Cms.Shared.Modules.Image.Services;
using Cms.Shared.Modules.UserProfile.Services;
using Cms.Shared.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cms.Shared.Shared;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection serviceCollection, IConfiguration configuration, ICollection<Assembly> assemblies)
    {
        serviceCollection.AddSingleton(assemblies);
        serviceCollection.AddScoped<DataContext>();
        serviceCollection.AddScoped<GetProfileService>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<ImageService>();
        serviceCollection.AddScoped<InitializerService>();
        serviceCollection.AddScoped<IInitializer, SharedInitializer>();
        serviceCollection.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        

        
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "issuer",
                ValidAudience = "audience",
                IssuerSigningKey = new SymmetricSecurityKey("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"u8.ToArray())
            };
        });
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}