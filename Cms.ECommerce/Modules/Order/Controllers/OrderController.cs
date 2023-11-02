using Cms.ECommerce.Modules.Order.Services;
using Cms.Shared.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Order.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly DataContext _dataContext;
    

    public OrderController( DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]Entities.Order order,long cartId)
    {
        try
        {
            var cart =await _dataContext.Set<Cart.Entities.Cart>().Include(c => c.Items)
                .FirstOrDefaultAsync(cart => cart.Id == cartId);
            order.Items = cart.Items.Select(item => new OrderItem.Entities.OrderItem(){CommodityId = item.CommodityId,Commodity = item.Commodity,Count = item.Count}).ToList();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}