using System.Text.Json.Serialization;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.CartItem.Entities;

public class CartItem : Entity
{
    public long CommodityId { get; set; }
    public Commodity.Entities.Commodity Commodity { get; set; } = new();
    public long CartId { get; set; }
    
    [JsonIgnore]
    public Cart.Entities.Cart Cart { get; set; } = null!;
    public int Count { get; set; }
    protected override int GetClassId() => ClassNames.CartItem;
}