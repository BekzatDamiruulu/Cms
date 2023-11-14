using Cms.Shared.Shared;
using Cms.Shared.Shared.Controllers;
using Cms.Shared.Shared.Models;
using Cms.Shared.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Category.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : SharedController<Entities.Category>
{
    public CategoryController( DataContext dataContext) : base( dataContext)
    {
    }
    protected override  IQueryable<Entities.Category> FilterPredicate(Filter filter, IQueryable<Entities.Category> entities)
    {
        switch (filter.Name)
        {
            case "Id":
                if (int.TryParse(filter.Value.GetString(), out int intId))
                {
                    return entities.Where(e => e.Id == intId);
                }
                break;
        }
        return entities;
    }
    
    public override Task<List<Entities.Category>> Read(string filter = @"[{""name"":""Id"",""value"":""1""}]", int pageIndex = 1, int pageSize = 20, string orderField = "Id", string orderType = "ASC")
    {
        return base.Read(filter, pageIndex, pageSize, orderField, orderType);
    }
}