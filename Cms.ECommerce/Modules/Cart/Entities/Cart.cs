using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.Cart.Entities;

public class Cart: Entity
{
    public Guid Uid { get; set; }
    public string? Name { get; set; }
    public List<CartItem.Entities.CartItem> Items { get; set; } = new();
    protected override int GetClassId() => ClassNames.Cart;
}