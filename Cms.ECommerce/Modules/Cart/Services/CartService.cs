using Cms.Shared.Shared;
using Cms.Shared.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Cms.ECommerce.Modules.Cart.Services;

public class CartService
{
    private readonly DataContext _dataContext;
    private readonly UidService _uidService;

    public CartService(DataContext dataContext, UidService uidService)
    {
        _dataContext = dataContext;
        _uidService = uidService;
    }
    public async Task<Entities.Cart> GetCart()
    {
        var uid = _uidService.GetUid();
        
        var cart = await _dataContext.Set<Entities.Cart>().Include(cart => cart.Items)
            .FirstOrDefaultAsync(x => x.Uid == uid);

        if (cart != null) return cart;
        
        cart = new Entities.Cart
        {
            Uid = uid
        };
        _dataContext.Set<Entities.Cart>().Add(cart);
        await _dataContext.SaveChangesAsync();

        return cart;
    }
}