using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMS.Infrastrucure.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.BookTitel).HasMaxLength(150).IsRequired();
        builder.Property(P => P.BookDescription).HasMaxLength(600).IsRequired(false);

        builder.Property(p => p.PublishedDate).IsRequired();
        builder.Property(p => p.CoverImage).IsRequired();
        builder.Property(p => p.PageSize).IsRequired();



        builder.HasOne(one => one.Category)
                .WithMany(many => many.Books)
                .HasForeignKey(x => x.CategoryId);

        builder.HasMany(many => many.Images)
                .WithMany(many => many.Books);

        builder.HasMany(many => many.Features)
                .WithOne(one => one.Book)
                .HasForeignKey(f => f.BookId);


        builder.HasMany(many => many.BookTypes)
                .WithMany(many => many.Books);
    }
}
