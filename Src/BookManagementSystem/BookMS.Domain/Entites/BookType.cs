

namespace BookMS.Domain.Entites;

public class BookType : BaseEntity<byte>
{
    public BookType(string typeName, ICollection<Book> books)
    {
        TypeName = typeName;
        Books = books;
    }

    public string TypeName { get; private set; }

    public ICollection<Book> Books { get; private set; }
}