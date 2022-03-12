using Microsoft.EntityFrameworkCore;
using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMS.Infrastrucure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Titel).HasMaxLength(150).IsRequired();

        builder.HasOne(one => one.Parent).WithMany(many => many.Children)
            .HasForeignKey(f => f.ParentId);

        builder.HasMany(many => many.Books)
               .WithOne(one => one.Category)
               .HasForeignKey(f => f.CategoryId);
    }
}
