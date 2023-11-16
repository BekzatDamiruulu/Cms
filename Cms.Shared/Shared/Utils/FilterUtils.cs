using System.Text.Json;
using Cms.Shared.Shared.Models;

namespace Cms.Shared.Shared.Utils;

public static class FilterUtils
{
    public  static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> entities, string? jsonStringFilter, Func<Filter,IQueryable<TEntity>, IQueryable<TEntity>> filter)
    {
        Console.WriteLine(jsonStringFilter);
        Console.WriteLine(string.IsNullOrEmpty(jsonStringFilter));
        var filterList = !string.IsNullOrEmpty(jsonStringFilter)
            ? JsonSerializer.Deserialize<List<JsonDocument>>(jsonStringFilter)
            : null;
        if(filterList == null)
        {
            return entities;
        }
        foreach (var filterItem in filterList)
        {
            var fieldName = filterItem.RootElement.GetProperty("name").GetString();
            if (fieldName == null) throw new NullReferenceException();
            var fieldValue = filterItem.RootElement.GetProperty("value");
            var filterValue = new Filter()
            {
                Name = fieldName,
                Value = fieldValue
            };
           
            entities = filter(filterValue, entities);
        }
        return entities;
    }
    
}