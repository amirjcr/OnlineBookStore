

namespace BookMS.Domain.Entites;

public class BookImages : BaseEntity<Guid>
{

    public BookImages()
    {
            
    }
    public BookImages(string imageName, uint imageSize, ICollection<Book> books)
    {
        ImageName = imageName;
        ImageSize = imageSize;
        Books = books;
    }

    public string ImageName { get; private set; }
    public uint ImageSize { get; private set; }

    public ICollection<Book> Books { get; private set; }
}
