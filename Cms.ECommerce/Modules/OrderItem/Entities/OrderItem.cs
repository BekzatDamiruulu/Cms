using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.OrderItem.Entities;

public class OrderItem : Entity
{
    public long CommodityId { get; set; }
    public Commodity.Entities.Commodity Commodity { get; set; } = null!;
    public long OrderId { get; set; }
    public Order.Entities.Order Order { get; set; } = null!;
    public int Count { get; set; }
    protected override int GetClassId() => ClassNames.OrderItem;
}