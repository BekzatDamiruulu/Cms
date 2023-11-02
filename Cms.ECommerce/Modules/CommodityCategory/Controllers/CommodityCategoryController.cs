using Cms.ECommerce.Modules.CommodityCategory.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cms.ECommerce.Modules.CommodityCategory.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CommodityCategoryController : ControllerBase
{
    private readonly CommodityCategoryService _commodityCategoryService;

    public CommodityCategoryController(CommodityCategoryService commodityCategoryService)
    {
        _commodityCategoryService = commodityCategoryService;
    }

    [HttpPost]
    public async Task<Entities.CommodityCategory> Create([FromBody] ServiceId serviceId)
    {
        return await _commodityCategoryService.Create(serviceId.CategoryId,serviceId.CommodityId);
    }
}