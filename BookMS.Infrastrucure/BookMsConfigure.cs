using Microsoft.EntityFrameworkCore;
using BookMS.Infrastrucure.Configurations;


namespace BookMS.Infrastrucure;

internal static class BookMsConfigure
{
    public static void CustomConfigure(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new BookTypeConfiguration());
        builder.ApplyConfiguration(new BookImagesConfiguration());
        builder.ApplyConfiguration(new BookFeaturesConfigruation());
        builder.ApplyConfiguration(new CategoryConfiguration());
    }
}
