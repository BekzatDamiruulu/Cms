using Cms.Shared.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.CartItem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly DataContext _dataContext;

    public CartItemController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    private  DbSet<Entities.CartItem> Items => _dataContext.Set<Entities.CartItem>();
    [HttpPost]
    public  async Task<Entities.CartItem> Create(Entities.CartItem item)
    {
        await Items.AddAsync(item);
        await _dataContext.SaveChangesAsync();
        return item;
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(long cartItemId)
    {
        try
        {
            var cartItem = await Items.FirstOrDefaultAsync(i => i.Id == cartItemId);
            if (cartItem == null) throw new NullReferenceException();
            Items.Remove(cartItem);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    } 
}