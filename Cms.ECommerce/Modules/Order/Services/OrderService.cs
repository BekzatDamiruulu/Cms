using Cms.Shared.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Order.Services;

public class OrderService
{
    private readonly DataContext _dataContext;

    public OrderService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // public async Task<Entities.Order>  Create([FromBody]Entities.Order order, long cartId)
    // {
    //     var cartItem =await _dataContext.Set<Cart.Entities.Cart>().FirstOrDefaultAsync(item=>item.Id == cartId);
    //     if(cartItem == null) throw new NullReferenceException();
    //     var orderItem = new OrderItem.Entities.OrderItem() { CommodityId = cartItem.Id};
    // }
    
}