using BookMS.Application.Interfaces;
using BookMS.Infrastrucure.Impelementions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace BookMS.Infrastrucure;

public static class BookMSDI
{
    public static void RegisterServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BookMSDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IBookMSDbContext>(x => x.GetRequiredService<BookMSDbContext>());

        services.AddScoped<IBookFeatureService, BookFeatureService>();
        services.AddScoped<IBookImagesService, BookImageService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBookTypeService, BookTypeService>();
    }
}
