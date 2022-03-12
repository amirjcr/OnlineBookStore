using Microsoft.EntityFrameworkCore;
using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMS.Infrastrucure.Configurations;

public class BookTypeConfiguration : IEntityTypeConfiguration<BookType>
{
    public void Configure(EntityTypeBuilder<BookType> builder)
    {
        builder.Property(p => p.TypeName).HasMaxLength(200).IsRequired();

        builder.HasMany(many=>many.Books)
            .WithMany(many=>many.BookTypes);
    }
}
