using Cms.Shared.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Shared.Services;

public class InitializerService
{
    private readonly DataContext _dataContext;

    public InitializerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddTestData<T>(T entity) where T : Entity, new()
    {
        var dataSet = _dataContext.Set<T>();
        var defined = await dataSet.FirstOrDefaultAsync(e => e.ObjectName == entity.ObjectName);
        if(defined != null) return;
        await dataSet.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }
    

    public async Task AddTestData<T>(List<T> entities) where T : Entity, new()
    {
        try
        {
            var dataSet = _dataContext.Set<T>();
            var names = entities
                .Select(e => e.ObjectName)
                .ToList();
            
            var existItems = await dataSet
                .Where(e => names.Contains(e.ObjectName))
                .ToListAsync();

            var notExistItems = entities
                .Where(e => existItems.Any(ei => ei.ObjectName != e.ObjectName))
                .ToList();
        
            if(!notExistItems.Any()) return;

            await dataSet.AddRangeAsync(notExistItems);

            await _dataContext.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}