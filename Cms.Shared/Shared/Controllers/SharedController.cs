using System.Reflection;
using System.Text.Json;
using Cms.Shared.Shared.Entities;
using Cms.Shared.Shared.Models;
using Cms.Shared.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Shared.Controllers;
public abstract class SharedController<TEntity> : ControllerBase
    where TEntity : Entity
{
    protected readonly DataContext DataContext;
    protected SharedController( DataContext dataContext)
    {
        DataContext = dataContext;
    }
    protected DbSet<TEntity> Entities => DataContext.Set<TEntity>();

    protected virtual IQueryable<TEntity> FilterPredicate(Filter filter, IQueryable<TEntity> entities)
    {
        return entities;
    }
    [HttpPost]
    public virtual async Task<TEntity> Create([FromBody] TEntity entity)
    {
        Entities.Add(entity);
        await DataContext.SaveChangesAsync();
        return entity;
    }
 
    [HttpDelete]
    public virtual async Task<IActionResult> Delete(long entityId)
    {
        try
        {
            var model = await Entities.FindAsync(entityId);
            Entities.Remove(model);
            await DataContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public virtual async Task<List<TEntity>> Read(string filter = "", int pageIndex = 1, int pageSize = 20,
        string orderField = "Id", string orderType = "ASC")
    {
        return await Entities
            .Filter(filter, FilterPredicate)
            .Paginate(pageIndex,pageSize)
            .Sort(orderField,orderType)
            .ToListAsync();
    }
    [HttpPut]
    public virtual async Task<IActionResult> UpdateField(long id,[FromBody] List<JsonModel> jsonModels )
    {
        try
        {
            var resultEntity = await Entities.FindAsync(id);
            if (resultEntity == null) throw new NullReferenceException($"{typeof(TEntity)} не найден");
            Type type = typeof(TEntity);
            foreach (var model in jsonModels)
            {
                var propName = model.Name;
                var property = type.GetProperty(propName,BindingFlags.Instance| BindingFlags.Public);
                if (property == null) throw new NullReferenceException();
                if (!property.CanWrite) throw new Exception($"{property.Name} не доступно для записи ");
                property.SetValue(model.Value, resultEntity);
            }
            Entities.Update(resultEntity);
            await DataContext.SaveChangesAsync();
            return Ok(resultEntity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}