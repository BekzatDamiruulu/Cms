using Cms.Shared.Shared;

namespace Cms.ECommerce.Modules.CartItem.CartItemService;

public class CartItemService
{
    private readonly DataContext _dataContext;

    public CartItemService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
}