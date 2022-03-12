using BookMS.Application.Interfaces;
using BookMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BookMS.Infrastrucure.Impelementions;

public class BookMSDbContext : DbContext, IBookMSDbContext
{
    public BookMSDbContext(DbContextOptions<BookMSDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.CustomConfigure();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookFeature> BookFeatures { get; set; }
    public DbSet<BookType> BookTypes { get; set; }
    public DbSet<BookImages> BookImages { get; set; }
}