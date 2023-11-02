using Cms.Shared.Shared;
using Cms.Shared.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.CommodityCategory.Services;

public class CommodityCategoryService
{
    private readonly DataContext _dataContext;

    public CommodityCategoryService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Entities.CommodityCategory> Create(long categoryId,long commodityId)
    {
        var commodityCategory = new Entities.CommodityCategory() {CommodityId = commodityId,CategoryId = categoryId};
        await _dataContext.Set<Entities.CommodityCategory>().AddAsync(commodityCategory);
        await _dataContext.SaveChangesAsync();
        return commodityCategory;
    }
}