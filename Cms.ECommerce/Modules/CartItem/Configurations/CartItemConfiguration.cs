using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.ECommerce.Modules.CartItem.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<Entities.CartItem>
{
    public void Configure(EntityTypeBuilder<Entities.CartItem> builder)
    {
        builder.ToTable("CartItems");
    }
}