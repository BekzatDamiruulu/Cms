using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Shared;

public sealed class DataContext: IdentityDbContext<IdentityUser>
{
    private readonly ICollection<Assembly> _assemblies;
    
    public DataContext(DbContextOptions<DataContext> options, ICollection<Assembly> assemblies) : base(options)
    {
        _assemblies = assemblies;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext))!);
        foreach (var assembly in _assemblies)
        {
            builder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}