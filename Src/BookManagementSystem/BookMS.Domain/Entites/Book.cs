

namespace BookMS.Domain.Entites;
public class Book : BaseEntity<long>
{
    public Book(string bookTitel, string bookDescription, DateOnly publishedDate, string coverImage, bool isAvaible, int categoryId, ICollection<BookType> bookTypes, ICollection<BookFeature> features, ushort pageSize, ICollection<BookImages> images)
    {
        BookTitel = bookTitel;
        BookDescription = bookDescription;
        PublishedDate = publishedDate;
        CoverImage = coverImage;
        IsAvaible = isAvaible;
        CategoryId = categoryId;
        BookTypes = bookTypes;
        Features = features;
        PageSize = pageSize;
        Images = images;
    }

    public string BookTitel { get; private set; }
    public string BookDescription { get; private set; }
    public DateOnly PublishedDate { get; private set; }
    public string CoverImage { get; private set; }
    public bool IsAvaible { get; private set; }
    public ushort PageSize { get; private set; }

    public int CategoryId { get; private set; }
    public ICollection<BookType> BookTypes { get; private set; }
    public ICollection<BookFeature> Features { get; private set; }
    public ICollection<BookImages> Images { get; private set; }
    public Category Category { get; private set; }
}
