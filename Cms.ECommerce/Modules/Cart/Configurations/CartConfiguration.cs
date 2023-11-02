using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.ECommerce.Modules.Cart.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Entities.Cart>
{
    public void Configure(EntityTypeBuilder<Entities.Cart> builder)
    {
        builder.ToTable("Carts");
    }
}