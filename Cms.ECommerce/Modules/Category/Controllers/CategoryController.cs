using Cms.Shared.Shared;
using Cms.Shared.Shared.Controllers;
using Cms.Shared.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Category.Controllers;

[ApiController]
[Route("category/api/[controller]")]
public class CategoryController : SharedController<Entities.Category>
{
    public CategoryController( DataContext dataContext) : base( dataContext)
    {
    }
}