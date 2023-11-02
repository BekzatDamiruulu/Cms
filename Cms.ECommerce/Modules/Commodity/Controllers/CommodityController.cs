using Cms.Shared.Shared;
using Cms.Shared.Shared.Controllers;
using Cms.Shared.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Commodity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommodityController : SharedController<Entities.Commodity>
{
    public CommodityController(DataContext dataContext) : base(dataContext)
    {
    }
}