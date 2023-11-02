using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.ECommerce.Modules.CommodityCategory.Configurations;

public class CommodityCategoryConfiguration: IEntityTypeConfiguration<Entities.CommodityCategory>
{
    public void Configure(EntityTypeBuilder<Entities.CommodityCategory> builder)
    {
        builder.ToTable("CommodityCategory");
    }
}