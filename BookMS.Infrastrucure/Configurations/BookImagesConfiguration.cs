
using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMS.Infrastrucure.Configurations;

public class BookImagesConfiguration : IEntityTypeConfiguration<BookImages>
{
    public void Configure(EntityTypeBuilder<BookImages> builder)
    {
        builder.Property(p => p.ImageName).IsRequired();
        builder.Property(p => p.ImageSize).IsRequired();

        builder.HasMany(many => many.Books)
            .WithMany(many => many.Images);
    }
}
