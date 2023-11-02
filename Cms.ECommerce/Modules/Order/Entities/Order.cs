using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.Order.Entities;

public class Order : Entity
{
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public List<OrderItem.Entities.OrderItem> Items { get; set; }
    protected override int GetClassId() => ClassNames.Order;
}