using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.ECommerce.Modules.OrderItem.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<Entities.OrderItem>
{
    public void Configure(EntityTypeBuilder<Entities.OrderItem> builder)
    {
        builder.ToTable("OrderItems");
    }
}