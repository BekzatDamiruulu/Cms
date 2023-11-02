using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Shared.Modules.Image.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Entities.Image>
{
    public void Configure(EntityTypeBuilder<Cms.Shared.Modules.Image.Entities.Image> builder)
    {
        builder.ToTable("Images");
    }
}