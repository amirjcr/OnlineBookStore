
using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMS.Infrastrucure.Configurations;

public class BookFeaturesConfigruation : IEntityTypeConfiguration<BookFeature>
{
    public void Configure(EntityTypeBuilder<BookFeature> builder)
    {
        builder.Property(p => p.Titel).HasMaxLength(100).IsRequired();

        builder.Property(p=>p.Value).HasMaxLength(600).IsRequired();



        builder.HasOne(one => one.Book)
            .WithMany(many => many.Features)
            .HasForeignKey(f => f.BookId);
    }
}
