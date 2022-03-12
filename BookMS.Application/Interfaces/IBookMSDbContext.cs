using Microsoft.EntityFrameworkCore;

namespace BookMS.Application.Interfaces;
public interface IBookMSDbContext : IDisposable
{
    DbSet<Book> Books { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<BookFeature> BookFeatures { get; set; }
    DbSet<BookType> BookTypes { get; set; }
    DbSet<BookImages> BookImages { get; set; }



    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
